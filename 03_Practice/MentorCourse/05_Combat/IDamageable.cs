namespace MentorCourse.Combat
{
    /// <summary>Пуля не знает, кто цель: игрок, враг или бочка.</summary>
    public interface IDamageable
    {
        void TakeDamage(in DamageInfo info);
    }
}
