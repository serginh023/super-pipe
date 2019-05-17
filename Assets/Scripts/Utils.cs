using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class Utils
{

    public static List<Puzzle> consomeDicionario(){
        string puzzleFonte;
        var fileStream = new FileStream(Application.dataPath + "/Resources/puzzle.txt", FileMode.Open, FileAccess.Read);
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
        {
            puzzleFonte = streamReader.ReadToEnd();
        }

        string[] texto = puzzleFonte.Split('\n');

        List<Puzzle> listaPuzzles = new List<Puzzle>();
        foreach(string linha in texto){
            string[] s = linha.Split('\t');

            int id;
            int.TryParse(s[0], out id);
            int tipo;
            int.TryParse(s[1], out tipo);
            int altura;
            int.TryParse(s[2], out altura);
            int largura;
            int.TryParse(s[3], out largura);
            //inicia a matriz de pipes com -1 em todas as posições
            int[,] pipes = zeraMatriz(altura, largura);

            string[] pipeString = s[4].Split(' ');

            for(int i = 0; i <= pipeString.Length-3;i+=3){
                //  pipes[i, i+1] = i+2;
                 int pos1;
                 int.TryParse(pipeString[i], out pos1);
                 int pos2;
                 int.TryParse(pipeString[i+1], out pos2);
                 int pipe;
                 int.TryParse(pipeString[i+2], out pipe);
                 pipes[pos1, pos2] = pipe;
            }

            string[] pipesFinalString = s[5].Split(' ');
            List<PipeFinal> pipeFinals = new List<PipeFinal>();

            for (int i = 0; i <= pipesFinalString.Length-4; i= i + 4)
            {
                int pos1;
                int.TryParse(pipesFinalString[i], out pos1);
               
                int pos2;
                int.TryParse(pipesFinalString[i + 1], out pos2);
                
                int pipe;
                int.TryParse(pipesFinalString[i + 2], out pipe);
                
                int pipe2;
                int.TryParse(pipesFinalString[i + 3], out pipe2);
                pipes[pos1, pos2] = pipe;
                

                PipeFinal pipeFinal = new PipeFinal(pos1, pos2, pipe, pipe2);
                pipeFinals.Add(pipeFinal);
                
            }

            Puzzle puzzle = new Puzzle(id, altura, largura, tipo, pipes, pipeFinals);
            
            listaPuzzles.Add(puzzle);
        }

        return listaPuzzles;
    }

    private static int[,] zeraMatriz(int altura, int largura){
        int[,] pipes = new int[altura, largura];

        for(int i = 0; i < altura; i++)
            for(int c = 0; c < largura; c++)
                pipes[i, c] = -1;

        return pipes;        

    }
}
