using System;
using System.Collections.Generic;

namespace MentorCourse.AI
{
    /// <summary>Utility AI: каждое действие получает оценку, побеждает наибольшая.</summary>
    public sealed class UtilitySelector
    {
        private readonly List<(string Name, Func<float> Score, Action Run)> _options = new();

        public void Add(string name, Func<float> score, Action run) => _options.Add((name, score, run));

        public string Execute()
        {
            float bestScore = 0f;
            int bestIndex = -1;

            for (int i = 0; i < _options.Count; i++)
            {
                float score = _options[i].Score();
                if (score <= bestScore) continue;

                bestScore = score;
                bestIndex = i;
            }

            if (bestIndex < 0) return null;

            _options[bestIndex].Run();
            return _options[bestIndex].Name;
        }
    }
}
