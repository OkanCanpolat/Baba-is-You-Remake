public interface IRule 
{
    public void Compile();
    public void Decompile();
    public bool IsSameRule(IRule rule);
}
