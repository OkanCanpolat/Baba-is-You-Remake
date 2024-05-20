using UnityEngine;
using Zenject;

public class MoveProperty : IProperty
{
    private Element element;
    private SignalBus signalBus;
    private GridSystem gridSystem;
    private MovementController movementController;
    private int lastFrameCount = -1;
    private MovemenetSignal movemenetSignal;

    public MoveProperty(Element element, SignalBus signalBus, GridSystem gridSystem, MovementController movementController)
    {
        this.signalBus = signalBus;
        this.gridSystem = gridSystem;
        this.movementController = movementController;
        this.element = element;
        movemenetSignal = new MovemenetSignal(element);
    }
    public void OnUpdate()
    {
    }
    public void OnEnter(Element element)
    {
        signalBus.Subscribe<TurnEndSignal>(OnMoveSignal);
    }
    public void OnExit()
    {
        // Debug.Log("MOVE EXIT");
        signalBus.TryUnsubscribe<TurnEndSignal>(OnMoveSignal);
    }

    private void OnMoveSignal()
    {
        int frameCount = Time.frameCount;

        if (frameCount == lastFrameCount) return;

        Vector3Int direction = GetDirection();
        lastFrameCount = frameCount;
        // Debug.Log("MOVE ENTER");

        Vector3Int result = new Vector3Int(element.Column, element.Row) + direction;

        if (!gridSystem.IsInsideGrid(result.x, result.y))
        {
            ReverseDirection();
            direction = GetDirection();
            result = new Vector3Int(element.Column, element.Row) + direction;
        }

        Cell targetCell = gridSystem.GetCell(result.x, result.y);

        if (TryMove(targetCell, direction))
        {
            return;
        }

        else
        {
            ReverseDirection();
            direction = GetDirection();
            result = new Vector3Int(element.Column, element.Row) + direction;
            targetCell = gridSystem.GetCell(result.x, result.y);
            TryMove(targetCell, direction);
        }
    }

    private void ReverseDirection()
    {
        switch (element.FacingDirection)
        {
            case FacingDirection.Up:
                element.FacingDirection = FacingDirection.Down;
                break;
            case FacingDirection.Down:
                element.FacingDirection = FacingDirection.Up;
                break;
            case FacingDirection.Right:
                element.FacingDirection = FacingDirection.Left;
                break;
            case FacingDirection.Left:
                element.FacingDirection = FacingDirection.Right;
                break;
        }
    }
    private Vector3Int GetDirection()
    {
        switch (element.FacingDirection)
        {
            case FacingDirection.Up:
                return Vector3Int.up;
            case FacingDirection.Down:
                return Vector3Int.down;
            case FacingDirection.Right:
                return Vector3Int.right;
            case FacingDirection.Left:
                return Vector3Int.left;
        }

        return default;
    }
    private bool TryMove(Cell targetCell, Vector3Int direction)
    {
        for (int i = targetCell.ElementCount - 1; i >= 0; i--)
        {
            if (!targetCell.Elements[i].MoveResponse(direction, element))
            {
                return false;
            }
        }

        movementController.MoveableElements.Add(element);
        movementController.StartMovement(direction);
        movemenetSignal.Direction = direction;
        signalBus.TryFire(movemenetSignal);
        return true;
    }
}
