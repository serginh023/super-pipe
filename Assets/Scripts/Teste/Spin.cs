using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Spin : MonoBehaviour, IPointerClickHandler
{
    private float rotAngle = 0;
    private Vector2 pivotPoint;
    private int angle = 90;

    private Vector3 target;
    private bool isRotating;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isRotating)

            StartCoroutine(Rotating(new Vector3(0, 0, 1), 90, 0.35f));


    }

    IEnumerator Rotating(Vector3 axis, float angle, float duration)
    {
        isRotating = true;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed <= duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
        isRotating = false;
    }

    public void PassaAgua()
    {
        StartCoroutine(PassandoAgua());
    }

    IEnumerator PassandoAgua()
    {
        yield return new WaitForSeconds(10f);

        //precisa-se colocar o assets da água caindo pelo cano
    }

}