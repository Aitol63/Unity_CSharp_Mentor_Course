using System;
using System.IO;
using MentorCourse.SaveLoad;
using NUnit.Framework;

namespace MentorCourse.Tests
{
    public class JsonSaveServiceTests
    {
        private string _folder;

        [SetUp]
        public void SetUp()
        {
            _folder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_folder);
        }

        [TearDown]
        public void TearDown() => Directory.Delete(_folder, true);

        [Test]
        public void SaveThenLoad_RestoresData()
        {
            var service = new JsonSaveService(_folder, "test.json");
            service.Save(new SaveData { Gold = 42, Level = 3 });
            service.Save(new SaveData { Gold = 43, Level = 4 });

            SaveData loaded = service.Load();

            Assert.AreEqual(43, loaded.Gold);
            Assert.AreEqual(4, loaded.Level);
            Assert.AreEqual(SaveData.CurrentVersion, loaded.Version);
        }

        [Test]
        public void Load_NoFile_ReturnsFreshData()
        {
            var service = new JsonSaveService(_folder, "missing.json");

            Assert.AreEqual(0, service.Load().Version);
        }
    }
}
