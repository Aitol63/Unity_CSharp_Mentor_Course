using UnityEngine;

namespace MentorCourse.PhysicsPractice
{
    public interface IInteractable
    {
        string Prompt { get; }
        void Interact(GameObject actor);
    }
}
