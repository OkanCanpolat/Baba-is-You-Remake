public class OpenPropertyText : PropertyTextBase
{
    public override void AddProperty(Element element)
    {
        element.AddProperty(new OpenProperty());
    }
    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(OpenProperty));
    }
}
