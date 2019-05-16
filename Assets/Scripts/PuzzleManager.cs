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
    public Transform alfaRegistro;

    string dataPath;
    public int faseAtual = 0;

    private Dictionary<int, Puzzle> dicionarioPuzzles;

    bool isRotating = false;

    float alfaRegistroTempoEspera = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        dicionarioPuzzles = new Dictionary<int, Puzzle>();

        List<Puzzle> listaPuzzles = Utils.consomeDicionario();

        posicoes = criaVetorPosicoes(xInicial, yInicial, qtdX, qtdY);

        foreach(Puzzle puzzle in listaPuzzles)
            dicionarioPuzzles.Add(puzzle.id, puzzle);
        
        instanciaPosicoes(faseAtual);

        StartCoroutine(giraRegistro());
        
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

    IEnumerator giraRegistro()
    {
        yield return new WaitForSeconds(5f);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), 45, 0.5f));

        yield return new WaitForSeconds(alfaRegistroTempoEspera);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), 45, 0.5f));

        yield return new WaitForSeconds(alfaRegistroTempoEspera);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), 45, 0.5f));

        yield return new WaitForSeconds(alfaRegistroTempoEspera);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), 45, 0.5f));

        yield return new WaitForSeconds(alfaRegistroTempoEspera);
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration)
    {
        isRotating = true;
        Quaternion from = alfaRegistro.rotation;
        Quaternion to = alfaRegistro.rotation;
        to *= Quaternion.Euler(axis * angle);

        //Vector3 originalScale       = transform.localScale;
        //Vector3 destinationScale    = new Vector3(originalScale.x * 1.25f, originalScale.y * 1.25f, originalScale.z * 1.25f);

        float elapsed = 0.0f;
        while (elapsed <= duration)
        {
            alfaRegistro.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            /*
            if(elapsed <= duration / 2)
                transform.localScale = Vector3.Lerp(originalScale, destinationScale, elapsed / duration);
            else
                transform.localScale = Vector3.Lerp(destinationScale, originalScale, elapsed / duration);
            */
            elapsed += Time.deltaTime;
            yield return null;
        }
        alfaRegistro.rotation = to;
        isRotating = false;
    }
}
