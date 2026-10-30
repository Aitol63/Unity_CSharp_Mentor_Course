using MentorCourse.Basics;
using MentorCourse.SaveLoad;
using UnityEngine;

namespace MentorCourse.Architecture
{
    /// <summary>
    /// Composition Root: единственное место, где создаются и связываются сервисы.
    /// Порядок -100 запускает этот Awake раньше обычных скриптов.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class GameBootstrap : MonoBehaviour, IPurchaseListener
    {
        [SerializeField] private int _startGold = 100;

        private ISaveService _saves;
        private SaveData _data;

        public Wallet Wallet { get; private set; }
        public Shop Shop { get; private set; }

        private void Awake()
        {
            _saves = new JsonSaveService();
            _data = _saves.Load();

            Wallet = new Wallet(_data.Version == 0 ? _startGold : _data.Gold);
            Shop = new Shop(Wallet, this);
            Wallet.Changed += SaveGold;
        }

        private void OnDestroy() => Wallet.Changed -= SaveGold;

        public void OnPurchased(string itemId)
        {
            _data.Unlocked.Add(itemId);
            _saves.Save(_data);
        }

        private void SaveGold(int gold)
        {
            _data.Gold = gold;
            _saves.Save(_data);
        }
    }
}
