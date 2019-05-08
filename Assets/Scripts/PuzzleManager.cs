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

        List<Puzzle> listaPuzzles = Utils.consomeDicionario();

        // posicoes = criaVetorPosicoes(xInicial, yInicial, qtdX, qtdY);

        // foreach(Puzzle puzzle in listaPuzzles)
        //     dicionarioPuzzles.Add(puzzle.id, puzzle);
        
        // instanciaPosicoes();
        
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
        Puzzle puzzleAtual = null;
        if(dicionarioPuzzles.ContainsKey(faseAtual))
            puzzleAtual = dicionarioPuzzles[faseAtual];
        for(int i = 0; i < qtdX; i++)
            for(int j = 0; j < qtdY; j++)
                switch(puzzleAtual.pipes[i, j]){
                    case 2: Instantiate(pipeRetoPrefab, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0), new Quaternion(0, 0, 0, 0));
                    break;
                    case 3: Instantiate(pipeCurvoPrefab, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0), new Quaternion(0, 0, 0, 0));
                    break;
                    case 4: Instantiate(pipeCruzPrefab, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0), new Quaternion(0, 0, 0, 0));
                    break;

                }
            
        
    }
}
