using Zenject;

public class HotPropertyText : PropertyTextBase
{
    public override void AddProperty(Element element)
    {
        element.AddProperty(new HotProperty());
    }

    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(HotProperty));
    }
}
