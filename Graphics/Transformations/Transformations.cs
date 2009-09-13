using System.Collections.Generic;
using OpenTK.Graphics;

namespace Graphics.Transformations
{
	public static class Transformations
	{
		public static void Apply(this IEnumerable<Transformation> transformations)
		{
			foreach (Transformation transformation in transformations) transformation.Apply();
		}
	}
}
