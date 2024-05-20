using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<YouDiedSignal>().OptionalSubscriber();
        Container.DeclareSignal<YouAddedSignal>().OptionalSubscriber();
        Container.DeclareSignal<MovemenetSignal>().OptionalSubscriber();
        Container.DeclareSignal<TurnEndSignal>().OptionalSubscriber();
        Container.DeclareSignal<LevelCompletedSignal>().OptionalSubscriber();

        Container.Bind<GridSystem>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MovementController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<RuleCompiler>().FromComponentInHierarchy().AsSingle();
        Container.BindFactory<UnityEngine.Object, Element, ElementFactory>().FromFactory<PrefabFactory<Element>>();
    }
}

public class YouDiedSignal { }
public class YouAddedSignal { }
public class MovemenetSignal
{
    public Vector3Int Direction;
    public Element Element;
    public MovemenetSignal(Element element)
    {
        Element = element;
    }
}
public class TurnEndSignal { }
public class LevelCompletedSignal { }