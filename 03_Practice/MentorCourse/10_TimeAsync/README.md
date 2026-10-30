# Время и асинхронность

- `CoroutineExamples`: ожидания, кэш `WaitForSeconds`, остановка копии.
- `AwaitableExamples`: отмена через `destroyCancellationToken` и работа в фоне.
- `SlowMotion`: совместное изменение `timeScale` и `fixedDeltaTime`.

**Разбери:** что случится с ожиданием, если объект уничтожить без токена отмены?
