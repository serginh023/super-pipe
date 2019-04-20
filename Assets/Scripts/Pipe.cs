using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private bool isRotating = false;
    public float smooth = 1f;
      private Quaternion targetRotation;
      void Start(){
               targetRotation = transform.rotation;
      }


    void OnMouseDown(){
        //targetRotation *=  Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
        //transform.rotation= Quaternion.Lerp (transform.rotation, targetRotation , 10 * smooth * Time.deltaTime);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);
        
        if(!isRotating)
            StartCoroutine(Rotate(new Vector3(0, 0, 1), 90, 0.3f));
    }

    IEnumerator Rotate( Vector3 axis, float angle, float duration)
    {
        isRotating = true;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler( axis * angle );

        Vector3 originalScale       = transform.localScale;
        Vector3 destinationScale    = new Vector3(1.25f, 1.25f, 1.25f);
        
        float elapsed = 0.0f;
        while( elapsed < duration )
        {
        transform.rotation = Quaternion.Slerp(from, to, elapsed / duration );
        if(elapsed < duration / 2)
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, elapsed / duration);
        else
            transform.localScale = Vector3.Lerp(destinationScale, originalScale, elapsed / duration);
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


}
