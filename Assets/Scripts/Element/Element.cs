using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum ElementType
{
    Baba, Wall, Rock, Flag, Skull, Water, Text, Tile, Grass, Lava, Brick,
    Flower, Ice, Jelly, Star, Crab, Algae, Keke, Love, Pillar, Key, Door, Bubble,
    Hedge
}
public enum FacingDirection
{
    Up, Down, Right, Left
}
public class Element : MonoBehaviour
{
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private ElementType type;
    [SerializeField] private FacingDirection facingDirection;
    private List<IProperty> properties;
    private List<IIntersectResponse> intersectResponses;
    private List<IMoveResponse> moveResponses;
    private List<IProperty> tempProperties;
    private GridSystem gridSystem;
    private MovementController movementController;
    private SignalBus signalBus;

    public int Column { get => column; set => column = value; }
    public int Row { get => row; set => row = value; }
    public ElementType Type => type;
    public List<IIntersectResponse> IntersectResponses => GetIntersectableProperties();
    public List<IMoveResponse> MoveResponses => GetMoveResponseProperties();
    public List<IProperty> Properties => properties;
    public FacingDirection FacingDirection { get => facingDirection; set => facingDirection = value; }

    [Inject]
    public void Construct(GridSystem gridSystem, MovementController movementController, SignalBus signalBus)
    {
        this.gridSystem = gridSystem;
        this.movementController = movementController;
        this.signalBus = signalBus;
    }
    private void Awake()
    {
        properties = new List<IProperty>();
        intersectResponses = new List<IIntersectResponse>();
        moveResponses = new List<IMoveResponse>();
        tempProperties = new List<IProperty>();
    }
    private void Start()
    {
        transform.position = gridSystem.GetCellPosition(column, row);
        gridSystem.GetCell(column, row).Elements.Add(this);

        switch (type)
        {
            case ElementType.Text:
                properties.Add(new PushProperty(this, gridSystem, movementController, signalBus));
                break;
        }
    }

    private void Update()
    {
        foreach (IProperty property in properties)
        {
            property.OnUpdate();
        }
    }
    public bool MoveResponse(Vector3Int direction, Element client)
    {
        bool result = true;
        List<IMoveResponse> responses = MoveResponses;

        foreach (IProperty property in properties)
        {
            if (property is IMoveResponse) responses.Add(property as IMoveResponse);
        }

        responses.Sort((x, y) => y.Priority.CompareTo(x.Priority));

        foreach (IMoveResponse moveResponse in responses)
        {
            result = moveResponse.GetMoveResponse(direction, client);
            return result;
        }

        return result;
    }
    public T GetProperty<T>() where T : IProperty
    {
        foreach (IProperty property in properties)
        {
            if (property is T) return (T)property;
        }

        return default;
    }
    public void AddProperty(IProperty property)
    {
        properties.Add(property);
        property.OnEnter(this);
    }
    public void RemoveProperty(Type type)
    {
        foreach (IProperty property in properties)
        {
            if (property.GetType() == type)
            {
                tempProperties.Add(property);

            }
        }

        foreach (IProperty property in tempProperties)
        {
            properties.Remove(property);
            property.OnExit();
        }

        tempProperties.Clear();
    }

    private List<IIntersectResponse> GetIntersectableProperties()
    {
        intersectResponses.Clear();

        foreach (IProperty property in properties)
        {
            if (property is IIntersectResponse)
            {
                intersectResponses.Add(property as IIntersectResponse);
            }
        }

        return intersectResponses;
    }
    private List<IMoveResponse> GetMoveResponseProperties()
    {
        moveResponses.Clear();

        foreach (IProperty property in properties)
        {
            if (property is IMoveResponse)
            {
                moveResponses.Add(property as IMoveResponse);
            }
        }

        return moveResponses;
    }
    private void OnDestroy()
    {
        gridSystem.GetCell(column, row).Elements.Remove(this);

        foreach (IProperty property in properties)
        {
            property.OnExit();
        }
    }
}
public class ElementFactory : PlaceholderFactory<UnityEngine.Object, Element>
{
}
