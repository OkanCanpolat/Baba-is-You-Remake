using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Cell cellPrefab;
    private Cell[,] grid;
    public int Width => width;
    public int Height => height;

    private void Awake()
    {
        grid = new Cell[width, height];

        GenerateMap();
    }
    private void GenerateMap()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        char splitChar = '_';

        foreach (Cell cell in cells)
        {
            string cellName = cell.name;
            string[] splitedName = cellName.Split(splitChar);
            int x = int.Parse(splitedName[0]);
            int y = int.Parse(splitedName[1]);
            grid[x, y] = cell;
        }
    }
    public Vector2 GetCellPosition(int column, int row)
    {
        return grid[column, row].transform.position;
    }
    public Cell GetCell(int column, int row)
    {
        if (!IsInsideGrid(column, row)) return null;

        return grid[column, row];
    }
    public bool IsInsideGrid(int column, int row)
    {
        return column >= 0 && column < width && row >= 0 && row < height;
    }
}
