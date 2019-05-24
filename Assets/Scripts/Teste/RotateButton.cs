using UnityEngine;
using System.Collections;

public class RotateButton : MonoBehaviour
{
    private float rotAngle = 0;
    private Vector2 pivotPoint;
    private int angle = 90;
    void OnGUI()
    {
        pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
        rotAngle += angle;
        
        //if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 25, 50, 50), "Rotate"))
        //    rotAngle += angle;

    }
}