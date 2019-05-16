using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public struct PipeFinal
{
    public int x, y, tipo, rotacao;

    public PipeFinal(int x1, int y1, int tipo1, int rotacao1)
    {
        x       = x1;
        y       = y1;
        tipo    = tipo1;
        rotacao = rotacao1;
    }
}

public class Puzzle
{
    public int id;
    public int tipo;
    public int altura;
    public int largura;
    public int[,] pipes;
    
    public List<PipeFinal> pipesFinals;

    public Puzzle(int id, int tipo, int altura, int largura, int[,] pipes, List<PipeFinal> pipesFinals)
    {
        this.altura         = altura;
        this.largura        = largura;
        this.id             = id;
        this.tipo           = tipo;
        this.pipes          = pipes;
        this.pipesFinals    = pipesFinals;
    }

}