using UnityEngine;

public interface IMoveResponse
{
    public int Priority { get; }
    public bool GetMoveResponse(Vector3Int direction, Element client);
}
