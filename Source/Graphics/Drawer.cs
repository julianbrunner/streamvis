// Copyright © Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using OpenTK.Graphics;
using OpenTK.Math;

namespace Graphics
{
	public class Drawer : IDisposable
	{
		static readonly Size characterSize = new Size(7, 12);
		static readonly string characters = "+-.0123456789E";

		bool disposed = false;
		int[] textTextures = new int[1];
		int characterLists;

		public Drawer(bool lineSmoothing)
		{
			GL.EnableClientState(EnableCap.VertexArray);

			GL.Enable(EnableCap.Texture2D);
			if (lineSmoothing)
			{
				GL.Enable(EnableCap.LineSmooth);
				GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			}
			GL.Enable(EnableCap.Blend);
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
		public Size GetTextSize(double number)
		{
			string text = GetNumberString(number);

			return new Size(text.Length * characterSize.Width, characterSize.Height);
		}
		public void DrawNumber(double number, Vector2 position, Color color, TextAlignment alignment)
		{
			string text = GetNumberString(number);

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

			foreach (char character in text) GL.CallList(characterLists + characters.IndexOf(character));

			GL.BindTexture(TextureTarget.Texture2D, 0);

			GL.LoadIdentity();
		}
		public void DrawLine(Vector2 start, Vector2 end, Color color, float width)
		{
			GL.LineWidth(width);
			GL.Color3(color);

			GL.Begin(BeginMode.Lines);

			GL.Vertex2(start.X + 0.5f, start.Y + 0.5f);
			GL.Vertex2(end.X + 0.5f, end.Y + 0.5f);

			GL.End();
		}
		public void DrawLineStrip(float[] vertices, Matrix4 transformation, Color color, float width)
		{
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.Translate(0.5f, 0.5f, 0);
			GL.MultMatrix(ref transformation);

			GL.LineWidth(width);
			GL.Color3(color);

			GL.VertexPointer(2, VertexPointerType.Float, 0, vertices);
			GL.DrawArrays(BeginMode.LineStrip, 0, vertices.Length / 2);
			GL.VertexPointer(2, VertexPointerType.Float, 0, IntPtr.Zero);

			GL.LoadIdentity();
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

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Graphics.Images.Text.png"))
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
				GL.NewList(characterLists + character, ListMode.Compile);

				GL.Begin(BeginMode.Quads);
				DrawCharacter(character);
				GL.End();

				GL.Translate(characterSize.Width, 0, 0);

				GL.EndList();
			}
		}

		static void DrawCharacter(int character)
		{
			Rectangle textureBounds = new Rectangle(new Point(character * characterSize.Width, 0), characterSize);
			Rectangle worldBounds = new Rectangle(Point.Empty, characterSize);

			GL.TexCoord2(textureBounds.Left, textureBounds.Top); GL.Vertex2(worldBounds.Left, worldBounds.Top);
			GL.TexCoord2(textureBounds.Right, textureBounds.Top); GL.Vertex2(worldBounds.Right, worldBounds.Top);
			GL.TexCoord2(textureBounds.Right, textureBounds.Bottom); GL.Vertex2(worldBounds.Right, worldBounds.Bottom);
			GL.TexCoord2(textureBounds.Left, textureBounds.Bottom); GL.Vertex2(worldBounds.Left, worldBounds.Bottom);
		}
		static string GetNumberString(double number)
		{
			string decimalString = number.ToString("0.##", CultureInfo.InvariantCulture);
			string scientificString = number.ToString("0.##E+0", CultureInfo.InvariantCulture);
			return decimalString.Length <= 10 ? decimalString : scientificString;
		}
	}
}
