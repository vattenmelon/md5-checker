/*
 * Created by SharpDevelop.
 * User: Erling
 * Date: 21.01.2012
 * Time: 23:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using System.IO;

namespace HashSumGenerator
{
    [TestFixture]
    public class HashUtilTest
    {
        [Test]
        public void TestMethod()
        {
            FileStream f = File.OpenRead("HashSumGenerator.exe");
            string md5 = HashUtil.Hash(HashUtil.Algorithm.MD5, f);
            string sha256 = HashUtil.Hash(HashUtil.Algorithm.SHA256, f);
            Assert.AreNotEqual(md5, sha256);
        }
        
        [TestFixtureSetUp]
        public void Init()
        {
            // TODO: Add Init code.
        }
        
        [TestFixtureTearDown]
        public void Dispose()
        {
            // TODO: Add tear down code.
        }
    }
}
