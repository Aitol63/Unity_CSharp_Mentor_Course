# Архитектура

- `EventBus`: редкие глобальные события.
- `CommandHistory`: отмена и повтор.
- `DamageCalculator`: открытость для расширения.
- `Ability`, `AbilityEffect`, `DamageEffect`, `HealEffect`: способности из данных. Создай ассеты через **Create → Mentor Course → Abilities**.
- `Shop`: внедрение зависимостей через конструктор.
- `GameBootstrap`: точка сборки.

Прежде чем добавлять абстракцию, спроси: есть ли уже два разных варианта поведения?
