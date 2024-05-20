using UnityEngine;
using Zenject;

public class DefeatProperty : IProperty, IIntersectResponse
{
    private const int intersectPriority = 750;
    public int Priority => intersectPriority;

    public bool GetIntersectResponse(Element element)
    {
        YouProperty youProperty = element.GetProperty<YouProperty>();

        if (youProperty != null)
        {
            Object.Destroy(element.gameObject);
            return true;
        }
        return false;
    }

    public void OnUpdate()
    {
    }
    public void OnEnter(Element element)
    {
        YouProperty youProperty = element.GetProperty<YouProperty>();

        if (youProperty != null)
        {
            Object.Destroy(element.gameObject);
        }
    }
    public void OnExit()
    {
    }
}
