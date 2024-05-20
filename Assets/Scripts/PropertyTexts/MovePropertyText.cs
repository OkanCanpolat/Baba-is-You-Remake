using Zenject;

public class MovePropertyText : PropertyTextBase
{
    private GridSystem gridSystem;
    private MovementController movementController;
    private SignalBus signalBus;

    [Inject]
    public void Construct(GridSystem gridSystem, MovementController movementController, SignalBus signalBus)
    {
        this.gridSystem = gridSystem;
        this.movementController = movementController;
        this.signalBus = signalBus;
    }
    public override void AddProperty(Element element)
    {
        element.AddProperty(new MoveProperty(element, signalBus, gridSystem, movementController));
    }
    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(MoveProperty));
    }
}
