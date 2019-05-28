using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private Transform puzzleFieldBG;
    [SerializeField]
    private GameObject btn;
    [SerializeField]
    private GameObject goBG;
    private void Awake()
    {
        for(int i = 0; i < 30; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);

            GameObject go = Instantiate(goBG);
            go.transform.SetParent(puzzleFieldBG, false);
        }
    }

}
