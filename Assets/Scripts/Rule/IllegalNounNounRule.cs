using System.Collections.Generic;
using UnityEngine;

public class IllegalNounNounRule : IRule
{
    private Noun prefix;
    private Noun suffix;
    private OperatorBase op;
    private Noun illegalNoun;
    private GameObject xSpritePrefab;
    private List<GameObject> createdXSprites;
    public Noun Prefix => prefix;
    public Noun Suffix => suffix;
    public IllegalNounNounRule(Noun prefix, Noun suffix, OperatorBase op, Noun illegalNoun, GameObject xSpritePrefab)
    {
        this.prefix = prefix;
        this.suffix = suffix;
        this.op = op;
        this.illegalNoun = illegalNoun;
        this.xSpritePrefab = xSpritePrefab;
        createdXSprites = new List<GameObject>();
    }
    public void Compile()
    {
        prefix.GetComponent<SpriteRenderer>().color = prefix.ValidRuleColor;
        prefix.ConnectedRuleCount++;

        op.GetComponent<SpriteRenderer>().color = op.ValidRuleColor;
        op.ConnectedRuleCount++;
        
        suffix.GetComponent<SpriteRenderer>().color = suffix.ValidRuleColor;
        suffix.ConnectedRuleCount++;

        GameObject illegalNounX = Object.Instantiate(xSpritePrefab, Vector3.zero, Quaternion.identity);
        createdXSprites.Add(illegalNounX);
        illegalNounX.transform.SetParent(illegalNoun.transform, false);

        GameObject opX = Object.Instantiate(xSpritePrefab, Vector3.zero, Quaternion.identity);
        createdXSprites.Add(opX);
        opX.transform.SetParent(op.transform, false);
    }

    public void Decompile()
    {
        prefix.ConnectedRuleCount--;
        if (prefix.ConnectedRuleCount <= 0) prefix.GetComponent<SpriteRenderer>().color = prefix.InvalidRuleColor;

        op.ConnectedRuleCount--;
        if (op.ConnectedRuleCount <= 0) op.GetComponent<SpriteRenderer>().color = op.InvalidRuleColor;

        suffix.ConnectedRuleCount--;
        if (suffix.ConnectedRuleCount <= 0) suffix.GetComponent<SpriteRenderer>().color = suffix.InvalidRuleColor;

        foreach (GameObject xSprite in createdXSprites)
        {
            Object.Destroy(xSprite);
        }
    }
    public bool IsSameRule(IRule rule)
    {
        if (!(rule is IllegalNounNounRule)) return false;

        IllegalNounNounRule target = rule as IllegalNounNounRule;

        if (target.Suffix == Suffix && target.Prefix == Prefix)
        {
            return true;
        }
        return false;
    }
}
