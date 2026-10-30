using System;

namespace MentorCourse.Basics
{
    /// <summary>
    /// Инкапсуляция: золото меняется только через методы, поэтому отрицательным не станет.
    /// Обычный C#-класс: не зависит от сцены и легко тестируется.
    /// </summary>
    public class Wallet
    {
        public int Gold { get; private set; }

        public event Action<int> Changed;

        public Wallet(int startGold = 0)
        {
            if (startGold < 0) throw new ArgumentOutOfRangeException(nameof(startGold));
            Gold = startGold;
        }

        public void Add(int amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            Gold += amount;
            Changed?.Invoke(Gold);
        }

        // Нехватка денег — обычный сценарий, а не ошибка, поэтому паттерн Try*.
        public bool TrySpend(int amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            if (Gold < amount) return false;

            Gold -= amount;
            Changed?.Invoke(Gold);
            return true;
        }
    }
}
