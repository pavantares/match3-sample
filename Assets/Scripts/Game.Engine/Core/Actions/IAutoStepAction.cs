namespace Game.Engine.Core
{
    public interface IAutoStepAction : IAction
    {
        bool HasSuccessStep { get; }
    }
}
