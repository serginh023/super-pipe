using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pipe : MonoBehaviour
{
    private bool        isRotating      = false;
    public float        smooth          = 1f;
    private Quaternion  targetRotation;
    public UnityEvent   OnPipeWaterStarts;
      
    void Start(){
        targetRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        verificaPipe();
        /*
        if (!isRotating)
        {
            int flag = 0;
            if (Input.GetMouseButtonDown(0))
            {
                flag = 1;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                flag = -1;
            }
            StartCoroutine(Rotate(new Vector3(0, 0, 1), 90 * flag, 0.35f));
        }
        Debug.Log("**" + gameObject.name + "**");*/
    }

    IEnumerator Rotate( Vector3 axis, float angle, float duration)
    {
        isRotating      = true;
        Quaternion from = transform.rotation;
        Quaternion to   = transform.rotation;
        to              *= Quaternion.Euler( axis * angle );
        
        float elapsed = 0.0f;
        while( elapsed <= duration )
        {   
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration );
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
        isRotating = false;
    }

    /*
    CÓDIGO DE MODIFICAR ESCALA
    https://answers.unity.com/questions/805199/how-do-i-scale-a-gameobject-over-time.html
     */

    int verificaEntradaSaida(int entrada)
    {
        Transform transform = GetComponent<Transform>();
        string name = gameObject.name;
        int saida = -1;
        double rotation = Math.Truncate(transform.rotation.z);
        Debug.Log("Rotacao: " + rotation);

        switch (name)
        {
            
            case "cano-alfa-2(Clone)":
                switch (rotation)
                {
                    case 0:
                        saida = 0;
                        break;
                    case 90:
                        saida = 3;
                        break;
                    case 180:
                        saida = 2;
                        break;
                    case -90:
                        saida = 1;
                        break;

                }
                break;

            case "cano-reto-1.2(Clone)":
                switch (rotation)
                {
                    case 0:
                }

        }
        
    }

}
