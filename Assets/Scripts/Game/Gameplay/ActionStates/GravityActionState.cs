using Cysharp.Threading.Tasks;
using Game.Engine.Core;

namespace Game.Gameplay.ActionStates
{
    public class GravityActionState : ActionStateBase
    {
        public override UniTask Render(ActorsFactory actorsFactory, IBoard board, IState state)
        {
            var field = state.Field;

            for (var i = 0; i < field.Elements.Count; i++)
            {
                var element = field.Elements[i];
                var elementActor = actorsFactory.GetElementActor(element.Id);

                if (elementActor == null)
                {
                    continue;
                }

                if (!element.Point.Equal(elementActor.Point))
                {
                    elementActor.RenderGravity(element.Point).Forget();
                }
            }

            return UniTask.CompletedTask;
        }
    }
}
