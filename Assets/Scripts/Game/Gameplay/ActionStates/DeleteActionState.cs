using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Engine.Core;

namespace Game.Gameplay.ActionStates
{
    public class DeleteActionState : ActionStateBase
    {
        public override async UniTask Render(ActorsFactory actorsFactory, IBoard board, IState state)
        {
            var deleteAction = state.Action as IDeleteAction;
            var deletedElements = deleteAction.DeleteElements;
            var tasks = new List<UniTask>();

            if (deletedElements.Count > 0)
            {
                await UniTask.WaitWhile(actorsFactory.AnyElementUseGravity);
            }

            for (var i = 0; i < deletedElements.Count; i++)
            {
                var deletedElement = deletedElements[i];
                var deletedElementActor = actorsFactory.GetElementActor(deletedElement.Id);
                var task = deletedElementActor.RenderDelete().ContinueWith(() => actorsFactory.DeleteElementActor(deletedElementActor));
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
