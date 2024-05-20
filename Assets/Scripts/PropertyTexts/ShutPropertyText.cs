
public class ShutPropertyText : PropertyTextBase
{
    public override void AddProperty(Element element)
    {
        element.AddProperty(new ShutProperty(element));
    }

    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(ShutProperty));
    }
}
