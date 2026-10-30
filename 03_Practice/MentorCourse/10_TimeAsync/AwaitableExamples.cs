using System;
using UnityEngine;

namespace MentorCourse.TimeAsync
{
    /// <summary>
    /// async/await в Unity 6. В отличие от корутин async сам не остановится,
    /// поэтому всегда передавай destroyCancellationToken.
    /// </summary>
    public class AwaitableExamples : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        // async void допустим только на границе, как здесь, и исключения надо ловить внутри.
        public async void FlashSafely()
        {
            try
            {
                await FlashAsync();
            }
            catch (OperationCanceledException)
            {
                // Объект уничтожен во время ожидания: это нормальный сценарий.
            }
        }

        public async Awaitable FlashAsync()
        {
            _renderer.enabled = false;
            await Awaitable.WaitForSecondsAsync(0.2f, destroyCancellationToken);
            _renderer.enabled = true;
        }

        /// <summary>Тяжёлые вычисления в фоне, результат применяется на главном потоке.</summary>
        public async Awaitable<long> SumAsync(int count)
        {
            await Awaitable.BackgroundThreadAsync();

            long sum = 0;
            for (int i = 0; i < count; i++) sum += i;

            await Awaitable.MainThreadAsync();
            destroyCancellationToken.ThrowIfCancellationRequested();

            Debug.Log($"Сумма: {sum}");
            return sum;
        }
    }
}
