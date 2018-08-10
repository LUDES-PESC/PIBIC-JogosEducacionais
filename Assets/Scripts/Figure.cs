using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Figure")]
public class Figure : ScriptableObject {
    private const int GRID_SIZE = 8;
    public Texture2D image;

    public bool GetModeOnPixel(int x, int y)
    {
        return (image.GetPixel(x, y).b < 0.1f);
    }
    public string GetTranscription()
    {
        string trans = "";
        for (int lin = 0; lin < GRID_SIZE; lin++)
        {
            bool mode = false; //false = white; true = black;
            int count = 0;
            if(trans != "") trans += "\n";
            for (int col = 0; col < GRID_SIZE; col++)
            {
                if (mode == GetModeOnPixel(lin,col))
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
}
