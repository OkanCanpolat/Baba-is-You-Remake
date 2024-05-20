using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IsOperator : OperatorBase
{
    private GridSystem gridSystem;
    private RuleCompiler ruleCompiler;
    private ElementFactory factory;

    [Inject]
    public void Construct(GridSystem gridSystem, RuleCompiler ruleCompiler, ElementFactory factory)
    {
        this.gridSystem = gridSystem;
        this.ruleCompiler = ruleCompiler;
        this.factory = factory;
    }
    public override void FindRules()
    {
        FindRule(Vector3Int.right);
        FindRule(Vector3Int.down);
    }

    private void FindRule(Vector3Int direction)
    {
        Cell cellBefore = gridSystem.GetCell(Element.Column - direction.x, Element.Row - direction.y);

        if (cellBefore == null) return;
        List<Element> leftElements = cellBefore.Elements;

        foreach (Element leftElement in leftElements)
        {
            Noun nounBefore = leftElement.GetComponent<Noun>();
            if (nounBefore == null) continue;

            Cell cellAfter = gridSystem.GetCell(Element.Column + direction.x, Element.Row + direction.y);

            if (cellAfter == null) return;
            List<Element> rightElements = cellAfter.Elements;

            foreach (Element rightElement in rightElements)
            {
                Noun nounAfter = rightElement.GetComponent<Noun>();

                if (nounAfter != null)
                {
                    ruleCompiler.AddRule(new NounNounRule(nounBefore, nounAfter, factory, ruleCompiler, this));
                    return;
                }

                PropertyTextBase propertyText = rightElement.GetComponent<PropertyTextBase>();

                if (propertyText != null)
                {
                    ruleCompiler.AddRule(new NounPropertyRule(nounBefore, propertyText, this));
                    return;
                }
            }
        }
    }
}
