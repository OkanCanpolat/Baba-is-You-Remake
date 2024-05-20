public interface IIntersectResponse
{
    public int Priority { get; }
    public bool GetIntersectResponse(Element element);
}
