using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsMenuManager : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private Button btn;

    private TextAsset[] puzzles;

    List<Button> btns = new List<Button>();

    public const string IDFASEATUAL = "idFaseAtual";

    private void Awake()
    {
        puzzles = Resources.LoadAll<TextAsset>("Puzzles");

        for(int i = 0; i < puzzles.Length; i++)
        {
            Button obj = Instantiate(btn); 
            obj.name    = "" + i;
            Text text   = obj.GetComponentInChildren<Text>();
            text.text   = obj.name;
            text.color  = new Color(0, 0, 255);
            obj.transform.SetParent(puzzleField, false);
            btns.Add(obj);
        }

        AddListeners();
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener( () => PickPuzzle() );
        }
    }

    public void PickPuzzle()
    {
        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        //string name = obj.name;
        Button btn = obj.GetComponent<Button>();
        int aux;
        Int32.TryParse(obj.name, out aux);
        PlayerPrefs.SetInt(IDFASEATUAL, aux);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

}
