using MentorCourse.Basics;
using NUnit.Framework;

namespace MentorCourse.Tests
{
    public class WalletTests
    {
        [Test]
        public void TrySpend_NotEnoughGold_ReturnsFalseAndKeepsGold()
        {
            var wallet = new Wallet(5);

            bool result = wallet.TrySpend(10);

            Assert.IsFalse(result);
            Assert.AreEqual(5, wallet.Gold);
        }

        [Test]
        public void TrySpend_EnoughGold_DecreasesGoldAndNotifies()
        {
            var wallet = new Wallet(10);
            int notified = -1;
            wallet.Changed += gold => notified = gold;

            Assert.IsTrue(wallet.TrySpend(4));
            Assert.AreEqual(6, wallet.Gold);
            Assert.AreEqual(6, notified);
        }

        [Test]
        public void Add_NegativeAmount_Throws()
        {
            var wallet = new Wallet();

            Assert.Throws<System.ArgumentOutOfRangeException>(() => wallet.Add(-1));
        }
    }
}
