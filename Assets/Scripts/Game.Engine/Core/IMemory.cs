using System.Collections.Generic;

namespace Game.Engine.Core
{
    public interface IMemory
    {
        IBoard Board { get; }
        ElementsFactory ElementsFactory { get; }
        ActionsFactory ActionsFactory { get; }
        StatesFactory StatesFactory { get; }
        List<IState> StateBySteps { get; }
        Point FromPoint { get; set; }
        Point ToPoint { get; set; }
    }
}
