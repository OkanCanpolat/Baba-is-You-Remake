using UnityEngine;

public class StopProperty : IProperty, IMoveResponse
{
    private const int priority = 400;
    public int Priority => priority;

    public bool GetMoveResponse(Vector3Int direction, Element client)
    {
        return false;
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
