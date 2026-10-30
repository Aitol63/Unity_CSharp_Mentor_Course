using System.Collections.Generic;
using UnityEngine;

namespace MentorCourse.Architecture
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    /// <summary>Действие как объект: работает отмена, очередь и повтор.</summary>
    public sealed class CommandHistory
    {
        private readonly Stack<ICommand> _done = new();
        private readonly Stack<ICommand> _undone = new();

        public void Execute(ICommand command)
        {
            command.Execute();
            _done.Push(command);
            _undone.Clear();
        }

        public bool Undo()
        {
            if (_done.Count == 0) return false;

            ICommand command = _done.Pop();
            command.Undo();
            _undone.Push(command);
            return true;
        }

        public bool Redo()
        {
            if (_undone.Count == 0) return false;

            ICommand command = _undone.Pop();
            command.Execute();
            _done.Push(command);
            return true;
        }
    }

    public sealed class MoveCommand : ICommand
    {
        private readonly Transform _target;
        private readonly Vector3 _delta;

        public MoveCommand(Transform target, Vector3 delta)
        {
            _target = target;
            _delta = delta;
        }

        public void Execute() => _target.position += _delta;
        public void Undo() => _target.position -= _delta;
    }
}
