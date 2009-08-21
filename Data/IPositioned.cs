namespace Data
{
	public interface IPositioned<TPosition>
	{
		TPosition Position { get; }
	}
}
