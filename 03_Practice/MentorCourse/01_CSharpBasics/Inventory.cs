using System;
using System.Collections.Generic;

namespace MentorCourse.Basics
{
    public sealed class ItemDefinition
    {
        public string Id { get; }
        public int MaxStack { get; }

        public ItemDefinition(string id, int maxStack = 1)
        {
            Id = id;
            MaxStack = Math.Max(1, maxStack);
        }
    }

    public sealed class ItemStack
    {
        public ItemDefinition Item { get; }
        public int Count { get; internal set; }

        public ItemStack(ItemDefinition item, int count)
        {
            Item = item;
            Count = count;
        }
    }

    /// <summary>
    /// Инвентарь со стаками. Наружу отдаётся IReadOnlyList: читать можно, менять в обход правил нельзя.
    /// </summary>
    public class Inventory
    {
        private readonly List<ItemStack> _slots;

        public int Capacity { get; }
        public IReadOnlyList<ItemStack> Slots => _slots;

        public event Action Changed;

        public Inventory(int capacity)
        {
            Capacity = capacity;
            _slots = new List<ItemStack>(capacity);
        }

        /// <summary>Возвращает количество, которое не поместилось.</summary>
        public int Add(ItemDefinition item, int count)
        {
            int left = count;

            for (int i = 0; i < _slots.Count && left > 0; i++)
            {
                ItemStack slot = _slots[i];
                if (slot.Item != item) continue;

                int moved = Math.Min(item.MaxStack - slot.Count, left);
                slot.Count += moved;
                left -= moved;
            }

            while (left > 0 && _slots.Count < Capacity)
            {
                int moved = Math.Min(item.MaxStack, left);
                _slots.Add(new ItemStack(item, moved));
                left -= moved;
            }

            if (left != count) Changed?.Invoke();
            return left;
        }

        public int CountOf(ItemDefinition item)
        {
            int total = 0;
            foreach (ItemStack slot in _slots)
                if (slot.Item == item) total += slot.Count;
            return total;
        }

        public bool TryRemove(ItemDefinition item, int count)
        {
            if (CountOf(item) < count) return false;

            // Обратный for: удаляем элементы прямо во время обхода.
            for (int i = _slots.Count - 1; i >= 0 && count > 0; i--)
            {
                ItemStack slot = _slots[i];
                if (slot.Item != item) continue;

                int removed = Math.Min(slot.Count, count);
                slot.Count -= removed;
                count -= removed;
                if (slot.Count == 0) _slots.RemoveAt(i);
            }

            Changed?.Invoke();
            return true;
        }
    }
}
