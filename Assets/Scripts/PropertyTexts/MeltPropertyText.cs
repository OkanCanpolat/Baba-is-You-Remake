public class MeltPropertyText : PropertyTextBase
{
    public override void AddProperty(Element element)
    {
        element.AddProperty(new MeltProperty(element));
    }

    public override void RemoveProperty(Element element)
    {
        element.RemoveProperty(typeof(MeltProperty));
    }
}
