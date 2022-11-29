using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Engine.Core;

namespace Game.Gameplay.ActionStates
{
    public class FillActionState : ActionStateBase
    {
        public override async UniTask Render(ActorsFactory actorsFactory, IBoard board, IState state)
        {
            var field = state.Field;
            var tasks = new List<UniTask>();

            for (var i = 0; i < field.Elements.Count; i++)
            {
                var element = field.Elements[i];

                if (!actorsFactory.HasElementAt(element.Id))
                {
                    var elementActor = actorsFactory.CreateElementActor(element);
                    var task = elementActor.RenderAppearing(element.Point);
                    tasks.Add(task);
                }
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
