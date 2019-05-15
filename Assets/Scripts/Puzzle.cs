using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

struct Pipefinal{
    int tipo;
    int x;
    int y;
    int posicao;
}

public class Puzzle
{
    public int id;
    public int tipo;
    public int altura;
    public int largura;
    public int[,] pipes;
    

    public Puzzle(int id, int tipo, int altura, int largura, int[,] pipes/*, Pipefinal pipesFinals*/){
        this.altura     = altura;
        this.largura    = largura;
        this.id         = id;
        this.tipo       = tipo;
        this.pipes      = pipes;
        //this.pipesFinals = pipesFinals;
    }

}