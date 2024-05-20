public enum PropertType
{
    Push, Stop, You, Win, Defeat, Float, Sink, Hot, Melt, Move, Open, Shut
}
public abstract class PropertyTextBase : RuleTextPart
{
    public PropertType PropertType;
    public abstract void AddProperty(Element element);
    public abstract void RemoveProperty(Element element);
}
