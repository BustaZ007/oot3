using System.IO;
using System.Linq;
using Dictionary.Dictionary;
using Dictionary.FileService;
using NUnit.Framework;

namespace DictionaryTests;

public class Tests
{
    public class MemoryStreamService : IFileService
    {
        public MemoryStream stream = new MemoryStream();

        public Stream GetReadStream(string name)
        {
            stream.Position = 0;
            return stream;
        }

        public Stream GetWriteStream(string name)
        {
            return stream;
        }
    }
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestSerializeAndDeserialize()
    {
        var testedDic = new BinaryTreeDictionary<string, int>();
        testedDic["1"] = 1;
        testedDic["2"] = 2;

        var serializer = new BinaryTreeDictionarySerializer(new MemoryStreamService());
        serializer.Serialize(testedDic, "123", closeStream: false);
        var newTree = serializer.Deserialize<string, int>("123", closeStream:false);

        CollectionAssert.AreEqual(testedDic.ToList(), newTree.ToList());
    }
}