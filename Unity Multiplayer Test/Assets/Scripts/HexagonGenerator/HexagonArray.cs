using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

[System.Serializable]
public class HexagonArray : MonoBehaviour
{
    [SerializeField]
    public List<List<GameObject>> Tiles = new List<List<GameObject>>();

    private GameObject hexagonTile;
    [SerializeField]
    private Texture2D hexagonMap;
    private Texture2D hexagonPad;
    private Texture2D colorMap;

    private int xGrid = 64;
    private int zGrid = 64;
    private float GridHeight;
    private float radius = 1;
    private float unitLength;
    private bool useAsInnerCircleRadius = true;

    private float offsetX, offsetZ;

    


    void Start()
    {
        hexagonTile = Resources.Load("Prefab/HexagonGenerator/HexagonTile") as GameObject;
        hexagonMap = Resources.Load("Prefab/HexagonGenerator/HeightMapHexagon") as Texture2D;
        hexagonPad = Resources.Load("Prefab/HexagonGenerator/PadPlacement") as Texture2D;
        colorMap = Resources.Load("Prefab/HexagonGenerator/Colormap") as Texture2D;

        unitLength = radius;

        offsetX = unitLength * Mathf.Sqrt(3);
        offsetZ = unitLength * 1.5f;

        for (int i = 0; i < xGrid; i++)
        {
            Tiles.Add(new List<GameObject>());
            for (int j = 0; j < zGrid; j++)
            {
                Vector2 hexpos = HexOffset(i, j);
                Vector3 pos = new Vector3(hexpos.x, hexpos.y, 0);
                if (hexagonPad.GetPixel(i,j).grayscale == 0)
                {
                    GridHeight = hexagonMap.GetPixel(i, j).grayscale * Random.Range(20, 22) + 2;
                }
                else if (hexagonPad.GetPixel(i,j).grayscale == 1)
                { 
                    GridHeight = hexagonMap.GetPixel(i, j).grayscale * 150 + Random.value;
                }
                GameObject Tile = Instantiate(hexagonTile, new Vector3(hexpos.y, 0, hexpos.x), Quaternion.identity);
                Tile.transform.localScale = new Vector3(Tile.transform.localScale.x, GridHeight, Tile.transform.localScale.z);
                Tile.transform.position += new Vector3(0f, GridHeight * 0.5f, 0f);
                Tile.GetComponent<Renderer>().material.color = colorMap.GetPixel(i, j).linear;
                Tiles[i].Add(Tile);

            }
        }


        

    }

    Vector2 HexOffset(int x, int z)
    {
        Vector2 position = Vector2.zero;

        if (z % 2 == 0)
        {
            position.x = x * offsetX;
            position.y = z * offsetZ;
        }
        else
        {
            position.x = (x + 0.5f) * offsetX;
            position.y = z * offsetZ;
        }

        return position;


    }
}
