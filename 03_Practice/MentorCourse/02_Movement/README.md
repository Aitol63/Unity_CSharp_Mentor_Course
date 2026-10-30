# Движение

Все варианты получают команды через `IMoveInput`, поэтому ввод легко заменить.

| Файл | Когда использовать | Что нужно на объекте |
|---|---|---|
| `TransformMover` | Без физики | `LegacyInputReader` |
| `RigidbodyMover` | Физический персонаж | `Rigidbody`, `Collider`, ввод |
| `CharacterMover` | Персонаж без сил | `CharacterController`, ввод |
| `Platformer2DMover` | 2D-платформер | `Rigidbody2D`, `Collider2D`, ввод, точка проверки земли |
| `DashAbility` | Рывок | `Rigidbody`, ввод |

У `RigidbodyMover` исключи слой персонажа из `Ground Mask`, иначе он будет считать землёй себя.

**Разбери:** почему нажатие сохраняется в поле, а не читается в `FixedUpdate`?
