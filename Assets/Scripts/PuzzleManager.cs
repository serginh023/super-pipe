using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public Transform pipeAlfa;
    public Transform pipeOmega;

    private string dataPath;

    public int idFaseAtual = 1;

    private Dictionary<int, Puzzle> dicionarioPuzzles;

    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    public Text textTempo;

    private GameObject[,] puzzle;


    // Start is called before the first frame update
    void Start()
    {
        dicionarioPuzzles = new Dictionary<int, Puzzle>();

        List<Puzzle> listaPuzzles = Utilidade.consomeDicionario();

        posicoes = criaVetorPosicoes(xInicial, yInicial, qtdX, qtdY);

        puzzle = new GameObject[qtdX,qtdY];

        foreach(Puzzle puzzle in listaPuzzles)
            dicionarioPuzzles.Add(puzzle.id, puzzle);
        
        instanciaPosicoes(idFaseAtual);

        StartCoroutine(contagemRegressiva());
    }

    float[,,] criaVetorPosicoes(float x, float y, int qtdX, int qtdY){
        float xbase = x;
        float ybase = y;
        float[, , ] posicoes = new float[qtdX, qtdY, 2];

        float passo = 1.2f;

        for(int i = 0; i < qtdX; i++)
            for(int j = 0; j < qtdY; j++){
                posicoes[i, j, 0] = xbase + passo*i;//coordenada X
                posicoes[i, j, 1] = ybase - passo*j;//coordenada Y
            }
        return posicoes;
    }

    void instanciaPosicoes(int idFase){
        Puzzle puzzleAtual = null;
        if (dicionarioPuzzles.ContainsKey(idFase))
            {
            if (dicionarioPuzzles.TryGetValue(idFase, out puzzleAtual))
            {
                
            }
            else Debug.Log("deu ruim 1");

            for (int i = 0; i < qtdX; i++)
                for (int j = 0; j < qtdY; j++)
                {
                    Instantiate(fundo, new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 1), Quaternion.Euler(new Vector3(0, 0, 0)));
                    Transform gameObject;
                    GameObject go = null;

                    //instancia dos pipes no puzzle
                    switch (puzzleAtual.pipes[i, j])
                    {
                        case 0:
                            gameObject  = Instantiate(
                                pipeAlfa,
                                new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0),
                                sorteioRotacao()
                                );
                            go          = gameObject.gameObject;
                            break;
                        case 1:
                            gameObject  = Instantiate(
                                pipeOmega,
                                new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0),
                                sorteioRotacao()
                                );
                            go          = gameObject.gameObject;
                            break;
                        case 2:
                            gameObject  = Instantiate(
                                pipeRetoPrefab, 
                                new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0), 
                                sorteioRotacao()
                                );
                            go          = gameObject.gameObject;
                            break;
                        case 3:
                            gameObject  = Instantiate(
                                pipeCurvoPrefab,
                                new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0),
                                sorteioRotacao()
                                );
                            go          = gameObject.gameObject;
                            break;
                        case 4:
                            gameObject  = Instantiate(
                                pipeCruzPrefab,
                                new Vector3(posicoes[i, j, 0], posicoes[i, j, 1], 0),
                                sorteioRotacao()
                                );
                            go          = gameObject.gameObject;
                            break;
                    }
                    puzzle[i, j] = go;
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

    private IEnumerator contagemRegressiva()
    {
        float tempo=6f;
        while (tempo > 1f)
        {
            tempo = tempo - Time.deltaTime;
            int tempoInt = (int)tempo;
            string s = tempoInt.ToString();
            textTempo.text = s;
            yield return null;
        }
        textTempo.text = "GO!";

        //Precisa-se dar início a água passando pelos canos
        if (OnClicked != null)
            OnClicked();
    }

    private void Awake()
    {
        NovoPipe.PipeSaida += verificaSaidaAgua;
    }

    public void verificaSaidaAgua(int saida)
    {
        switch (saida)
        {
            case -1:
                //Game Over!
                break;
            //case 0:
            //case 1:
            //case 2:
            //case 3:
        }
    }

}
