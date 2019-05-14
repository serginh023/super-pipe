using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    float [,,] posicoes;
    float xInicial = -2.4f;
    float yInicial =  2.7f;
    int qtdX = 6;
    int qtdY = 5;
    public Transform pipeRetoPrefab;
    public Transform pipeCurvoPrefab;
    public Transform pipeCruzPrefab;
    public Transform fundo;

    string dataPath;
    public int faseAtual = 0;

    private Dictionary<int, Puzzle> dicionarioPuzzles;


    // Start is called before the first frame update
    void Start()
    {
        dicionarioPuzzles = new Dictionary<int, Puzzle>();

        List<Puzzle> listaPuzzles = Utils.consomeDicionario();

        posicoes = criaVetorPosicoes(xInicial, yInicial, qtdX, qtdY);

        foreach(Puzzle puzzle in listaPuzzles)
            dicionarioPuzzles.Add(puzzle.id, puzzle);
        
        instanciaPosicoes(faseAtual);
        
    }

    float[,,] criaVetorPosicoes(float x, float y, int qtdX, int qtdY){
        float xbase = x;
        float ybase = y;
        float[, , ] posicoes = new float[qtdX, qtdY, 2];

        float passo = 1.2f;

        for(int i = 0; i < qtdX; i++)
            for(int j = 0; j < qtdY; j++){
                posicoes[i, j, 0] = xbase + passo*i;
                posicoes[i, j, 1] = ybase - passo*j;
            }
        return posicoes;
    }

    void instanciaPosicoes(int idFase){
        Puzzle puzzleAtual = null;
        if (dicionarioPuzzles.ContainsKey(idFase))
        {
            if (dicionarioPuzzles.TryGetValue(idFase, out puzzleAtual)) ;
            else Debug.Log("deu ruim 1");

            for (int i = 0; i < qtdX; i++)
                for (int j = 0; j < qtdY; j++)
                {
                    Instantiate(fundo, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 1), Quaternion.Euler(new Vector3(0, 0, 0)));

                    switch (puzzleAtual.pipes[i, j])
                    {
                        case 2:
                            Instantiate(pipeRetoPrefab, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0), sorteioRotacao());
                            break;
                        case 3:
                            Instantiate(pipeCurvoPrefab, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0), sorteioRotacao());
                            break;
                        case 4:
                            Instantiate(pipeCruzPrefab, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0), sorteioRotacao());
                            break;
                    }
                }
        }
        else Debug.Log("Deu ruim 2");
        
    }

    Quaternion sorteioRotacao()
    {
        int i = Random.Range(1, 4);

        switch (i)
        {
            case 1: return Quaternion.Euler(new Vector3(0, 0, 0));
                
            case 2: return Quaternion.Euler(new Vector3(0, 0, 90));

            case 3: return Quaternion.Euler(new Vector3(0, 0, 180));

            case 4: return Quaternion.Euler(new Vector3(0, 0, 270));

            default: return Quaternion.Euler(new Vector3(0, 0, 90));

        }
    }
}
