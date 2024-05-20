using UnityEngine;

public class ShutProperty : IProperty, IMoveResponse
{
    private Element element;
    private const int priority = 425;

    public int Priority => priority;

    public ShutProperty(Element element)
    {
        this.element = element;
    }

    public bool GetMoveResponse(Vector3Int direction, Element client)
    {
        OpenProperty openProperty = client.GetProperty<OpenProperty>();

        if (openProperty != null)
        {
            Object.Destroy(client.gameObject);
            Object.Destroy(element.gameObject);
            return true;
        }

        return false;
    }

    public void OnEnter(Element element)
    {
        OpenProperty openProperty = element.GetProperty<OpenProperty>();

        if (openProperty != null)
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
