namespace Extensions
{
	public struct Pair<T>
	{
		readonly T a;
		readonly T b;
		
		public T A { get { return a; } }
		public T B { get { return b; } }
		
		public Pair(T a, T b)
		{
			this.a = a;
			this.b = b;
		}
	}
}
