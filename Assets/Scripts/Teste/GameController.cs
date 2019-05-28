using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<Image> imgs = new List<Image>();
    private List<Button> btns = new List<Button>();

    private List<Image> imgsAlfa = new List<Image>();
    private List<Button> btnsAlfa = new List<Button>();

    private List<Image> imgsOmega = new List<Image>();
    private List<Button> btnsOmega = new List<Button>();

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

    void Start()
    {
        GetImages();
        FillPuzzle();

    }

    void GetImages()
    {
        //GameObject[] objects = GameObject.FindGameObjectsWithTag("pipeGameObject");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("pipe");

        //Debug.Log("length: " + objects.Length);

        for (int i = 0; i < objects.Length; i++)
        {
            //imgs.Add(objects[i].GetComponent<Image>());
            //imgs[i].sprite = bgSprite;
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = spriteCanoAlfa;
        }
    }

    /// <summary>
    /// Preenche oas células do puzzle com os devidos sprites
    /// </summary>
    //void fillPuzzle()
    //{

    //    string puzzleText = puzzle.text.Replace("\r", "");
    //    string[] linhas = puzzleText.Split('\n');
    //    Int32.TryParse(linhas[0], out qtdlinhas);
    //    Int32.TryParse(linhas[1], out qtdcolunas);
    //    timeStartPuzzle =  float.Parse(linhas[3]);

    //    int index = 0;
    //    for (int i = 3; i < linhas.Length; i++)
    //    {
    //        switch (linhas[i])
    //        {
    //            case "0":
    //                imgs[index].sprite = spriteCanoAlfa;
    //                imgsAlfa.Add(imgs[index]);
    //                break;
    //            case "1":
    //                imgs[index].sprite = spriteCanoOmega;
    //                imgsOmega.Add(imgs[index]);
    //                break;
    //            case "2":
    //                imgs[index].sprite = spriteCanoReto;
    //                break;
    //            case "3":
    //                imgs[index].sprite = spriteCanoCurvo;
    //                break;
    //            case "4":
    //                imgs[index].sprite = spriteCanoCruz;
    //                break;

    //            default:
    //                imgs[index].sprite = spriteBG;
    //                break;
    //        }
    //        index++;
    //    }
    //}


    /// <summary>
    /// Preenche as células do puzzle com os devidos sprites, utilizando Button
    /// </summary>
    void FillPuzzle()
    {
        string puzzleText = puzzle.text.Replace("\r", "");
        string[] linhas = puzzleText.Split('\n');
        Int32.TryParse(linhas[0], out qtdlinhas);
        Int32.TryParse(linhas[1], out qtdcolunas);
        timeStartPuzzle = float.Parse(linhas[3]);

        int index = 0;
        for (int i = 3; i < linhas.Length; i++)
        {
            switch (linhas[i])
            {
                case "0":
                    btns[index].image.sprite = spriteCanoAlfa;
                    //Acrescenta o pipe na lista de canos Alfa para melhor controle
                    btnsAlfa.Add(btns[index]);
                    break;
                case "1":
                    btns[index].image.sprite = spriteCanoOmega;
                    //Acrescenta o pipe na lista de canos Omega para melhor controle
                    btnsOmega.Add(btns[index]);
                    break;
                case "2":
                    imgs[index].sprite = spriteCanoReto;
                    btns[index].image.sprite = spriteCanoReto;
                    break;
                case "3":
                    imgs[index].sprite = spriteCanoCurvo;
                    btns[index].image.sprite = spriteCanoCurvo;
                    break;
                case "4":
                    imgs[index].sprite = spriteCanoCruz;
                    btns[index].image.sprite = spriteCanoCruz;
                    break;

                default:
                    imgs[index].sprite = spriteBG;
                    btns[index].image.sprite = spriteBG;
                    break;
            }
            index++;
        }
    }

    IEnumerator iniciaPuzzle()
    {
        yield return new WaitForSeconds(5f);

        foreach(Image img in imgsAlfa)
        {
            
        }
    }

}
