using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public TextAsset puzzleFonte;
    public void consomeDicionario(){
        string[] texto = puzzleFonte.text.Split('\n');

        foreach(string linha in texto){
            string[] s = linha.Split('\t');

            int id      = Int32.Parse(s[0]);
            int tipo    = Int32.Parse(s[1]);
            int altura  = Int32.Parse(s[2]);
            int largura = Int32.Parse(s[3]);
            //inicia a matriz de pipes com -1 em todas as posições
            int[, ] pipes = zeraMatriz(altura, largura);

            string[] pipeString = s[4].Split(' ');

            for(int i = 0; i < pipeString.Length-3;i=+3)
                pipes[i, i+1] = i+2;

            Puzzle puzzle = new Puzzle(id, altura, largura, tipo, pipes);
            /*
            PRECISA-SE COLOCAR UMA ESTRUTURA DE PUZZLES ARMAZENADA PARA PODER CARREGAR TODOS OS PUZZLES A HORA QUE O GAME COMEÇAR,
            TAMBÉM PARA CARREGAR CADA PUZZLE QUANDO CADA FASE DO GAME INICIAR
            */

            

        }
    }

    private int[,] zeraMatriz(int altura, int largura){
        int[,] pipes = new int[altura, largura];

        for(int i = 0; i < altura; i++)
            for(int c = 0; c < largura; c++)
                pipes[i, c] = -1;

        return pipes;        

    }
}
