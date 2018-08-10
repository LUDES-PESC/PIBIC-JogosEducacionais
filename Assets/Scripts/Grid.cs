using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    private const int GRID_SIZE = 8;
    private Tile[,] tiles = new Tile[GRID_SIZE,GRID_SIZE];

    private void Start()
    {
        PutChildTilesInMatrix();
    }
    public string GetTranscription()
    {
        string trans = "";
        for(int lin = 0; lin < GRID_SIZE; lin++)
        {
            bool mode = false; //false = white; true = black;
            int count = 0;
            for(int col = 0; col < GRID_SIZE; col++)
            {
                if (mode == tiles[lin,col].GetMode())
                {
                    count++;
                }
                else
                {
                    mode = !mode;
                    trans += count + ", ";
                    count = 1;
                }
            }
            trans += count;
        }
        return trans;
    }
    public float CompareFigureAndDrawing(Figure fig)
    {
        int score = 0;
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                if (tiles[i, j].GetMode() == fig.GetModeOnPixel(i, j))
                    score++;
            }
        }
        return (float) (score * 100) / (GRID_SIZE * GRID_SIZE);
    }
    private void PutChildTilesInMatrix()
    {
        for(int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                tiles[i, j] = transform.GetChild(i * GRID_SIZE + j).GetComponent<Tile>();
            }
        }
    }
    public void EraseAllTiles()
    {
        StartCoroutine(c_EraseAllTiles());
    }
    private IEnumerator c_EraseAllTiles()
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                yield return new WaitForSeconds(0.01f);
                StartCoroutine(tiles[i, j].Disable());
            }
        }
    }
}
