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
        private string _testFolderPath;
        private string _filename;

        [TestFixtureSetUp]
        public void TestInitialize()
        {
            var fileWriter = new FolderCreator();
            var serializeClient = new BinarySerializeClient();
            _clientWrapper = new SerializeClientWrapper(serializeClient, fileWriter);

            _clientWrapper.SerializedFilePath = @"D:\test2.bin";
            _testFolderPath = @"D:\Test";
            _filename = Path.Combine(_testFolderPath, "test.txt");

            CreateFolder(_testFolderPath);
            CreateFile(_filename);
        }

        [TestFixtureTearDown]
        public void TestDestroy()
        {
            DirectoryInfo di = new DirectoryInfo(_testFolderPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

        }

        [Test]
        public void ShouldSerializeFileTest()
        {
            // arrange
            var file = new FileInfo(_filename);

            // act
            _clientWrapper.SerializeFile(file);

            // assert
            Assert.IsTrue(File.Exists(_clientWrapper.SerializedFilePath));
        }

        [Test]
        public void ShouldDeserializeFileTest()
        {
            // arrange
            var outPath = @"D:\";
            var file = new FileInfo(_filename);

            // act
            _clientWrapper.SerializeFile(file);
            var fullName = _clientWrapper.DeserializeFile(outPath);

            // assert
            Assert.IsTrue(File.Exists(fullName));
        }

        [Test]
        public void ShouldSerializeFolderTest()
        {
            // arrange
            
            // act
            _clientWrapper.SerializeFolder(_testFolderPath);

            // assert
            Assert.IsTrue(File.Exists(_clientWrapper.SerializedFilePath));
        }

        [Test]
        public void ShouldDeserializeFolderTest()
        {
            // arrange
            var folderOutPath = @"D:\";
            var deserializeFolder = Path.Combine(@"D:\", _testFolderPath);

            // act
            _clientWrapper.SerializeFolder(_testFolderPath);
            _clientWrapper.DeserializeFolderModel(_clientWrapper.SerializedFilePath);

            // assert
            Assert.IsTrue(Directory.Exists(deserializeFolder));
            Assert.IsNotEmpty(Directory.GetDirectories(folderOutPath));
        }

        private void CreateFolder(string folderPath)
        {
            var subFolder = folderPath;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private void CreateFile(string filename)
        {
            using (TextWriter tw = new StreamWriter(filename))
            {
                tw.WriteLine("The line!");
                tw.Close();
            }
        }
    }
}
