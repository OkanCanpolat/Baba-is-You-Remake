
public class SinkPropertyText : PropertyTextBase
{
    public override void AddProperty(Element element)
    {
        element.AddProperty(new SinkProperty(element));
    }

    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(SinkProperty));
    }
}
