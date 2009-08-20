using System;

namespace Data
{
	public class BinarySearcher
	{
		readonly IBinarySearchable source;
		
		public BinarySearcher(IBinarySearchable source)
		{
			this.source = source;
		}
		
		public int GetIndex(Time time)
		{
			return GetIndex(0, source.Count, time);
		}

		/// <summary>
		/// Returns the index of the first item which has a timestamp that is greater than or equal to <paramref name="time"/>.
		/// If no such item is found, <paramref name="end"/> is returned.
		/// </summary>
		/// <param name="start">The start index of the range to search.</param>
		/// <param name="end">The end index of the range to search.</param>
		/// <param name="time">The time to search for.</param>
		/// <returns>The index of the first item which has a timestamp that is greater than or equal to <paramref name="time"/>.</returns>
		int GetIndex(int start, int end, Time time)
		{
			if (start == end) return start;

			int index = (start + end) / 2;

			if (source[index].Time > time) return GetIndex(start, index, time);
			if (source[index].Time < time) return GetIndex(index + 1, end, time);

			return index;
		}
	}
}
