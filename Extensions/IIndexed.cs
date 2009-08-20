namespace Extensions
{
	public interface IIndexed<TItem, TIndex>
	{
		TItem this[TIndex index] { get; }
	}
}
