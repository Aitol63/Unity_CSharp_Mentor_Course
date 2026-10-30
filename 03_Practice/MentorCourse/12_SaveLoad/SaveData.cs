using System;
using System.Collections.Generic;

namespace MentorCourse.SaveLoad
{
    /// <summary>Сохраняем только данные и ID, а не ссылки на объекты Unity.</summary>
    [Serializable]
    public class SaveData
    {
        public const int CurrentVersion = 2;

        // 0 означает, что сохранения ещё не было.
        public int Version;
        public int Gold;
        public int Level = 1;
        public float PlayMinutes;
        public List<string> Unlocked = new();
    }

    public static class SaveMigrator
    {
        public static SaveData Migrate(SaveData data)
        {
            if (data.Version == 1)
            {
                // Пример: в версии 2 экономика стала в 10 раз крупнее.
                data.Gold *= 10;
                data.Version = 2;
            }

            return data;
        }
    }
}
