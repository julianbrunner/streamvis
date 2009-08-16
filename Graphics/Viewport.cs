using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
// TODO: Remove reference to System.Core once this isn't needed anymore
using System.Linq;

namespace Graphics
{
	public class Viewport : GLControl
	{
		static readonly Size characterSize = new Size(7, 12);
		static readonly string characters = "+-.0123456789";
		
		bool disposed = false;
		int textTexture;
		int characterLists;
		Color clearColor;
		
		// DEBUG
		int streamLists;
		int streamCount;
		float[] vertices;
		
		public Color ClearColor
		{
			get { return clearColor; }
			set { GL.ClearColor(clearColor = value); }
		}

		public Viewport()
		{			
			Layout += viewport_Layout;
			ClearColor = Color.Black;

			// DEBUG
			GL.EnableClientState(EnableCap.VertexArray);
			
			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.LineSmooth);
			GL.Enable(EnableCap.Blend);

			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			#region Create and initialize text texture
			GL.GenTextures(1, out textTexture);

			GL.BindTexture(TextureTarget.Texture2D, textTexture);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			Bitmap bitmap = new Bitmap("Text.png");
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, OpenTK.Graphics.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

			bitmap.UnlockBits(bitmapData);

			GL.BindTexture(TextureTarget.Texture2D, 0);

			GL.MatrixMode(MatrixMode.Texture);
			GL.LoadIdentity();
			Glu.Ortho2D(-bitmap.Width, bitmap.Width, -bitmap.Height, bitmap.Height);
			#endregion

			#region Create character display lists
			characterLists = GL.GenLists(characters.Length);

			for (int character = 0; character < characters.Length; character++)
			{
				Rectangle textureBounds = new Rectangle(new Point(character * characterSize.Width, 0), characterSize);
				Rectangle worldBounds = new Rectangle(Point.Empty, characterSize);

				GL.NewList(characterLists + character, ListMode.Compile);

				GL.BindTexture(TextureTarget.Texture2D, textTexture);

				GL.Begin(BeginMode.Quads);

				GL.TexCoord2(textureBounds.Left, textureBounds.Top); GL.Vertex2(worldBounds.Left, worldBounds.Top);
				GL.TexCoord2(textureBounds.Right, textureBounds.Top); GL.Vertex2(worldBounds.Right, worldBounds.Top);
				GL.TexCoord2(textureBounds.Right, textureBounds.Bottom); GL.Vertex2(worldBounds.Right, worldBounds.Bottom);
				GL.TexCoord2(textureBounds.Left, textureBounds.Bottom); GL.Vertex2(worldBounds.Left, worldBounds.Bottom);

				GL.End();

				GL.BindTexture(TextureTarget.Texture2D, 0);

				GL.EndList();
			}
			#endregion
			
			PrintCapabilities();
		}
		~Viewport()
		{
			Dispose(false);
		}

		public void Begin()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
		}
		public void End()
		{
			SwapBuffers();
		}
		public void DrawNumber(double number, PointF position, Color color, TextAlignment alignment)
		{
			string text = number.ToString("F2", CultureInfo.InvariantCulture);

			int width = text.Length * characterSize.Width;

			switch (alignment)
			{
				case TextAlignment.Near: position.X -= 0.0f * width; break;
				case TextAlignment.Center: position.X -= 0.5f * width; break;
				case TextAlignment.Far: position.X -= 1.0f * width; break;
				default: throw new ArgumentOutOfRangeException("alignment");
			}

			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Translate((int)position.X, (int)position.Y, 0);

			GL.Color3(color);

			foreach (char character in text)
			{
				GL.CallList(characterLists + characters.IndexOf(character));
				GL.Translate(characterSize.Width, 0, 0);
			}

			GL.LoadIdentity();
		}
		public void DrawLine(PointF from, PointF to, Color color, float width)
		{
			GL.LineWidth(width);
			GL.Color3(color);

			GL.Begin(BeginMode.Lines);

			GL.Vertex2(from.X + 0.5f, from.Y + 0.5f);
			GL.Vertex2(to.X + 0.5f, to.Y + 0.5f);

			GL.End();
		}
		public void DrawLineStrip(IEnumerable<PointF> points, Color color, float width)
		{
			GL.LineWidth(width);
			GL.Color3(color);

			GL.Begin(BeginMode.LineStrip);

			foreach (PointF point in points) GL.Vertex2(point.X + 0.5f, point.Y + 0.5f);

			GL.End();
		}

		public void InitializeStreams(List<List<PointF>> streams)
		{
			if (streamCount > 0) GL.DeleteLists(streamLists, streamCount);
			
			streamCount = streams.Count();
			
			IEnumerable<float> vertices = from stream in streams
										  from point in stream
										  from value in new[] { point.X + 0.5f, point.Y + 0.5f }
										  select value;
			
			// TODO: Apparently, the array gets garbage collected otherwise
			this.vertices = vertices.ToArray();
			
			GL.VertexPointer(2, VertexPointerType.Float, 0, this.vertices);
			
			streamLists = GL.GenLists(streamCount);

			for (int stream = 0; stream < streamCount; stream++)
			{
				GL.NewList(streamLists + stream, ListMode.Compile);

				GL.DrawArrays(BeginMode.LineStrip, stream * 1000, 1000);

				GL.EndList();
			}
		}
		public void DrawStream(int mode, IEnumerable<PointF> points, int stream)
		{			
			if (mode == 1) DrawLineStrip(points, Color.Red, 1.5f);
			else
			{
				GL.LineWidth(1.5f);
				
				if (mode == 2) { GL.Color3(Color.Lime); GL.DrawArrays(BeginMode.LineStrip, stream * 1000, 1000); }
				if (mode == 3) { GL.Color3(Color.Blue); GL.CallList(streamLists + stream); }
			}
		}
		public void ToggleTexture2D()
		{
			ToggleCapability(EnableCap.Texture2D);
		}
		public void ToggleLineSmooth()
		{
			ToggleCapability(EnableCap.LineSmooth);
		}
		public void ToggleBlend()
		{
			ToggleCapability(EnableCap.Blend);
		}
		public void ToggleDither()
		{
			ToggleCapability(EnableCap.Dither);
		}
		public void ToggleMultisample()
		{
			ToggleCapability(EnableCap.Multisample);
		}
		public void ToggleCapability(EnableCap capability)
		{
			if (GL.IsEnabled(capability)) GL.Disable(capability);
			else GL.Enable(capability);
		}
		public void PrintCapabilities()
		{
			System.Console.Write("Enabled capabilities: ");
			foreach (EnableCap capability in Enum.GetValues(typeof(EnableCap)))
				if (GL.IsEnabled(capability))
					System.Console.Write(capability + " ");
			System.Console.WriteLine();
		}
		
		protected override void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;

				GL.DeleteTextures(1, ref textTexture);
				GL.DeleteLists(characterLists, characters.Length);
				
				// DEBUG
				GL.DeleteLists(streamLists, streamCount);
				
				base.Dispose(disposing);
			}
		}
		
		void viewport_Layout(object sender, LayoutEventArgs e)
		{
			GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			Glu.Ortho2D(ClientRectangle.Left, ClientRectangle.Right, ClientRectangle.Bottom, ClientRectangle.Top);
		}
	}
}