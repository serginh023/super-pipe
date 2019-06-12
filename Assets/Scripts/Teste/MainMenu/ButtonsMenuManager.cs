using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsMenuManager : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private Button btn;

    private TextAsset[] puzzles;

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
        }
    }

}
