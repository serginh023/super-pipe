using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlfaRegistro : MonoBehaviour
{

    float tempoEsperaRegistroAnimacao = 1f;
    bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(giraRegistro());
    }

    IEnumerator giraRegistro()
    {
        //yield return new WaitForSeconds(5f);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), -45, 0.5f));

        yield return new WaitForSeconds(tempoEsperaRegistroAnimacao);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), -45, 0.5f));

        yield return new WaitForSeconds(tempoEsperaRegistroAnimacao);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), -45, 0.5f));

        yield return new WaitForSeconds(tempoEsperaRegistroAnimacao);
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration)
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
}
