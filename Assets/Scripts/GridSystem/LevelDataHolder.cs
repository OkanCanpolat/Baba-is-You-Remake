#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridData
{
    public Texture Texture;
    public List<Element> Elements = new List<Element>();

    public GridData(Texture texture, params Element[] elements)
    {
        Texture = texture;

        foreach (Element e in elements)
        {
            Elements.Add(e);
        }
    }
}
[Serializable]
public class DataHolderEditor
{
    public Vector2 Tile;
    public GridData TileData;

    public DataHolderEditor(Vector2 tile, GridData tileData)
    {
        Tile = tile;
        TileData = tileData;
    }
}
public class LevelDataHolder : MonoBehaviour
{
    public List<DataHolderEditor> DataHolders;
    public bool TryGetValue(Vector2 tile, out GridData gridData)
    {
        foreach(DataHolderEditor dataHolderEditor in DataHolders)
        {
            if(dataHolderEditor.Tile == tile)
            {
                gridData = dataHolderEditor.TileData;
                return true;
            }
        }
        gridData = null;
        return false;
    }

    public void Remove(Vector2 tile)
    {
        DataHolderEditor dataHolder = null;

        foreach (DataHolderEditor dataHolderEditor in DataHolders)
        {
            if (dataHolderEditor.Tile == tile)
            {
                dataHolder = dataHolderEditor;
                break;
            }
        }

        if (dataHolder != null) DataHolders.Remove(dataHolder);
    }
}

#endif