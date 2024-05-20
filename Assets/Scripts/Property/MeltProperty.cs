using UnityEngine;

public class MeltProperty : IProperty
{
    private Element element;
    public MeltProperty(Element element)
    {
        this.element = element;
    }
    public void OnUpdate()
    {
    }
    public void OnEnter(Element element)
    {
        MeltProperty meltProperty = this.element.GetProperty<MeltProperty>();

        if (meltProperty != null)
        {
            Object.Destroy(this.element.gameObject);
        }
    }
    public void OnExit()
    {
    }
}
