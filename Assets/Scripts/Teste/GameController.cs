using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> btns = new List<GameObject>();

    private List<GameObject> btnsAlfa = new List<GameObject>();

    private List<GameObject> btnsOmega = new List<GameObject>();

    [SerializeField]
    TextAsset puzzle;

    private int qtdlinhas;
    private int qtdcolunas;

    [SerializeField]
    private Sprite spriteCanoReto;
    [SerializeField]
    private Sprite spriteCanoCurvo;
    [SerializeField]
    private Sprite spriteCanoCruz;
    [SerializeField]
    private Sprite spriteCanoAlfa;
    [SerializeField]
    private Sprite spriteCanoOmega;
    [SerializeField]
    private Sprite spriteBG;

    float timeStartPuzzle;

    List<PuzzleNovo> puzzles;

    [SerializeField]
    private int idFaseAtual;

    private const int CIMA      = 0;
    private const int ESQUERDA  = 3;
    private const int DIREITA   = 1;
    private const int BAIXO     = 2;

    private List<Button> aguaPassando = new List<Button>();

    [SerializeField]
    GameObject panelGameOver;

    [SerializeField]
    GameObject panelSuccess;

    private int contadorSuccess=0;

    void Start()
    {
        GetButtons();
        //AddListeners(); 
        FillPuzzle();
        StartCoroutine(iniciaPuzzle());
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("pipe");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i]);
        }
    }

    /// <summary>
    /// Preenche as células do puzzle com os devidos sprites, utilizando Button
    /// </summary>
    void FillPuzzle()
    {
        string puzzleText = readPuzzle(idFaseAtual).Replace("\r", "");
        string[] linhas = puzzleText.Split('\n');
        Int32.TryParse(linhas[0], out qtdlinhas);
        Int32.TryParse(linhas[1], out qtdcolunas);
        timeStartPuzzle = float.Parse(linhas[2]);

        int index = 0;
        for (int i = 3; i < linhas.Length; i++)
        {
            switch (linhas[i])
            {
                case "0":
                    btns[index].GetComponent<Button>().image.sprite = spriteCanoAlfa;
                    //Acrescenta o pipe na lista de canos Alfa para melhor controle
                    btnsAlfa.Add(btns[index]);
                    break;
                case "1":
                    btns[index].GetComponent<Button>().image.sprite = spriteCanoOmega;
                    //Acrescenta o pipe na lista de canos Omega para melhor controle
                    btnsOmega.Add(btns[index]);
                    break;
                case "2":
                    btns[index].GetComponent<Button>().image.sprite = spriteCanoReto;
                    break;
                case "3":
                    btns[index].GetComponent<Button>().image.sprite = spriteCanoCurvo;
                    break;
                case "4":
                    btns[index].GetComponent<Button>().image.sprite = spriteCanoCruz;
                    break;

                default:
                    btns[index].GetComponent<Button>().image.sprite = spriteBG;
                    break;
            }
            index++;
        }
        StartCoroutine(verificaSuccess());
    }

    void AddListeners()
    {
        foreach (GameObject btn in btns)
        {
            btn.GetComponent<Button>().onClick.AddListener(() => PickPipe()); 
        }
    }

    public void PickPipe()
    {
        GameObject obj  = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        //string name = obj.name;
        Button btn      = obj.GetComponent<Button>();
    }

    string readPuzzle(int id)
    {
        TextAsset[] puzzleText = Resources.LoadAll<TextAsset>("Teste");
        return puzzleText[id].text;

    }

    /// <summary>
    /// Aqui o puzzle de fato é iniciado, com uma contagem regresiva de n segundos
    /// Após isso, todos os canos Alfa do puzzle iniciam a liberação de água
    /// </summary>
    /// <returns></returns>
    IEnumerator iniciaPuzzle()
    {
        yield return new WaitForSeconds(timeStartPuzzle);
        Debug.Log("esperando: " + timeStartPuzzle + " segundos");

        Debug.Log("amanho btnsAlfa: " + btnsAlfa.Count);
        foreach (GameObject btn in btnsAlfa)
            //Inicia água e a animação da água
            //start água - começou o puzzle
            btn.GetComponent<Spin>().PassaAgua(-1);
        
    }

    private void Awake()
    {
        Spin.onAguaPassando     += SpinOn;
        Spin.onGameOver         += GameOver;
        Spin.onOmegaFinished    += contaOmegaSucesso;
    }


    private void SpinOn(GameObject obj)
    {
        Spin   spin     = obj   .GetComponent<Spin>();
        Button btn      = spin  .GetComponent<Button>();
        int    index    = Int32 .Parse(btn.name);
        Spin   spinProx;

        switch (spin.saidaAtual)
        {
            case Spin.CIMA:
                int indexCima = index - qtdcolunas;
                if (verificaIndex(index, spin.saidaAtual))
                {
                    spinProx = btns[indexCima].GetComponent<Spin>();
                        spinProx.PassaAgua(Spin.BAIXO);
                }
                else
                {
                    //GAMEOVER
                    //spin atual precisa jorrar água pelo lado certo
                }
                break;
            case Spin.DIREITA:
                int indexDireita = index + 1;
                if (verificaIndex(index, spin.saidaAtual))
                {
                    spinProx = btns[indexDireita].GetComponent<Spin>();
                        spinProx.PassaAgua(Spin.ESQUERDA);
                }
                else
                {
                    //GAMEOVER
                    //spin atual precisa jorrar água pelo lado certo
                    panelGameOver.SetActive(true);
                }
                break;
            case Spin.BAIXO:
                int indexBaixo = index + qtdcolunas;
                if (verificaIndex(index, spin.saidaAtual))
                {
                    spinProx = btns[indexBaixo].GetComponent<Spin>();
                        spinProx.PassaAgua(Spin.CIMA);
                }
                else
                {
                    //GAMEOVER
                    //spin atual precisa jorrar água pelo lado certo
                }
                break;
            case Spin.ESQUERDA:
                int indexEsquerda = index - 1;
                if (verificaIndex(index, indexEsquerda))
                {
                    spinProx = btns[indexEsquerda].GetComponent<Spin>();
                        spinProx.PassaAgua(Spin.DIREITA);
                }
                else
                {
                    //GAMEOVER
                    Debug.Log("Game Over 2 no pipe: " + index);
                    //spin atual precisa jorrar água pelo lado certo
                }
                break;
        }
    }

    private bool verificaIndex(int index, int saida)
    {


        if(index % qtdcolunas == 0)
        {
            if (saida == Spin.ESQUERDA)
                return false;
            else
                return true;
        }else if(index % qtdcolunas == qtdcolunas - 1)
        {
            if (saida == Spin.DIREITA)
            {
                return false;
            }
            return true;
        }

        if (0 <= index && index < qtdcolunas)
        {
            if (saida == Spin.CIMA)
                return false;
            else return true;
        }else if (btns.Count - qtdcolunas <= index && index < btns.Count)
        {
            if (saida == Spin.BAIXO)
                return false;
            else return true;
        }

        return true;

    }


    private void GameOver(GameObject obj)
    {
        /*
         * Aqui deve-se finalizar o game
         */
        Time.timeScale = 0;
        panelGameOver.SetActive(true);
    }

    IEnumerator verificaSuccess()
    {
        yield return new WaitUntil(() => contadorSuccess >= btnsOmega.Count);
        //Success!!!
        Time.timeScale = 0;
        panelSuccess.SetActive(true);
        Debug.Log("QTD.BTNS.OMEGA " + btnsOmega.Count);
        Debug.Log(contadorSuccess);
        
    }

    private void contaOmegaSucesso()
    {
        contadorSuccess++;
    }
}
