public class StopPropertText : PropertyTextBase
{
    public override void AddProperty(Element element)
    {
        element.AddProperty(new StopProperty());
    }
    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(StopProperty));
    }
}
