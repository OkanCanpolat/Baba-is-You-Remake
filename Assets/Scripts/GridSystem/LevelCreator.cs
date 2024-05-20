#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public int width;
    public int height;
    public Cell CellPrefab;
    public Color CellColor;
    public LevelDataHolder LevelDataHolder;

    [HideInInspector] public bool mapCreated;
    [HideInInspector] public Dictionary<Vector2, GridData> GridData = new Dictionary<Vector2, GridData>();

    public void Create()
    {

        GameObject parent = GameObject.Find("Cells");

        if (parent != null) return;

        parent = new GameObject("Cells");

        for (int c = 0; c < width; c++)
        {
            for (int r = 0; r < height; r++)
            {
                Cell cell = Instantiate(CellPrefab, new Vector2(c, r), Quaternion.identity, parent.transform);
                cell.gameObject.name = c + "_" + r;
                cell.GetComponent<SpriteRenderer>().color = CellColor;
            }
        }
        mapCreated = true;
    }
    public void Delete()
    {
        GameObject parent = GameObject.Find("Cells");

        if (parent != null) DestroyImmediate(parent);
        mapCreated = false;
    }
}

#endif
