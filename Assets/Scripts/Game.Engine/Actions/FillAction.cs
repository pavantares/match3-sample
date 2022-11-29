using System.Collections.Generic;
using Game.Engine.Core;
using Game.Engine.Core.Elements;
using Game.Engine.Utilities;
using Game.Utilities;

namespace Game.Engine.Actions
{
    public class FillAction : IFillAction
    {
        private readonly IMemory memory;

        public FillAction(IMemory memory)
        {
            this.memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var boardSize = memory.Board.Size;
            var previousField = previousState.Field.Copy();
            var field = (IField)new Field(new List<IElement>(boardSize.x * boardSize.y));
            var maxRow = boardSize.x;
            var rowWithNewElements = -1;
            var newElementsInRow = new List<IElement>();
            var createElementMethods = memory.ElementsFactory.GetCreateElementMethods();

            for (var row = 0; row < boardSize.x; row++)
            {
                for (var column = 0; column < boardSize.y; column++)
                {
                    var point = new Point(row, column);
                    var element = previousField.GetElementAt(point);

                    if (element != null)
                    {
                        field.AddOrReplaceElementByPoint(element.Copy());

                        continue;
                    }

                    if (rowWithNewElements != -1 && rowWithNewElements != row)
                    {
                        continue;
                    }

                    rowWithNewElements = row;

                    createElementMethods.Shuffle();

                    for (var i = 0; i < createElementMethods.Count; i++)
                    {
                        var newElement = createElementMethods[i](point);
                        field.AddOrReplaceElementByPoint(newElement);

                        if (!MatchEngineUtility.HasMatch(field, point))
                        {
                            newElementsInRow.Add(newElement);

                            break;
                        }

                        field.RemoveElement(newElement);
                    }
                }
            }

            newElementsInRow.ForEach(x => x.Point = new Point(maxRow, x.Point.Column));

            return memory.StatesFactory.CreateState(field, this, previousState);
        }
    }
}
