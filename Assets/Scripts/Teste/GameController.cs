using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> btns;

    private List<GameObject> btnsAlfa = new List<GameObject>();
    private List<GameObject> btnsOmega = new List<GameObject>();

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

    [SerializeField]
    private Sprite spriteCanoTriplo;

    [SerializeField]
    private Text textContagemRegressiva;

    [SerializeField]
    private Text textLevel;

    
    private Sprite spriteRegistroAlfa;

    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private Transform puzzleFieldBG;
    [SerializeField]
    private GameObject btn;
    [SerializeField]
    private GameObject goBG;
    [SerializeField]
    private GameObject btnAlfa;

    [SerializeField]
    private Text textPoints;

    private int pontuacao = 0;

    void InicializaElementosCena()
    {
        GetButtons();
        FillPuzzle();
        FillTextLevel();
        StartCoroutine(iniciaPuzzle());
    }

    private void FillTextLevel()
    {
        textLevel.text = "LEVEL " + (idFaseAtual+1).ToString();
    }

    void GetButtons()
    {
        btns = new List<GameObject>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("pipe");

        for (int i = 0; i < objects.Length; i++)
        
            btns.Add(objects[i]);
    }

    /// <summary>
    /// Preenche as células do puzzle com os devidos sprites, utilizando Button
    /// TODO precisa-se mover esse método para a Classe AddButton, para que seja realizado tudo isso nela e já venha o Puzzle pronto pra esta
    /// </summary>
    void FillPuzzle()
    {
        
        string puzzleText   = readPuzzle(idFaseAtual).Replace("\r", "");
        string[] linhas     = puzzleText.Split('\n');

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
                case "5":
                    btns[index].GetComponent<Button>().image.sprite = spriteCanoTriplo;
                    break;

                default:
                    btns[index].GetComponent<Button>().image.sprite = spriteBG;
                    break;
            }
            index++;
        }
        StartCoroutine(verificaSuccess());
    }

    string readPuzzle(int id)
    {
        TextAsset[] puzzleText = Resources.LoadAll<TextAsset>("Puzzles");
        return puzzleText[id].text;

    }

    /// <summary>
    /// Aqui o puzzle de fato é iniciado, com uma contagem regresiva de n segundos
    /// Após isso, todos os canos Alfa do puzzle iniciam a liberação de água
    /// </summary>
    IEnumerator iniciaPuzzle()
    {
        float tempo = timeStartPuzzle + 1;
        while (tempo > 1f)
        {
            tempo           = tempo - Time.deltaTime;
            int tempoInt    = (int)tempo;
            string s        = tempoInt.ToString();
            textContagemRegressiva.text = s;
            yield return    null;
        }
        textContagemRegressiva.text = "GO!";

        foreach (GameObject btn in btnsAlfa)
        //Inicia água e a animação da água
        //start água - começou o puzzle
        {
            btn.GetComponent<Spin>().giraRegistroAlfa();
            btn.GetComponent<Spin>().PassaAgua(-1);
        }
        
    }

    private void Awake()
    {
        InstanciaBotoesPuzzle();
        InicializaElementosCena();
        
        Spin.onAguaPassando     += SpinOn;
        Spin.onGameOver         += GameOver;
        Spin.onOmegaFinished    += contaOmegaSucesso;
    }


    private void SpinOn(GameObject obj)
    {
            Spin spin = obj.GetComponent<Spin>();
            Button btn = spin.GetComponent<Button>();
            int index = Int32.Parse(btn.name);
            Spin spinProx;


            foreach (int saidaAtual in spin.saidasAtuais)
            {
                switch (saidaAtual)
                {
                    case Spin.CIMA:
                        int indexCima = index - qtdcolunas;
                        if (verificaIndex(index, saidaAtual))
                        {
                            spinProx = btns[indexCima].GetComponent<Spin>();
                            spinProx.PassaAgua(Spin.BAIXO);
                            PontuacaoMaisMais();    
                        }
                        else
                        {
                            //GAMEOVER
                            //spin atual precisa jorrar água pelo lado certo
                            Debug.Log("gameover1");
                            GameOver();
                        }
                        break;
                    case Spin.DIREITA:
                        int indexDireita = index + 1;
                        if (verificaIndex(index, saidaAtual) && btns[indexDireita] != null)
                        {
                            spinProx = btns[indexDireita].GetComponent<Spin>();
                            spinProx.PassaAgua(Spin.ESQUERDA);
                            PontuacaoMaisMais();
                        }
                        else
                        {
                            //GAMEOVER
                            //spin atual precisa jorrar água pelo lado certo
                            Debug.Log("gameover2");
                            GameOver();
                        }
                        break;
                    case Spin.BAIXO:
                        int indexBaixo = index + qtdcolunas;
                        if (verificaIndex(index, saidaAtual) && btns[indexBaixo] != null)
                        {
                            btns[indexBaixo].GetComponent<Spin>();
                            spinProx = btns[indexBaixo].GetComponent<Spin>();
                            spinProx.PassaAgua(Spin.CIMA);
                            PontuacaoMaisMais();
                        }
                        else
                        {
                            //GAMEOVER
                            //spin atual precisa jorrar água pelo lado certo
                            Debug.Log("gameover3");
                            GameOver();
                        }
                        break;
                    case Spin.ESQUERDA:
                        int indexEsquerda = index - 1;
                        if (verificaIndex(index, saidaAtual) && btns[indexEsquerda] != null)
                        {
                            spinProx = btns[indexEsquerda].GetComponent<Spin>();
                            spinProx.PassaAgua(Spin.DIREITA);
                            PontuacaoMaisMais();
                        }
                        else
                        {
                            //GAMEOVER
                            //spin atual precisa jorrar água pelo lado certo
                            Debug.Log("gameover4");
                            GameOver();
                        }
                        break;
                }

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


    private void GameOver()
    {
        /*
         * Aqui deve-se finalizar o game
         */
         if(panelGameOver != null)
            panelGameOver.SetActive(true);
    }

    IEnumerator verificaSuccess()
    {
        yield return new WaitUntil(() => contadorSuccess == btnsOmega.Count);
        //Success!!!
        panelSuccess.SetActive(true);
    }

    private void contaOmegaSucesso()
    {
        contadorSuccess++;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void InstanciaBotoesPuzzle()
    {
        idFaseAtual         = PlayerPrefs.GetInt(ButtonsMenuManager.IDFASEATUAL);
        string puzzleText   = readPuzzle(idFaseAtual).Replace("\r", "");
        string[] linhas     = puzzleText.Split('\n');
        Int32.TryParse      (linhas[0], out qtdlinhas);
        Int32.TryParse      (linhas[1], out qtdcolunas);
        timeStartPuzzle     = float.Parse(linhas[2]);
        

        for(int i = 3; i < linhas.Length; i++)
        {
            GameObject button;

            if (linhas[i].Equals("0"))
            {
                button = Instantiate(btnAlfa);
            }
            else
            {
                button = Instantiate(btn);
            }

            button.name = "" + (i-3).ToString();
            button.transform.SetParent(puzzleField, false);

            GameObject go = Instantiate(goBG);
            go.transform.SetParent(puzzleFieldBG, false);
        }

        //for (int i = 0; i < 30; i++)
        //{
        //    GameObject button = Instantiate(btn);
        //    button.name = "" + i;
        //    button.transform.SetParent(puzzleField, false);

        //    GameObject go = Instantiate(goBG);
        //    go.transform.SetParent(puzzleFieldBG, false);
        //}
    }

    public void PontuacaoMaisMais()
    {
        pontuacao = pontuacao + (int)Math.Round(Time.timeScale * 10);
        textPoints.text = "POINTS: " + pontuacao.ToString();
    }
}
