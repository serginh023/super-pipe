using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNovo
{
    public int linhas;
    public int colunas;
    public List<int> pipes;


    public PuzzleNovo(int qtdLinhas, int qtdColunas, List<int> pipes)
    {
        this.linhas     = qtdLinhas;
        this.colunas    = qtdColunas;
        this.pipes      = pipes;
    }
}
