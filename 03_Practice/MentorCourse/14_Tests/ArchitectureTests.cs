using MentorCourse.AI;
using MentorCourse.Architecture;
using MentorCourse.Basics;
using MentorCourse.SaveLoad;
using MentorCourse.Spawn;
using MentorCourse.UI;
using NUnit.Framework;

namespace MentorCourse.Tests
{
    public class ArchitectureTests
    {
        private sealed class FakeListener : IPurchaseListener
        {
            public string Last;
            public void OnPurchased(string itemId) => Last = itemId;
        }

        private sealed class FakeState : IState
        {
            public int Entered;
            public int Exited;
            public void Enter() => Entered++;
            public void Tick() { }
            public void Exit() => Exited++;
        }

        [Test]
        public void DamageCalculator_AppliesModifiersInOrder()
        {
            var calculator = new DamageCalculator();
            calculator.Add(new FlatArmor(10));
            calculator.Add(new PercentShield(0.5f));

            Assert.AreEqual(20, calculator.Calculate(50));
        }

        [Test]
        public void Shop_NotEnoughGold_DoesNotNotify()
        {
            var listener = new FakeListener();
            var shop = new Shop(new Wallet(5), listener);

            Assert.IsFalse(shop.TryBuy("sword", 10));
            Assert.IsNull(listener.Last);
        }

        [Test]
        public void StateMachine_SameState_DoesNotReenter()
        {
            var machine = new StateMachine();
            var state = new FakeState();

            machine.Change(state);
            machine.Change(state);

            Assert.AreEqual(1, state.Entered);
            Assert.AreEqual(0, state.Exited);
        }

        [Test]
        public void BehaviourTree_SelectorStopsAtFirstSuccess()
        {
            int calls = 0;
            var tree = new BtSelector(
                new BtCondition(() => false),
                new BtAction(() => { calls++; return NodeStatus.Success; }),
                new BtAction(() => { calls++; return NodeStatus.Success; }));

            Assert.AreEqual(NodeStatus.Success, tree.Tick());
            Assert.AreEqual(1, calls);
        }

        [Test]
        public void WeightedTable_PicksByWeight()
        {
            var table = new WeightedTable<string>();
            table.Add("common", 3f);
            table.Add("rare", 1f);

            Assert.AreEqual("common", table.Pick(0.5f));
            Assert.AreEqual("rare", table.Pick(0.9f));
        }

        [Test]
        public void SaveMigrator_Version1_ScalesGold()
        {
            SaveData data = SaveMigrator.Migrate(new SaveData { Version = 1, Gold = 7 });

            Assert.AreEqual(70, data.Gold);
            Assert.AreEqual(2, data.Version);
        }

        [Test]
        public void CounterModel_StopsAtMax()
        {
            var model = new CounterModel(1);

            model.Increment();
            model.Increment();

            Assert.AreEqual(1, model.Value);
        }

        [Test]
        public void CommandHistory_UndoAndRedo()
        {
            var history = new CommandHistory();
            var value = new CounterCommand();

            history.Execute(value);
            Assert.AreEqual(1, value.Count);

            history.Undo();
            Assert.AreEqual(0, value.Count);

            history.Redo();
            Assert.AreEqual(1, value.Count);
        }

        private sealed class CounterCommand : ICommand
        {
            public int Count;
            public void Execute() => Count++;
            public void Undo() => Count--;
        }
    }
}
