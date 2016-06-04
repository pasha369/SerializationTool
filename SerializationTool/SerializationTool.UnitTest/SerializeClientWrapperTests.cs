using System.IO;
using NUnit.Framework;
using SerializationClient;
using SerializationClient.Core.FIleWriter;
using SerializationClient.Core.SerializeClients;

namespace SerializationTool.UnitTest
{
    [TestFixture]
    public class SerializeClientWrapperTests
    {
        private SerializeClientWrapper _clientWrapper;

        [TestFixtureSetUp]
        public void TestInitialize()
        {
            var fileWriter = new FileWriter();
            var serializeClient = new BinarySerializeClient();
            _clientWrapper = new SerializeClientWrapper(serializeClient, fileWriter);

            _clientWrapper.SerializedFilePath = @"D:\test2.bin";
        }

        [Test]
        public void ShouldSerializeFileTest()
        {
            // arrange
            var file = new FileInfo(@"C:\Users\pasha\Pictures\trir_by_location.png");

            // act
            _clientWrapper.SerializeFile(file);

            // assert
            Assert.IsTrue(File.Exists(_clientWrapper.SerializedFilePath));
        }

        [Test]
        public void ShouldDeserializeFileTest()
        {
            // arrange
            var filePath = @"D:\";
            var file = new FileInfo(@"C:\Users\pasha\Pictures\trir_by_location.png");

            // act
            _clientWrapper.SerializeFile(file);
            var fullName = _clientWrapper.DeserializeFile(filePath);

            // assert
            Assert.IsTrue(File.Exists(fullName));
        }

        [Test]
        public void ShouldSerializeFolderTest()
        {
            // arrange
            var folderPath = @"C:\Users\Public\Pictures\Sample Pictures\";
            
            // act
            _clientWrapper.SerializeFolder(folderPath);

            // assert
            Assert.IsTrue(File.Exists(_clientWrapper.SerializedFilePath));
        }

        [Test]
        public void ShouldDeserializeFolderTest()
        {
            // arrange
            var folderOutPath = @"D:\";
            var folderPath = @"C:\Users\Public\Pictures\Sample Pictures\";

            // act
            _clientWrapper.SerializeFolder(folderPath);
            _clientWrapper.DeserializeFolderModel(_clientWrapper.SerializedFilePath);

            // assert
            Assert.IsTrue(Directory.Exists(folderOutPath));
            Assert.IsNotEmpty(Directory.GetDirectories(folderOutPath));
        }
    }
}
