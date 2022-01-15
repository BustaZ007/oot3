using System.Collections;
using Dictionary.BinaryTree;

namespace Dictionary.Dictionary;

public class BinaryTreeDictionary<TK,TV> : IDictionary<TK,TV> where TK : IComparable
{
    private BinaryTree<BinaryTreeDictionaryItem<TK, TV>> _binaryTree = new BinaryTree<BinaryTreeDictionaryItem<TK, TV>>();
    public int Count => _binaryTree.Traverse().Count();
    public bool IsReadOnly => false;
    public ICollection<TK> Keys => (ICollection<TK>) _binaryTree.Traverse().Select(i => i.Key).ToList();
    public ICollection<TV> Values => (ICollection<TV>) _binaryTree.Traverse().Select(i => i.Value).ToList();
    //Я не нашел способа сделать BinaryTree<KeyValuePair<TK, TV>> без добавления собственного класса BinaryTreeDictionaryItem
    //так как KeyValuePair, насколько я понял, не реализует метод CompareTo без указания дженериков, поэтому далее 
    //почти во всех методах используется каст BinaryTreeDictionaryItem<TK, TV> => KeyValuePair<TK, TV> и обратно
    //Можно было, конечно, жестко зашить параметры <TK, TV> в BinaryTree, но тогда мне кажется это уже будет не совсем
    //универсальное дерево поиска
    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
    {
        return _binaryTree
            .Traverse()
            .Select(item => new KeyValuePair<TK, TV>(item.Key, item.Value))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(KeyValuePair<TK, TV> item)
    {
        var (key, value) = item;
        _binaryTree.Insert(new BinaryTreeDictionaryItem<TK, TV>(key, value));
    }

    public void Clear()
    {
        _binaryTree = new BinaryTree<BinaryTreeDictionaryItem<TK, TV>>();
    }

    public bool Contains(KeyValuePair<TK, TV> item)
    {
        var (key, value) = item;
        var finditem = _binaryTree.Find(new BinaryTreeDictionaryItem<TK, TV>(key, value));
        if (finditem != null && finditem.Value == null && value == null)
        {
            return true;
        }
        return finditem != null && finditem.Key.CompareTo(key) == 0 && finditem.Value != null &&  finditem.Value.Equals(value);
    }

    public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex)
    {
        _binaryTree
             .Traverse()
             .Select(item => new KeyValuePair<TK, TV>(item.Key, item.Value))
             .ToArray()
             .CopyTo(array,arrayIndex);
    }

    public bool Remove(KeyValuePair<TK, TV> item)
    {
        var (key, value) = item;
        var newItem = new BinaryTreeDictionaryItem<TK, TV>(key, value);
        var findItem = _binaryTree.Find(newItem);
        if (findItem != null && findItem.Value == null && value == null)
        {
            return true;
        }
        if (findItem != null && findItem.Value != null && findItem.Value.Equals(value))
        {
            return _binaryTree.Remove(newItem);
        }

        return false;
    }
    
    public void Add(TK key, TV value)
    {
        _binaryTree.Insert(new BinaryTreeDictionaryItem<TK, TV>(key, value));
    }

    public bool ContainsKey(TK key)
    {
        return _binaryTree.Find(new BinaryTreeDictionaryItem<TK,TV>(key)).Key.CompareTo(key) == 0;
    }
    //Из-за проблем описанных выше, пришлось добавить еще один конструктор для BinaryTreeDictionaryItem.
    public bool Remove(TK key)
    {
        return _binaryTree.Remove(new BinaryTreeDictionaryItem<TK, TV>(key));
    }

    public bool TryGetValue(TK key, out TV? value)
    {
        var item = _binaryTree.Find(new BinaryTreeDictionaryItem<TK, TV>(key));
        value = item.Value;

        return item != null;
    }

    public TV this[TK key]
    {
        get => _binaryTree.Find(new BinaryTreeDictionaryItem<TK, TV>(key)).Value;
        set => Add(key, value);
    }
}