using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class MovementController : MonoBehaviour
{
    private List<Element> moveableElements;
    private GridSystem gridSystem;
    public List<Element> MoveableElements => moveableElements;

    [Inject]
    public void Construct(GridSystem gridSystem, RuleCompiler ruleCompiler)
    {
        this.gridSystem = gridSystem;
        moveableElements = new List<Element>();
    }

    public void StartMovement(Vector3Int direction)
    {
        foreach (Element element in moveableElements)
        {
            Vector3Int result = new Vector3Int(element.Column, element.Row) + direction;

            Cell currentCell = gridSystem.GetCell(element.Column, element.Row);
            currentCell.Elements.Remove(element);

            element.Column = result.x;
            element.Row = result.y;

            Cell targetCell = gridSystem.GetCell(element.Column, element.Row);

            List<IIntersectResponse> targetCellIntersectResponses = new List<IIntersectResponse>();
            List<IIntersectResponse> currentElementIntersectResponses = element.IntersectResponses;

            List<Element> targetCellElements = targetCell.Elements;

            foreach (Element e in targetCellElements)
            {
                foreach (IIntersectResponse intersectResponse in e.IntersectResponses)
                {
                    targetCellIntersectResponses.Add(intersectResponse);
                }
            }

            currentElementIntersectResponses.Sort((x, y) => y.Priority.CompareTo(x.Priority));
            targetCellIntersectResponses.Sort((x, y) => y.Priority.CompareTo(x.Priority));

            foreach(Element targetElement in targetCellElements)
            {
                foreach (IIntersectResponse intersectResponse in currentElementIntersectResponses)
                {
                    if (intersectResponse.GetIntersectResponse(targetElement)) break;
                }
            }

            foreach (IIntersectResponse intersectResponse in targetCellIntersectResponses)
            {
                if (intersectResponse.GetIntersectResponse(element)) break;
            }


            targetCell.Elements.Add(element);
            element.transform.position = result;
        }

        moveableElements.Clear();
    }
}
