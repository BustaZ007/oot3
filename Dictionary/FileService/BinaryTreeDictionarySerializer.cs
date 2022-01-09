using System.Runtime.Serialization.Json;
using Dictionary.Dictionary;

namespace Dictionary.FileService;

public class BinaryTreeDictionarySerializer
{
    private readonly IFileService _fileService;

    public BinaryTreeDictionarySerializer(IFileService fileService)
    {
        this._fileService = fileService;
    }

    public void Serialize<TKey, TValue>(BinaryTreeDictionary<TKey, TValue> dictionary, string filename, bool closeStream = true) where TKey: IComparable
    {
        var formatter = new DataContractJsonSerializer(typeof(BinaryTreeDictionary<TKey, TValue>));

        Stream fs = _fileService.GetWriteStream(filename);
        formatter.WriteObject(fs, dictionary);

        if (closeStream)
            fs.Close();
    }

    public BinaryTreeDictionary<TKey, TValue> Deserialize<TKey, TValue>(string filename, bool closeStream = true) where TKey: IComparable
    {
        var formatter = new DataContractJsonSerializer(typeof(BinaryTreeDictionary<TKey, TValue>));
        Stream fs = _fileService.GetReadStream(filename);

        var result = (BinaryTreeDictionary<TKey, TValue>) formatter.ReadObject(fs);

        if (closeStream)
            fs.Close();

        return result;
    }
}