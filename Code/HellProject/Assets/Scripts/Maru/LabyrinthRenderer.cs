using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthRenderer : MonoBehaviour
{

    [SerializeField] private int _width = 0;
    [SerializeField] private int _height = 0;
    
    private int[,] labyrinth;

    public TextAsset fileName;
    //private ArrayList<Tile> tiles;

    void Start()
    {
        string[] textString = fileName.text.Split(new string[] { " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
        int numNodes = int.Parse(textString[0]);
    }

    private void generateLabyrinth(int[,] labyrinth)
    {
        //Genero la posición incial del player
        //Vector2Int playerPosition = new Vector2Int(UnityEngine.Random.Range(0, _height), UnityEngine.Random.Range(0, _width));
        Vector2Int playerPosition = new Vector2Int(UnityEngine.Random.Range(0, _height), UnityEngine.Random.Range(0, _width));
        labyrinth[playerPosition.x, playerPosition.y] = 0;

        


    }

    private void setRenderType(int[,] labyrinth, int i, int j)
    {

    }

    private void rasterizeLabyrinth(int[,] dungeon)
    {
        for (int i = 0; i < _height; i++)
            for (int j = 0; j < _width; j++)
                if (dungeon[i, j] == 1)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(j - (_width / 2), 0, i - (_height / 2));
                }
    }

}


class Tile
{
    //0 land, 1 lava
    int colliderType { get; set; }

    //lava positions
    bool top, topRight, right, bottomRight, bottom, bottomLeft, left, topLeft;


    Tile(int colliderType)
    {
        this.colliderType = colliderType;
    }
}