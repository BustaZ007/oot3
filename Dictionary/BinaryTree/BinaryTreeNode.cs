namespace Dictionary.BinaryTree;

[Serializable]
public class BinaryTreeNode<T> where T : IComparable
{
    public T Value;
    public BinaryTreeNode<T>? Left;
    public BinaryTreeNode<T>? Right;
    public BinaryTreeNode<T>? Parent;

    public BinaryTreeNode(T value, BinaryTreeNode<T>? parent = null)
    {
        Value = value;
        Parent = parent;
    }
}