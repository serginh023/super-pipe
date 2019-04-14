using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{

    public int altura = 5;
    public int largura = 5;
    
    private int[,] puzzle;

    

    // Start is called before the first frame update
    void Start()
    {
        puzzle = new int[altura, largura];
    }

}
