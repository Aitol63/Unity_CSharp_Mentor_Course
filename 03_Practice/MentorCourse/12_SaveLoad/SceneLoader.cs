using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MentorCourse.SaveLoad
{
    /// <summary>Асинхронная загрузка с прогрессом. Сцена должна быть в списке сцен билда.</summary>
    public class SceneLoader : MonoBehaviour
    {
        public event Action<float> ProgressChanged;

        public bool IsLoading { get; private set; }

        public async void Load(string sceneName)
        {
            if (IsLoading) return;
            IsLoading = true;

            try
            {
                await LoadAsync(sceneName);
            }
            catch (OperationCanceledException)
            {
                // Загрузчик уничтожен, ждать больше некому.
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Awaitable LoadAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            // При запрете активации прогресс останавливается на 0.9.
            while (operation.progress < 0.9f)
            {
                ProgressChanged?.Invoke(operation.progress / 0.9f);
                await Awaitable.NextFrameAsync(destroyCancellationToken);
            }

            ProgressChanged?.Invoke(1f);
            operation.allowSceneActivation = true;
        }
    }
}
