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
		int[] textTextures = new int[1];
		int characterLists;
		Color clearColor;
		
		// DEBUG
		List<List<PointF>> streams;
		float[] vertices;
		int streamLists;
		int[] vertexBufferObjects;
		
		public Color ClearColor
		{
			get { return clearColor; }
			set { GL.ClearColor(clearColor = value); }
		}

		public Viewport() : base(new GraphicsMode(DisplayDevice.Default.BitsPerPixel, 0, 0, 0, 0, 2, false))
		{
			Layout += viewport_Layout;
			
			ClearColor = Color.Black;
			
			System.Console.WriteLine("GraphicsMode: " + GraphicsMode.ToString());

			// DEBUG
			GL.EnableClientState(EnableCap.VertexArray);
			
			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.LineSmooth);
			GL.Enable(EnableCap.Blend);

			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			#region Create and initialize text texture
			GL.GenTextures(textTextures.Length, textTextures);

			GL.BindTexture(TextureTarget.Texture2D, textTextures[0]);

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

				GL.Begin(BeginMode.Quads);

				GL.TexCoord2(textureBounds.Left, textureBounds.Top); GL.Vertex2(worldBounds.Left, worldBounds.Top);
				GL.TexCoord2(textureBounds.Right, textureBounds.Top); GL.Vertex2(worldBounds.Right, worldBounds.Top);
				GL.TexCoord2(textureBounds.Right, textureBounds.Bottom); GL.Vertex2(worldBounds.Right, worldBounds.Bottom);
				GL.TexCoord2(textureBounds.Left, textureBounds.Bottom); GL.Vertex2(worldBounds.Left, worldBounds.Bottom);

				GL.End();

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
			GL.BindTexture(TextureTarget.Texture2D, textTextures[0]);

			foreach (char character in text)
			{
				GL.CallList(characterLists + characters.IndexOf(character));
				GL.Translate(characterSize.Width, 0, 0);
			}
			
			GL.BindTexture(TextureTarget.Texture2D, 0);

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
			if (this.streams != null)
			{
				GL.DeleteLists(streamLists, 1);
				GL.DeleteBuffers(vertexBufferObjects.Length, vertexBufferObjects);
			}
			
			this.streams = streams;
			
			IEnumerable<float> vertices = from stream in streams
										  from point in stream
										  from value in new[] { point.X + 0.5f, point.Y + 0.5f }
										  select value;
			
			// TODO: Apparently, the array gets garbage collected otherwise
			this.vertices = vertices.ToArray();
			
			GL.VertexPointer(2, VertexPointerType.Float, 0, this.vertices);
			
			streamLists = GL.GenLists(1);
			GL.NewList(streamLists, ListMode.Compile);
			for (int stream = 0; stream < this.streams.Count; stream++) GL.DrawArrays(BeginMode.LineStrip, stream * 1000, 1000);
			GL.EndList();
			
			vertexBufferObjects = new int[1];
			GL.GenBuffers(vertexBufferObjects.Length, vertexBufferObjects);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObjects[0]);
			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(this.vertices.Length * sizeof(float)), this.vertices, BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}
		public void DrawStreams(int mode)
		{
			switch(mode)
			{
				case 1:
					foreach (List<PointF> points in streams) DrawLineStrip(points, Color.Red, 1.5f);
					break;
				case 2:
					GL.LineWidth(1.5f);
					GL.Color3(Color.Lime);
					
					for (int stream = 0; stream < streams.Count; stream++) GL.DrawArrays(BeginMode.LineStrip, stream * 1000, 1000);
					break;
				case 3:
					GL.LineWidth(1.5f);
					GL.Color3(Color.Blue);
					
					GL.CallList(streamLists);
					break;
				case 4:
					GL.LineWidth(1.5f);
					GL.Color3(Color.Yellow);
					
					GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObjects[0]);
					GL.VertexPointer(2, VertexPointerType.Float, 0, 0);
					
					for (int stream = 0; stream < streams.Count; stream++) GL.DrawArrays(BeginMode.LineStrip, stream * 1000, 1000);
					
					GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
					GL.VertexPointer(2, VertexPointerType.Float, 0, this.vertices);
					break;
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

				GL.DeleteTextures(textTextures.Length, textTextures);
				GL.DeleteLists(characterLists, characters.Length);
				
				// DEBUG
				GL.DeleteLists(streamLists, 1);
				
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