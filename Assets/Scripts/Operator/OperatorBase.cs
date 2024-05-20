
public enum OperatorType
{
    Is, And
}
public abstract class OperatorBase : RuleTextPart
{
    public Element Element;
    public OperatorType Type;

    public abstract void FindRules();
}
