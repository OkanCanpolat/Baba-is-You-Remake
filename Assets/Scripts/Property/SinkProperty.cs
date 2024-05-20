using UnityEngine;
using Zenject;
public class SinkProperty : IProperty, IIntersectResponse
{
    private Element element;
    private const int intersectPriority = 525;
    public SinkProperty(Element element)
    {
        this.element = element;
    }
    public int Priority => intersectPriority;

    public bool GetIntersectResponse(Element element)
    {
        Object.Destroy(element.gameObject);
        Object.Destroy(this.element.gameObject);
        return true;
    }

    public void OnUpdate()
    {
    }
    public void OnEnter(Element element)
    {
    }
    public void OnExit()
    {
    }
}
