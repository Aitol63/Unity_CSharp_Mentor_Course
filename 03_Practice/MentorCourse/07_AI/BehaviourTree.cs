using System;

namespace MentorCourse.AI
{
    public enum NodeStatus { Success, Failure, Running }

    public abstract class BtNode
    {
        public abstract NodeStatus Tick();
    }

    /// <summary>Пробует детей по порядку до первого успеха: "или".</summary>
    public sealed class BtSelector : BtNode
    {
        private readonly BtNode[] _children;

        public BtSelector(params BtNode[] children) => _children = children;

        public override NodeStatus Tick()
        {
            foreach (BtNode child in _children)
            {
                NodeStatus status = child.Tick();
                if (status != NodeStatus.Failure) return status;
            }

            return NodeStatus.Failure;
        }
    }

    /// <summary>Выполняет детей по порядку до первой неудачи: "и".</summary>
    public sealed class BtSequence : BtNode
    {
        private readonly BtNode[] _children;

        public BtSequence(params BtNode[] children) => _children = children;

        public override NodeStatus Tick()
        {
            foreach (BtNode child in _children)
            {
                NodeStatus status = child.Tick();
                if (status != NodeStatus.Success) return status;
            }

            return NodeStatus.Success;
        }
    }

    public sealed class BtCondition : BtNode
    {
        private readonly Func<bool> _check;

        public BtCondition(Func<bool> check) => _check = check;

        public override NodeStatus Tick() => _check() ? NodeStatus.Success : NodeStatus.Failure;
    }

    public sealed class BtAction : BtNode
    {
        private readonly Func<NodeStatus> _action;

        public BtAction(Func<NodeStatus> action) => _action = action;

        public override NodeStatus Tick() => _action();
    }
}
