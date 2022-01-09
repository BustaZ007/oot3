namespace Dictionary.BinaryTree;

[Serializable]
    public class BinaryTree<T> where T : IComparable<T>, IComparable
    {
        private BinaryTreeNode<T>? _root;

        public bool Find(T element)
        {
            var current = _root;

            while (current != null)
            {
                var compareResult = element.CompareTo(current.Value);
                switch (compareResult)
                {
                    case < 0:
                        current = current.Left;
                        break;
                    case > 0:
                        current = current.Right;
                        break;
                    default:
                        return true;
                }
            }

            return false;
        }

        public bool Insert(T element)
        {
            if (_root == null)
            {
                _root = new BinaryTreeNode<T>(element);
                return true;
            };

            var current = _root;

            while (current != null)
            {
                var compareResult = element.CompareTo(current.Value);
                switch (compareResult)
                {
                    case < 0 when current.Left == null:
                        current.Left = new BinaryTreeNode<T>(element, current );
                        return true;
                    case < 0:
                        current = current.Left;
                        break;
                    case > 0 when current.Right == null:
                        current.Right = new BinaryTreeNode<T>(element, current);
                        return true;
                    case > 0:
                        current = current.Right;
                        break;
                    default:
                        current.Value = element;
                        return true;
                }
            }

            return false;
        }

        public bool Remove(T element)
        {
            var current = _root;

            while (current != null)
            {
                var compareResult = element.CompareTo(current.Value);
                if (compareResult < 0)
                {
                    current = current.Left;
                }
                else if (compareResult > 0)
                {
                    current = current.Right;
                }
                else
                {
                    if (current.Left == null && current.Right == null)
                    {
                        if (current.Parent == null)
                        {
                            _root = null;
                        }
                        else if (current == current.Parent.Left)
                        {
                            current.Parent.Left = null;
                        }
                        else
                        {
                            current.Parent.Right = null;
                        }
                    } else if (current.Left == null)
                    {
                        current.Right.Parent = current.Parent;
                        if (current.Parent == null)
                        {
                            _root = current.Right;
                        }
                        else if (current == current.Parent.Left)
                        {
                            current.Parent.Left = current.Right;
                        }
                        else
                        {
                            current.Parent.Right = current.Right;
                        }
                    }
                    else if (current.Right == null)
                    {
                        current.Left.Parent = current.Parent;
                        if (current.Parent == null)
                        {
                            _root = current.Left;
                        }
                        else if (current == current.Parent.Left)
                        {
                            current.Parent.Left = current.Left;
                        }
                        else
                        {
                            current.Parent.Right = current.Left;
                        }
                    }
                    else
                    {
                        if (current.Parent == null)
                        {
                            _root = current.Right;
                        }
                        else if (current == current.Parent.Left)
                        {
                            current.Parent.Left = current.Right;
                        }
                        else
                        {
                            current.Parent.Right = current.Right;
                        }

                        var newParentRight = current.Right;
                        while (newParentRight.Left != null)
                            newParentRight = newParentRight.Left;

                        newParentRight.Left = current.Left;
                        current.Left.Parent = newParentRight;
                    }

                    return true;
                }
            }

            return false;
        }

        public IEnumerable<T> Traverse()
        {
            return TraverseNode(_root);
        }

        private IEnumerable<T> TraverseNode(BinaryTreeNode<T>? node)
        {
            if (node != null)
            {
                foreach (var element in TraverseNode(node.Left))
                    yield return element;

                yield return node.Value;

                foreach (var element in TraverseNode(node.Right))
                    yield return element;
            }
        }
    }