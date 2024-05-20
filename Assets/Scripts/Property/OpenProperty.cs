using UnityEngine;
public class OpenProperty : IProperty
{
    public void OnEnter(Element element)
    {
        ShutProperty shutProperty = element.GetProperty<ShutProperty>();

        if (shutProperty != null)
        {
            Object.Destroy(element.gameObject);
        }
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
