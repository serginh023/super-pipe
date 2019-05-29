using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoAleatoria : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rotate();
    }

    void Rotate()
    {
        int i = Random.Range(1, 4);
        transform.Rotate(new Vector3(0, 0, i*90));
    }
}
