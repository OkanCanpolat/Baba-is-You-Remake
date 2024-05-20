using UnityEngine;

public class NounPropertyRule : IRule
{
    private Noun noun;
    private OperatorBase op;
    private PropertyTextBase propertyText;
    public Noun Noun => noun;
    public PropertyTextBase PropertyText => propertyText;
    public NounPropertyRule(Noun noun, PropertyTextBase propertyText, OperatorBase op)
    {
        this.noun = noun;
        this.op = op;
        this.propertyText = propertyText;
    }

    public void Compile()
    {
        Element[] targetElements = Object.FindObjectsOfType<Element>();

        foreach(Element element in targetElements)
        {
            if(element.Type == noun.ElementType)
            {
                propertyText.AddProperty(element);
            }
        }

        noun.GetComponent<SpriteRenderer>().color = noun.ValidRuleColor;
        noun.ConnectedRuleCount++;

        op.GetComponent<SpriteRenderer>().color = op.ValidRuleColor;
        op.ConnectedRuleCount++;

        propertyText.GetComponent<SpriteRenderer>().color = propertyText.ValidRuleColor;
        propertyText.ConnectedRuleCount++;
    }
    public void Decompile()
    {
        Element[] targetElements = Object.FindObjectsOfType<Element>();

        foreach (Element element in targetElements)
        {
            if (element.Type == noun.ElementType)
            {
                propertyText.RemoveProperty(element);
            }
        }

        noun.ConnectedRuleCount--;
        if(noun.ConnectedRuleCount <= 0) noun.GetComponent<SpriteRenderer>().color = noun.InvalidRuleColor;


        op.ConnectedRuleCount--;
        if (op.ConnectedRuleCount <= 0) op.GetComponent<SpriteRenderer>().color = op.InvalidRuleColor;

        propertyText.ConnectedRuleCount--;
        if (propertyText.ConnectedRuleCount <= 0) propertyText.GetComponent<SpriteRenderer>().color = propertyText.InvalidRuleColor;
    }
    public bool IsSameRule(IRule rule)
    {
        if (!(rule is NounPropertyRule)) return false;

        NounPropertyRule target = rule as NounPropertyRule;

        if (target.PropertyText == PropertyText && target.Noun == Noun)
        {
            return true;
        }

        return false;
    }
    public void CompileForOne(Element element)
    {
        if (element.Type == noun.ElementType)
        {
            propertyText.AddProperty(element);
        }
    }
}
