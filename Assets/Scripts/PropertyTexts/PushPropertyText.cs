using Zenject;

public class PushPropertyText : PropertyTextBase
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
        element.AddProperty(new PushProperty(element, gridSystem, movementController, signalBus));
    }
    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(PushProperty));
    }
}
