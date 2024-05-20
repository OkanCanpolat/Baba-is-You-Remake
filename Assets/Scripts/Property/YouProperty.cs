using UnityEngine;
using Zenject;
public class YouProperty : IProperty
{
    private Element element;
    private GridSystem gridSystem;
    private MovementController movementController;
    private SignalBus signalBus;
    private MovemenetSignal movemenetSignal;
    public YouProperty(Element element, GridSystem gridSystem, MovementController movementController, SignalBus signalBus)
    {
        this.signalBus = signalBus;
        this.element = element;
        this.gridSystem = gridSystem;
        this.movementController = movementController;
        movemenetSignal = new MovemenetSignal(element);
    }
    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3Int direction = Vector3Int.right;
            Move(direction);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3Int direction = Vector3Int.left;
            Move(direction);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3Int direction = Vector3Int.up;
            Move(direction);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3Int direction = Vector3Int.down;
            Move(direction);
        }
    }

    private void Move(Vector3Int direction)
    {
        Vector3Int result = new Vector3Int(element.Column, element.Row) + direction;

        if (!gridSystem.IsInsideGrid(result.x, result.y)) return;

        Cell targetCell = gridSystem.GetCell(result.x, result.y);

        for (int i = targetCell.ElementCount - 1; i >= 0; i--)
        {
            if (!targetCell.Elements[i].MoveResponse(direction, element))
            {
                return;
            }
        }

        movementController.MoveableElements.Add(element);
        movementController.StartMovement(direction);

        movemenetSignal.Direction = direction;
        signalBus.TryFire(movemenetSignal);
    }
    public void OnEnter(Element element)
    {
        signalBus.TryFire<YouAddedSignal>();
    }
    public void OnExit()
    {
        signalBus.TryFire<YouDiedSignal>();
    }
}
