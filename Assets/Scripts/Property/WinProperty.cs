using Zenject;
public class WinProperty : IProperty, IIntersectResponse
{
    private SignalBus signalBus;
    private const int intersectPriority = 500;
    public int Priority => intersectPriority;
    public WinProperty(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
    public bool GetIntersectResponse(Element element)
    {
        YouProperty youProperty = element.GetProperty<YouProperty>();

        if (youProperty != null)
        {
            signalBus.TryFire<LevelCompletedSignal>();
            return true;
        }
        return false;
    }
    public void OnUpdate()
    {
    }
    public void OnEnter(Element element)
    {
        YouProperty youProperty = element.GetProperty<YouProperty>();

        if (youProperty != null)
        {
            signalBus.TryFire<LevelCompletedSignal>();
        }
    }
    public void OnExit()
    {
    }
}
