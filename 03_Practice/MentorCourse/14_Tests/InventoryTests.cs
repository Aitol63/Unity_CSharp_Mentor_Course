using MentorCourse.Basics;
using NUnit.Framework;

namespace MentorCourse.Tests
{
    public class InventoryTests
    {
        private static readonly ItemDefinition Potion = new("potion", 5);

        [Test]
        public void Add_FillsExistingStackFirst()
        {
            var inventory = new Inventory(4);

            inventory.Add(Potion, 3);
            inventory.Add(Potion, 2);

            Assert.AreEqual(1, inventory.Slots.Count);
            Assert.AreEqual(5, inventory.CountOf(Potion));
        }

        [Test]
        public void Add_WhenFull_ReturnsLeftover()
        {
            var inventory = new Inventory(1);

            int left = inventory.Add(Potion, 7);

            Assert.AreEqual(2, left);
        }

        [Test]
        public void TryRemove_RemovesEmptySlots()
        {
            var inventory = new Inventory(4);
            inventory.Add(Potion, 7);

            Assert.IsTrue(inventory.TryRemove(Potion, 6));
            Assert.AreEqual(1, inventory.Slots.Count);
            Assert.AreEqual(1, inventory.CountOf(Potion));
        }

        [Test]
        public void TryRemove_NotEnough_ChangesNothing()
        {
            var inventory = new Inventory(4);
            inventory.Add(Potion, 2);

            Assert.IsFalse(inventory.TryRemove(Potion, 3));
            Assert.AreEqual(2, inventory.CountOf(Potion));
        }
    }
}
