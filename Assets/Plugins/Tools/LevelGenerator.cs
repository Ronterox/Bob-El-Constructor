using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct ColorLevelTile
{
    public Color color;
    public GameObject gameObject;
}

/// <summary>
/// Generates Levels In base of an image and its pixels
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D map;
    [SerializeField] private ColorLevelTile[] tiles;

    private GameObject tempParent;
    private List<GameObject> tempGroups;

    /// <summary>
    /// Iterates through the whole map and instantiates objects depending the ColorLevelTiles set
    /// </summary>
    public void GenerateLevel()
    {
        #region Organize_On_Groups
        (tempParent = new GameObject()).transform.name = "New Area";
        if (tempGroups.Count > 0) tempGroups.Clear();
        foreach (ColorLevelTile tile in tiles)
        {
            GameObject group = new GameObject();
            group.transform.name = tile.gameObject.name + " Group";
            group.transform.parent = tempParent.transform;
            tempGroups.Add(group);
        }
        #endregion
        for (int x = 0; x < map.width; x++) { for (int y = 0; y < map.width; y++) GenerateTile(x, y); }
    }

    /// <summary>
    /// Instantiates the corresponding gameObject on the position x, y transfered by parameters if the pixel is the corresponded color
    /// </summary>
    /// <param name="x">Position X of the pixel of the image</param>
    /// <param name="y">Position Y of the pixel of the image</param>
    public void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0) return;
        foreach (ColorLevelTile tile in tiles)
        {
            if (tile.color.Equals(pixelColor)) 
                InstantiatePrefab(tile.gameObject, new Vector2(x, y), Quaternion.identity, tempParent.transform)
                    .transform.parent = tempGroups
                    .Find(g => g.transform.name.Contains(tile.gameObject.transform.name)).transform;
        }
    }

    /// <summary>
    /// Instantiates an actual prefab instead of a gameObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    public static GameObject InstantiatePrefab(GameObject prefab, Vector2 position, Quaternion rotation, Transform parent = null)
    {
        GameObject result = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        result.transform.position = position;
        result.transform.parent = parent;
        result.transform.rotation = rotation;
        return result;
    }
}
