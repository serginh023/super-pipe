using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Puzzle
{
    public int id;
    public int tipo;
    public int altura;
    public int largura;
    public int[,] pipes;
    public List<> pipesFinals;


    public Puzzle(int id, int tipo, int altura, int largura, int[,] pipes){
        this.altura         = altura;
        this.largura        = largura;
        this.id             = id;
        this.tipo           = tipo;
        this.pipes          = pipes;
        this.pipesFinals    = pipesFinals;
    }   



}