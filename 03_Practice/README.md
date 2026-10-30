# Практика

Эталонные скрипты, разложенные по темам. Это не задания, а образцы для сравнения и разбора.

## Подключение к проекту

1. Создай проект на шаблоне **Universal 3D**.
2. Скопируй папку `MentorCourse` целиком в `Assets`.
3. Открой **Edit → Project Settings → Player → Active Input Handling** и выбери **Both**. Unity перезапустится.
4. Для главы про ИИ установи пакет **AI Navigation** в Package Manager.
5. Создай тег `Player` и поставь его на персонажа.

Нужны пакеты `com.unity.ugui` и `com.unity.test-framework`. Они уже есть в стандартных шаблонах. Если создаёшь пустой проект, добавь их через Package Manager.

Пакет Input System необязателен: `InputSystemReader.cs` компилируется только при его наличии.

## Модули

| Папка | Тема | Главы лекций |
|---|---|---|
| `01_CSharpBasics` | Классы, generics, расширения | 1–4 |
| `02_Movement` | Ввод, Transform, Rigidbody, CharacterController, 2D | 7 |
| `03_Camera` | Следование, FPS, орбита, тряска | 6, 7 |
| `04_Physics` | Триггеры, рейкасты, взрывы, платформы | 8 |
| `05_Combat` | Здоровье, урон, оружие | 2, 8 |
| `06_Spawn` | Пулы, волны, таблицы лута | 4, 12, 13 |
| `07_AI` | FSM, зрение, NavMesh, BT, Utility | 12 |
| `08_UI` | HUD, MVP, экраны, пауза | 10 |
| `09_AnimationAudio` | Animator, звук, частицы, hit stop | 11 |
| `10_TimeAsync` | Корутины, Awaitable, slow motion | 9 |
| `11_Architecture` | Команды, шина, DI, способности | 13 |
| `12_SaveLoad` | JSON, миграции, сцены, настройки | 14 |
| `13_Performance` | Менеджер обновления, профилирование | 15 |
| `14_Tests` | NUnit-тесты логики | 15 |

В каждой папке есть свой `README.md` со схемой сборки сцены.

## Запуск тестов

**Window → General → Test Runner → EditMode → Run All**.

## Быстрый старт: монеты

1. Создай Plane и Capsule. На Capsule поставь тег `Player`.
2. Добавь Capsule компоненты `Rigidbody` (Freeze Rotation X, Y, Z), `LegacyInputReader`, `RigidbodyMover`.
3. Создай префаб Sphere с `Coin`. Коллайдер станет триггером автоматически.
4. Создай пустой объект с `CoinSpawner` и назначь префаб монеты.
5. Запусти Play.
