using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class RuleCompiler : MonoBehaviour
{
    [SerializeField] private GameObject illegalRuleXPrefab;
    private List<OperatorBase> operators;
    private List<IRule> rules;
    private List<IRule> newRules;
    private List<IRule> tempRules;
    private Dictionary<NounNounRule, Noun> illegalRulesMap;
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void Awake()
    {
        operators = FindObjectsOfType<OperatorBase>().ToList();
        rules = new List<IRule>();
        newRules = new List<IRule>();
        tempRules = new List<IRule>();
        illegalRulesMap = new Dictionary<NounNounRule, Noun>();
        signalBus.Subscribe<TurnEndSignal>(OnTurnEnd);
    }
    private void OnTurnEnd()
    {
        StartCoroutine(OnTurnEndC());
    }
    private IEnumerator OnTurnEndC()
    {
        yield return new WaitForEndOfFrame();
        FindRules();
    }

    private void Start()
    {
        FindRules();
    }
    public void FindRules()
    {
        foreach (OperatorBase op in operators)
        {
            op.FindRules();
        }
        ChangeIllegalNounNouns();
        CompilesRules();
        newRules.Clear();
    }
    public void CompilesRules()
    {
        FindRemovedRules(rules, newRules);
        FindNewRules(rules, newRules);
    }
    public void AddRule(IRule rule)
    {
        newRules.Add(rule);
    }
    public void CompileForOne(Element element)
    {
        foreach (IRule rule in rules)
        {
            if (rule is NounPropertyRule)
            {
                NounPropertyRule nounPropRule = rule as NounPropertyRule;

                if (nounPropRule.Noun.ElementType == element.Type)
                {
                    nounPropRule.CompileForOne(element);
                }
            }
        }
    }
    private void FindNewRules(List<IRule> source, List<IRule> target)
    {
        foreach (IRule rule in target)
        {
            if (!source.ContainsRule(source, rule))
            {
                tempRules.Add(rule);
                rule.Compile();
            }
        }

        foreach (IRule rule in tempRules)
        {
            source.Add(rule);
        }

        tempRules.Clear();
    }
    private void FindRemovedRules(List<IRule> source, List<IRule> target)
    {
        foreach (IRule rule in source)
        {
            if (!target.ContainsRule(target, rule))
            {
                tempRules.Add(rule);
                rule.Decompile();
            }
        }

        foreach (IRule rule in tempRules)
        {
            source.Remove(rule);
        }

        tempRules.Clear();
    }
    private void ChangeIllegalNounNouns()
    {
        for (int i = 0; i < newRules.Count - 1; i++)
        {
            if (!(newRules[i] is NounNounRule)) continue;

            NounNounRule source = newRules[i] as NounNounRule;

            for (int j = i + 1; j < newRules.Count; j++)
            {
                if (!(newRules[j] is NounNounRule)) continue;

                NounNounRule target = newRules[j] as NounNounRule;


                if (source.Prefix.ElementType == target.Prefix.ElementType
                    && source.Suffix.ElementType != target.Suffix.ElementType)
                {
                    MarkIllegalNounRule(source, target);
                }
            }

        }

        foreach (KeyValuePair<NounNounRule, Noun> illegalRule in illegalRulesMap)
        {
            newRules.Remove(illegalRule.Key);
            newRules.Add(new IllegalNounNounRule(illegalRule.Key.Prefix, illegalRule.Key.Suffix, illegalRule.Key.Operator, illegalRule.Value,illegalRuleXPrefab));
        }

        illegalRulesMap.Clear();
    }
    private void MarkIllegalNounRule(NounNounRule one, NounNounRule two)
    {
        if (rules.ContainsRule(rules, one))
        {
            Noun noun = one.Prefix == two.Prefix ? two.Suffix : two.Prefix;
            illegalRulesMap.Add(two, noun);
        }
        else if (rules.ContainsRule(rules, two))
        {
            Noun noun = one.Prefix == two.Prefix ? one.Suffix : one.Prefix;
            illegalRulesMap.Add(one, noun);
        }
        else
        {
            Noun noun = one.Prefix == two.Prefix ? two.Suffix : two.Prefix;
            illegalRulesMap.Add(two, noun);
        }
    }
}
