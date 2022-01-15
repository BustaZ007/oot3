using System.Collections.Generic;
using System.Linq;
using Dictionary.Dictionary;

namespace DictionaryTests;
using NUnit.Framework;

public class BinaryTreeDictionaryTests
{
        [SetUp]
        public void Setup()
        {
        }
        //Adding tests
        [Test]
        public void AddItemTest()
        {
            var _trustedDictionary = new Dictionary<string, int>{
                {"1", 1}
            };
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic.Add("1", 1);
            
            Assert.AreEqual(1, testedDic.Count);
            Assert.AreEqual(0, _trustedDictionary.Except(testedDic).Count());
        }
        [Test]
        public void AddItemByKeyPairTest()
        {
            var _trustedDictionary = new Dictionary<string, int>{
                {"1", 1}
            };
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic.Add(new KeyValuePair<string, int>("1", 1));
            
            Assert.AreEqual(1, testedDic.Count);
            Assert.AreEqual(0, _trustedDictionary.Except(testedDic).Count());
        }
        [Test]
        public void AddItemByKeyTest()
        {
            var _trustedDictionary = new Dictionary<string, int>{
                {"1", 1}
            };
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            Assert.AreEqual(1, testedDic.Count);
            Assert.AreEqual(0, _trustedDictionary.Except(testedDic).Count());
        }
        
        [Test]
        public void AddDuplicateItemTest()
        {
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            testedDic["1"] = 54;
            
            Assert.AreEqual(1, testedDic.Count);
            Assert.AreEqual(54, testedDic["1"]);
        }
        //Removing tests
        [Test]
        public void RemoveItemTest()
        {
            var _trustedDictionary = new Dictionary<string, int>{
                {"1", 1}
            };
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            testedDic["2"] = 2;
            
            Assert.AreEqual(2, testedDic.Count);
            Assert.AreEqual(true,testedDic.Remove(new KeyValuePair<string, int>("2", 2)));
            Assert.AreEqual(1, testedDic.Count);
            Assert.AreEqual(false,testedDic.Remove(new KeyValuePair<string, int>("1", 2)));
            Assert.AreEqual(0, _trustedDictionary.Except(testedDic).Count());
        }
        [Test]
        public void RemoveItemWithNullValueTest()
        {
            var dict = new BinaryTreeDictionary<int, string>();
            dict[0] = null;
            Assert.AreEqual(true,dict.Remove(new KeyValuePair<int, string>(0, null)));
        }
        [Test]
        public void RemoveItemByKeyTest()
        {
            var _trustedDictionary = new Dictionary<string, int>{
                {"1", 1}
            };
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            testedDic["2"] = 2;
            
            Assert.AreEqual(2, testedDic.Count);
            Assert.AreEqual(true,testedDic.Remove("2"));
            Assert.AreEqual(1, testedDic.Count);
            Assert.AreEqual(0, _trustedDictionary.Except(testedDic).Count());
        }
        [Test]
        public void RemoveNonExistentItemTest()
        {
            var _trustedDictionary = new Dictionary<string, int>{
                {"1", 1}
            };
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            
            Assert.AreEqual(1, testedDic.Count);
            Assert.AreEqual(false,testedDic.Remove("2"));
            Assert.AreEqual(false,testedDic.Remove(new KeyValuePair<string, int>("2", 2)));
            Assert.AreEqual(1, testedDic.Count);
            Assert.AreEqual(0, _trustedDictionary.Except(testedDic).Count());
        }
        [Test]
        public void ClearTest()
        {
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            testedDic["2"] = 2;
        
            Assert.AreEqual(2, testedDic.Count);
        
            testedDic.Clear();
        
            Assert.AreEqual(0, testedDic.Count);
        }
        //Getting tests
        [Test]
        public void GetKeysTest()
        {
            var _trustedDictionary = new Dictionary<string, int>{
                {"1", 1}, {"2", 2}
            };
            var testedDic = new BinaryTreeDictionary<string, int>();
        
            testedDic["1"] = 1;
            testedDic["2"] = 2;
        
            var keys = testedDic.Keys;
        
            Assert.AreEqual(_trustedDictionary.Keys, keys);
        }
        [Test]
        public void GetValuesTest()
        {
            var _trustedDictionary = new Dictionary<string, int>{
                {"1", 1}, {"2", 2}
            };
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            testedDic["2"] = 2;
        
            var values = testedDic.Values;
        
            Assert.AreEqual(_trustedDictionary.Values, values);
        }
        [Test]
        public void TryGetValueTest()
        {
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            testedDic["2"] = 2;
            int value = 0; 
            Assert.AreEqual(true, testedDic.TryGetValue("1", out value ));
            Assert.AreEqual(1, value);
        }
        //Other tests
        [Test]
        public void ContainKeyTest()
        {
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;

            Assert.AreEqual(true, testedDic.ContainsKey("1"));
        }
        [Test]
        public void ContainsTest()
        {
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            Assert.AreEqual(true, testedDic.Contains(new KeyValuePair<string, int>("1", 1)));
            Assert.AreEqual(false, testedDic.Contains(new KeyValuePair<string, int>("1", 2)));
            Assert.AreEqual(false, testedDic.Contains(new KeyValuePair<string, int>("2", 2)));
        }
        [Test]
        public void ContainsTestOfNull()
        {
            var dict = new BinaryTreeDictionary<int, string>();
            dict[0] = null;
            Assert.AreEqual(true, dict.Contains(new KeyValuePair<int, string>(0, null)));
        }
        [Test]
        public void ContainsTestNotExistItem()
        {
            var dict = new BinaryTreeDictionary<int, int>();
            Assert.AreEqual(false, dict.Contains(new KeyValuePair<int, int>(0, 0)));
        }
        [Test]
        public void CopyToTest()
        {
            var testedDic = new BinaryTreeDictionary<string, int>();
            testedDic["1"] = 1;
            testedDic["2"] = 2;
            KeyValuePair<string, int>[] value = new KeyValuePair<string, int>[2];
            testedDic.CopyTo(value, 0);
            KeyValuePair<string, int>[] assertValue = new[]
            {
                new KeyValuePair<string, int>("1", 1),
                new KeyValuePair<string, int>("2", 2)
            };
            Assert.AreEqual(assertValue, value);
        }
}