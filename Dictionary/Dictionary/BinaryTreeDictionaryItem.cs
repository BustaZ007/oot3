namespace Dictionary.Dictionary;

public class BinaryTreeDictionaryItem<TK, TV> :  IComparable, IComparable<BinaryTreeDictionaryItem<TK, TV>> 
    where TK: IComparable
{
    public BinaryTreeDictionaryItem(TK key, TV value)
    {
        Key = key;
        Value = value;
    }
    public BinaryTreeDictionaryItem(TK key)
    {
        Key = key;
    }

    public TK Key { get; }
    public TV? Value { get; }

    public int CompareTo(BinaryTreeDictionaryItem<TK, TV>? other)
    {
        return Key.CompareTo(other!.Key);
    }

    public int CompareTo(object? obj)
    {
        throw new NotImplementedException();
    }
}