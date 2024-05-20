using Zenject;

public class DefeatPropertyText : PropertyTextBase
{
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
    public override void AddProperty(Element element)
    {
        element.AddProperty(new DefeatProperty());
    }

    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(DefeatProperty));
    }
}
