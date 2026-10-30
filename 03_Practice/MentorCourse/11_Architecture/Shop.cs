using System;
using MentorCourse.Basics;

namespace MentorCourse.Architecture
{
    public interface IPurchaseListener
    {
        void OnPurchased(string itemId);
    }

    /// <summary>
    /// Зависимости приходят через конструктор. Магазин не ищет кошелёк по сцене,
    /// поэтому в тесте легко подставить свои объекты.
    /// </summary>
    public sealed class Shop
    {
        private readonly Wallet _wallet;
        private readonly IPurchaseListener _listener;

        public Shop(Wallet wallet, IPurchaseListener listener)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _listener = listener ?? throw new ArgumentNullException(nameof(listener));
        }

        public bool TryBuy(string itemId, int price)
        {
            if (!_wallet.TrySpend(price)) return false;

            _listener.OnPurchased(itemId);
            return true;
        }
    }
}
