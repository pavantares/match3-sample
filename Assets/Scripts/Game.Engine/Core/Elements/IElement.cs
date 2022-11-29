namespace Game.Engine.Core.Elements
{
    public interface IElement
    {
        string Id { get; }
        Point Point { get; set; }
        IElement Copy();
    }
}
