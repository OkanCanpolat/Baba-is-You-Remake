public class Noun : RuleTextPart
{
    public Element Element;
    public ElementType ElementType;
    public Element ElementPrefab;
    private void Awake()
    {
        Element = GetComponent<Element>();
    }
}
