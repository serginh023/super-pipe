using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    float [,,] posicoes;
    float xInicial = -2.4f;
    float yInicial = -2.7f;
    int qtdX = 5;
    int qtdY = 6;

    public Transform pipeRetoPrefab;
    public Transform pipeCurvoPrefab;
    public Transform pipeCruzPrefab;

    string dataPath;
    private int faseAtual = 1;

    private Dictionary<int, Puzzle> dicionarioPuzzles;


    // Start is called before the first frame update
    void Start()
    {
        dicionarioPuzzles = new Dictionary<int, Puzzle>();
        
    }

    float[,,] criaVetorPosicoes(float x, float y, int qtdX, int qtdY){
        float xbase = x;
        float ybase = y;
        float[, , ] posicoes = new float[qtdX, qtdY, 3];

        float passo = 1.2f;

        for(int i = 0; i < qtdX; i++)
            for(int j = 0; j < qtdY; j++){
                posicoes[i, j, 0] = xbase + passo*i;
                posicoes[i, j, 1] = ybase + passo*j;
                posicoes[i, j, 2] = -1;
            }
        return posicoes;
    }

    void instanciaPosicoes(){
        // for(int i = 0; i < qtdX; i++)
        //     for(int j = 0; j < qtdY; j++){
        //         Instantiate(pipeCurvoPrefab, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0), new Quaternion());
        //     }
        Puzzle puzzleAtual = null;
        if(dicionarioPuzzles.ContainsKey(faseAtual))
            puzzleAtual = dicionarioPuzzles[faseAtual];
        for(int i = 0; i < puzzleAtual.pipes.Length; i++){
            switch(){
                
            }
        }
    }
}
