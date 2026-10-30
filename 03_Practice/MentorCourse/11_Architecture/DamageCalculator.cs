using System;
using System.Collections.Generic;

namespace MentorCourse.Architecture
{
    public interface IDamageModifier
    {
        int Apply(int damage);
    }

    public sealed class FlatArmor : IDamageModifier
    {
        private readonly int _value;

        public FlatArmor(int value) => _value = value;

        public int Apply(int damage) => Math.Max(0, damage - _value);
    }

    public sealed class PercentShield : IDamageModifier
    {
        private readonly float _reduction;

        public PercentShield(float reduction) => _reduction = reduction;

        public int Apply(int damage) => (int)Math.Round(damage * (1f - _reduction));
    }

    /// <summary>Новый эффект добавляется новым классом, калькулятор не меняется.</summary>
    public sealed class DamageCalculator
    {
        private readonly List<IDamageModifier> _modifiers = new();

        public void Add(IDamageModifier modifier) => _modifiers.Add(modifier);
        public bool Remove(IDamageModifier modifier) => _modifiers.Remove(modifier);

        public int Calculate(int damage)
        {
            foreach (IDamageModifier modifier in _modifiers)
                damage = modifier.Apply(damage);
            return damage;
        }
    }
}
