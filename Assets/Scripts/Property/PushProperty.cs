using UnityEngine;
using Zenject;

public class PushProperty : IProperty, IMoveResponse
{
    private Element element;
    private GridSystem gridSystem;
    private MovementController movementController;
    private const int priority = 450;
    private MovemenetSignal movemenetSignal;
    private SignalBus signalBus;
    public int Priority => priority;

    public PushProperty(Element element, GridSystem gridSystem, MovementController movementController, SignalBus signalBus)
    {
        this.element = element;
        this.gridSystem = gridSystem;
        this.movementController = movementController;
        this.signalBus = signalBus;
        movemenetSignal = new MovemenetSignal(element);
    }

    public bool GetMoveResponse(Vector3Int direction, Element client)
    {
        Vector3Int result = new Vector3Int(element.Column, element.Row) + direction;

        if (!gridSystem.IsInsideGrid(result.x, result.y)) return false;

        Cell targetCell = gridSystem.GetCell(result.x, result.y);
       
        for (int i = targetCell.ElementCount - 1; i >= 0; i--)
        {
            if (!targetCell.Elements[i].MoveResponse(direction, element))
            {
                return false;
            }
        }

        movementController.MoveableElements.Add(element);
        element.FacingDirection = FromVector2Direction(direction);
        movemenetSignal.Direction = direction;
        signalBus.TryFire(movemenetSignal);

        return true;
    }
    private FacingDirection FromVector2Direction(Vector3Int direction)
    {
        switch (direction)
        {
            case Vector3Int v when v.Equals(Vector3Int.right):
                return FacingDirection.Right;
            case Vector3Int v when v.Equals(Vector3Int.left):
                return FacingDirection.Left;
            case Vector3Int v when v.Equals(Vector3Int.up):
                return FacingDirection.Up;
            case Vector3Int v when v.Equals(Vector3Int.down):
                return FacingDirection.Down;
        }
        return default;
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
