using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private List<Element> elements;
    public int ElementCount => elements.Count;
    public List<Element> Elements => elements;

    private void Awake()
    {
        elements = new List<Element>();
    }
}
