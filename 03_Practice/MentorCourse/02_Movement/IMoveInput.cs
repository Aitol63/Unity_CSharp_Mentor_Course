using UnityEngine;

namespace MentorCourse.Movement
{
    /// <summary>
    /// Движение не знает про клавиатуру. Команды может давать игрок, ИИ, сеть или тест.
    /// </summary>
    public interface IMoveInput
    {
        Vector2 Move { get; }
        bool JumpHeld { get; }

        /// <summary>Вернёт true один раз на каждое нажатие.</summary>
        bool ConsumeJump();

        bool ConsumeDash();
    }
}
