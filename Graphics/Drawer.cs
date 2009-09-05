using System;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK.Graphics;

namespace Graphics
{
	public class Drawer : IDisposable
	{
		static readonly Size characterSize = new Size(7, 12);
		static readonly string characters = "+-.0123456789E";
		
		bool disposed = false;
		int[] textTextures = new int[1];
		int characterLists;
		
		public Drawer()
		{
			GL.Enable(EnableCap.Texture2D);
			//GL.Enable(EnableCap.LineSmooth);
			GL.Enable(EnableCap.Blend);

			//GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			
			InitializeTextTexture();
			InitializeCharacterDisplayLists();
		}
		~Drawer()
		{
			Dispose(false);
		}
		
		public void Dispose()
		{
			Dispose(true);
		}
		public void DrawNumber(double number, PointF position, Color color, TextAlignment alignment)
		{
			string decimalString = number.ToString("0.##", CultureInfo.InvariantCulture);
			string scientificString = number.ToString("0.##E+0", CultureInfo.InvariantCulture);
			string text = decimalString.Length <= scientificString.Length ? decimalString : scientificString;

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
		
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;

				GL.DeleteTextures(textTextures.Length, textTextures);
				GL.DeleteLists(characterLists, characters.Length);
			}
		}
		
		void InitializeTextTexture()
		{
			GL.GenTextures(textTextures.Length, textTextures);

			GL.BindTexture(TextureTarget.Texture2D, textTextures[0]);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Graphics.Text.png"))
			using (Bitmap bitmap = new Bitmap(stream))
			{
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, OpenTK.Graphics.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
				bitmap.UnlockBits(bitmapData);
				
				GL.MatrixMode(MatrixMode.Texture);
				GL.LoadIdentity();
				Glu.Ortho2D(-bitmap.Width, bitmap.Width, -bitmap.Height, bitmap.Height);
			}

			GL.BindTexture(TextureTarget.Texture2D, 0);
		}
		void InitializeCharacterDisplayLists()
		{
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
		}
	}
}
