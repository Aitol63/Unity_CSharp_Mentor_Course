# Анимация, звук, эффекты

- `CharacterAnimator`: создай в Animator параметры `Speed`, `Grounded`, `Attack`.
- `AnimationEventRelay`: положи рядом с Animator. Событие в клипе вызывает `AnimEvent_Hit`.
- `AudioService`: пул 3D-источников.
- `VolumeSettings`: открой параметр миксера через **Expose** и назови его `SfxVolume`.
- `VfxPlayer`, `PooledParticle`: у частиц поставь **Stop Action = Callback** и выключи **Looping**.
- `HitStop`: вызывай `Play()` при сильном ударе.
