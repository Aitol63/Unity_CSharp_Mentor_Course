using UnityEngine;

namespace MentorCourse.Performance
{
    /// <summary>
    /// renderer.material создаёт копию материала. Блок свойств меняет цвет без копии,
    /// но с SRP Batcher может ухудшить батчинг: проверяй Frame Debugger.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class ColorTint : MonoBehaviour
    {
        private static readonly int ColorId = Shader.PropertyToID("_BaseColor");

        private Renderer _renderer;
        private MaterialPropertyBlock _block;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _block = new MaterialPropertyBlock();
        }

        public void SetColor(Color color)
        {
            _renderer.GetPropertyBlock(_block);
            _block.SetColor(ColorId, color);
            _renderer.SetPropertyBlock(_block);
        }
    }
}
