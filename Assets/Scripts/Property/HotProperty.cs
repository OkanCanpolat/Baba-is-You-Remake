using UnityEngine;
public class HotProperty : IProperty, IIntersectResponse
{
    private const int intersectPriority = 725;
    public int Priority => intersectPriority;
   
    public bool GetIntersectResponse(Element element)
    {
        MeltProperty meltProperty = element.GetProperty<MeltProperty>();

        if (meltProperty != null)
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
        MeltProperty meltProperty = element.GetProperty<MeltProperty>();

        if (meltProperty != null)
        {
            Object.Destroy(element.gameObject);
        }
    }
    public void OnExit()
    {
    }
}
