using Zenject;
public class WinPropertyText : PropertyTextBase
{
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
    public override void AddProperty(Element element)
    {
        element.AddProperty(new WinProperty(signalBus));
    }

    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(WinProperty));
    }
}
