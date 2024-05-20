using UnityEngine;

public class NounNounRule : IRule
{
    private Noun prefix;
    private Noun suffix;
    private OperatorBase op;
    private ElementFactory factory;
    private RuleCompiler ruleCompiler;

    public Noun Prefix => prefix;
    public Noun Suffix => suffix;
    public OperatorBase Operator => op;

    public NounNounRule(Noun prefix, Noun suffix, ElementFactory factory, RuleCompiler ruleCompiler, OperatorBase op)
    {
        this.prefix = prefix;
        this.suffix = suffix;
        this.op = op;
        this.factory = factory;
        this.ruleCompiler = ruleCompiler;
    }
    public void Compile()
    {
        if (prefix.ElementType != suffix.ElementType)
        {
            Element[] prefixElements = Object.FindObjectsOfType<Element>();

            foreach (Element element in prefixElements)
            {
                if (element.Type == prefix.ElementType)
                {
                    int column = element.Column;
                    int row = element.Row;
                    Object.Destroy(element.gameObject);

                    Element suffixElement = factory.Create(suffix.ElementPrefab);
                    suffixElement.Column = column;
                    suffixElement.Row = row;
                    ruleCompiler.CompileForOne(suffixElement);
                }
            }
        }

        prefix.GetComponent<SpriteRenderer>().color = prefix.ValidRuleColor;
        prefix.ConnectedRuleCount++;

        op.GetComponent<SpriteRenderer>().color = op.ValidRuleColor;
        op.ConnectedRuleCount++;

        suffix.GetComponent<SpriteRenderer>().color = suffix.ValidRuleColor;
        suffix.ConnectedRuleCount++;
    }

    public void Decompile()
    {
        prefix.ConnectedRuleCount--;
        if (prefix.ConnectedRuleCount <= 0) prefix.GetComponent<SpriteRenderer>().color = prefix.InvalidRuleColor;


        op.ConnectedRuleCount--;
        if (op.ConnectedRuleCount <= 0) op.GetComponent<SpriteRenderer>().color = op.InvalidRuleColor;

        suffix.ConnectedRuleCount--;
        if (suffix.ConnectedRuleCount <= 0) suffix.GetComponent<SpriteRenderer>().color = suffix.InvalidRuleColor;
    }

    public bool IsSameRule(IRule rule)
    {
        if (!(rule is NounNounRule)) return false;

        NounNounRule target = rule as NounNounRule;

        if (target.Suffix == Suffix && target.Prefix == Prefix)
        {
            return true;
        }

        return false;
    }
}
