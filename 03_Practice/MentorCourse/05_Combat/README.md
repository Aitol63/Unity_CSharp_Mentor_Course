# Бой

Сборка цели: объект с коллайдером, `Health` и, при необходимости, `DestroyOnDeath` или `KnockbackReceiver`.

Сборка оружия: создай префаб снаряда с `Projectile`, затем объект с `ProjectileWeapon`, назначь префаб и `Muzzle`. Вызывай `TryFire()` из кода игрока.

`Health`, `HitscanWeapon` и `MeleeAttack` не зависят друг от друга: связь идёт через `IDamageable`.

Коллайдер должен быть на том же объекте, что и `Health`. Иначе `TryGetComponent` цель не найдёт, и нужно использовать `GetComponentInParent`, как в `MeleeAttack`.
