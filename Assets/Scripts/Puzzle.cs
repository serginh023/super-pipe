using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Puzzle
{
    private int altura;
    private int largura;
    private int id;
    private int tipo;
    private int[,] pipes;

    public Puzzle(int id, int altura, int largura, int tipo, int[,] pipes){
        this.altura     = altura;
        this.largura    = largura;
        this.id         = id;
        this.tipo       = tipo;
        this.pipes      = pipes;
    }

}