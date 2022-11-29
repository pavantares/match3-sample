using System.Collections.Generic;
using Game.Engine.Core.Elements;

namespace Game.Engine.Core
{
    public interface IDeleteAction : IAction
    {
        List<IElement> DeleteElements { get; }
    }
}
