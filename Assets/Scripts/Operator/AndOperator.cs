using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AndOperator : OperatorBase
{
    private GridSystem gridSystem;
    private List<Noun> beforeIsNouns = new List<Noun>();
    private List<OperatorBase> beforeIsOperators = new List<OperatorBase>();
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
        Noun first = FindFarPart(Vector2Int.left);
        if (first != null)
        {
            StartSearch(first, Vector2Int.right);
            beforeIsNouns.Clear();
            beforeIsOperators.Clear();
        }

        Noun first2 = FindFarPart(Vector2Int.up);

        if (first2 != null)
        {
            StartSearch(first2, Vector2Int.down);
            beforeIsNouns.Clear();
            beforeIsOperators.Clear();
        }
    }

    private Noun FindFarPart(Vector2Int direction)
    {
        Cell cell = gridSystem.GetCell(Element.Column + direction.x, Element.Row + direction.y);
        Noun firstNoun = null;
        RuleTextPart temp = null;

        do
        {
            if (cell == null || cell.Elements.Count <= 0) return firstNoun;

            foreach (Element e in cell.Elements)
            {
                temp = e.GetComponent<RuleTextPart>();

                if (temp != null)
                {
                    if (temp is Noun) firstNoun = (Noun)temp;
                    cell = gridSystem.GetCell(e.Column + direction.x, e.Row + direction.y);
                    break;
                }
            }
        } while (temp != null);

        return firstNoun;
    }

    private void StartSearch(Noun noun, Vector2Int direction)
    {
        Cell cell = gridSystem.GetCell(noun.Element.Column + direction.x, noun.Element.Row + direction.y);

        foreach (Element e in cell.Elements)
        {
            OperatorBase op = e.GetComponent<OperatorBase>();

            if (op == null) continue;

            switch (op.Type)
            {
                case OperatorType.Is:
                    beforeIsNouns.Add(noun);
                    SearchAfterIs(e, direction);
                    break;
                case OperatorType.And:
                    beforeIsOperators.Add(op);
                    beforeIsNouns.Add(noun);
                    SearchAfterAndBeforeIs(e, direction);
                    break;
            }
        }
    }
    private void SearchAfterAndBeforeIs(Element element, Vector2Int direction)
    {
        Cell cell = gridSystem.GetCell(element.Column + direction.x, element.Row + direction.y);

        foreach (Element e in cell.Elements)
        {
            Noun noun = e.GetComponent<Noun>();

            if (noun != null)
            {
                StartSearch(noun, direction);
                break;
            }
        }
    }
    private void SearchAfterIs(Element isElement, Vector2Int direction)
    {
        Cell cell = gridSystem.GetCell(isElement.Column + direction.x, isElement.Row + direction.y);

        foreach (Element e in cell.Elements)
        {
            Noun noun = e.GetComponent<Noun>();

            if (noun != null)
            {
                SearchAndOperator(e, direction);
                break;
            }

            PropertyTextBase propertyText = e.GetComponent<PropertyTextBase>();

            if (propertyText != null)
            {
                for (int i = 0; i < beforeIsNouns.Count - 1; i++)
                {
                    Noun beforeIsNoun = beforeIsNouns[i];
                    OperatorBase beforeIsOp = beforeIsOperators[i];
                    NounPropertyRule nounPropertyRule = new NounPropertyRule(beforeIsNoun, propertyText, beforeIsOp);
                    ruleCompiler.AddRule(nounPropertyRule);
                }
                SearchAndOperator(e, direction);
                break;
            }
        }
    }

    private void SearchAndOperator(Element element, Vector2Int direction)
    {
        Cell cell = gridSystem.GetCell(element.Column + direction.x, element.Row + direction.y);

        foreach (Element e in cell.Elements)
        {
            AndOperator andOperator = e.GetComponent<AndOperator>();

            if (andOperator != null)
            {
                SearchAfterAnd(e, andOperator, direction);
                break;
            }
        }
    }

    private void SearchAfterAnd(Element element, OperatorBase and, Vector2Int direction)
    {
        Cell cell = gridSystem.GetCell(element.Column + direction.x, element.Row + direction.y);

        foreach (Element e in cell.Elements)
        {
            Noun noun = e.GetComponent<Noun>();

            if (noun != null)
            {
                foreach (Noun n in beforeIsNouns)
                {
                    NounNounRule nounNounRule = new NounNounRule(n, noun,factory,ruleCompiler, and);
                    ruleCompiler.AddRule(nounNounRule);
                }
                SearchAndOperator(e, direction);
                break;
            }

            PropertyTextBase propertyText = e.GetComponent<PropertyTextBase>();

            if (propertyText != null)
            {
                foreach (Noun n in beforeIsNouns)
                {
                    NounPropertyRule nounPropertyRule = new NounPropertyRule(n, propertyText, and);
                    ruleCompiler.AddRule(nounPropertyRule);
                }

                SearchAndOperator(e, direction);
                break;
            }
        }
    }
}
