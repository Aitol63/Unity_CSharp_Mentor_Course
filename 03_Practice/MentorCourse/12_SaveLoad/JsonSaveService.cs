using System;
using System.IO;
using UnityEngine;

namespace MentorCourse.SaveLoad
{
    public interface ISaveService
    {
        SaveData Load();
        void Save(SaveData data);
    }

    /// <summary>
    /// Запись через временный файл: вылет в момент сохранения не испортит старые данные.
    /// Повреждённый файл не роняет игру: он переименовывается для диагностики.
    /// </summary>
    public class JsonSaveService : ISaveService
    {
        private readonly string _path;

        public JsonSaveService(string fileName = "save.json")
            : this(Application.persistentDataPath, fileName) { }

        public JsonSaveService(string folder, string fileName)
        {
            _path = Path.Combine(folder, fileName);
        }

        public void Save(SaveData data)
        {
            data.Version = SaveData.CurrentVersion;

            string temp = _path + ".tmp";
            File.WriteAllText(temp, JsonUtility.ToJson(data, true));

            if (File.Exists(_path)) File.Replace(temp, _path, null);
            else File.Move(temp, _path);
        }

        public SaveData Load()
        {
            if (!File.Exists(_path)) return new SaveData();

            try
            {
                var data = JsonUtility.FromJson<SaveData>(File.ReadAllText(_path));
                return data == null ? new SaveData() : SaveMigrator.Migrate(data);
            }
            catch (Exception exception) when (exception is ArgumentException || exception is IOException)
            {
                Debug.LogException(exception);
                File.Copy(_path, _path + ".corrupted", true);
                return new SaveData();
            }
        }
    }
}
