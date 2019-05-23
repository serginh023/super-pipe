using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Pipe : MonoBehaviour
{
    private bool    isRotating = false;
    private Quaternion targetRotation;
    public  float   mooth = 1f;
    public  UnityEvent OnPipeWaterStarts;
    public  int     posicaoX;
    public  int     posicaoY;
    private int     posicaoSaida;

    public  const int SAIDA_CORRETA     = -10;
    public  const int SAIDA_ERRADA      = -20;
    public  const int SAIDA_ESQUERDA    = 3;
    public  const int SAIDA_BAIXO       = 2;
    public  const int SAIDA_DIREITA     = 1;
    public  const int SAIDA_CIMA        = 0;

    private int     saida = -1;
    private bool podeRotacionar = true;

    public static event Action<int> PipeSaida = delegate { };


    void Start()
    {
        targetRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        int entrada = 0;

        //verificaSaida(entrada);

        if (!isRotating && podeRotacionar)
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
        //Debug.Log("**" + gameObject.name + "**");
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

    /*
    CÓDIGO DE MODIFICAR ESCALA
    https://answers.unity.com/questions/805199/how-do-i-scale-a-gameobject-over-time.html
     */

    /// <summary>
    /// Verifica se a entrada de água do pipe atual é válida
    /// </summary>
    /// <param name="entrada">Qual lado a água vai entrar: cima, direita, baixo, esquerda</param>
    /// <returns>Qual o lado que a água vai sair: cima, direita, baixo, esquerda, ou (-1) caso não possa entrar no cano por causa da rotação dele</returns>
    int verificaSaida(int entrada)
    {
        Transform transform = GetComponent<Transform>();
        string name = gameObject.name;
       
        double rotation = Math.Truncate(transform.rotation.z);

        switch (name)
        {
            case "cano-alfa-2(Clone)":
                switch (rotation)
                {
                    case 0:
                        saida = SAIDA_CIMA;
                        break;
                    case 90:
                        saida = SAIDA_ESQUERDA;
                        break;
                    case 180:
                        saida = SAIDA_BAIXO;
                        break;
                    case -90:
                        saida = SAIDA_DIREITA;
                        break;
                }
                break;

            case "cano-reto-1.2(Clone)":
                switch (rotation)
                {
                    case 0:
                        if (entrada == SAIDA_ESQUERDA)
                            saida = SAIDA_DIREITA;
                        else if (entrada == SAIDA_DIREITA)
                            saida = SAIDA_ESQUERDA;
                        else return SAIDA_ERRADA;
                        break;
                    case -180:
                        if (entrada == SAIDA_ESQUERDA)
                            saida = SAIDA_DIREITA;
                        else if (entrada == SAIDA_DIREITA)
                            saida = SAIDA_ESQUERDA;
                        else return SAIDA_ERRADA;
                        break;
                    case 90:
                        if (entrada == SAIDA_CIMA)
                            saida = SAIDA_BAIXO;
                        else if (entrada == SAIDA_BAIXO)
                            saida = SAIDA_CIMA;
                        else return SAIDA_ERRADA;
                        break;
                    case -90:
                        if (entrada == SAIDA_CIMA)
                            saida = SAIDA_BAIXO;
                        else if (entrada == SAIDA_BAIXO)
                            saida = SAIDA_CIMA;
                        else return SAIDA_ERRADA;
                        break;
                    default:
                        return SAIDA_ERRADA;
                }
                break;

            case "cano-curvo-2.1-01(Clone)":
                switch (rotation)
                {
                    case 0:
                        if (entrada == SAIDA_BAIXO)
                            saida = SAIDA_DIREITA;
                        else if (entrada == SAIDA_DIREITA)
                            saida = SAIDA_BAIXO;
                        else return SAIDA_ERRADA;
                        break;
                    case 90:
                        if (entrada == SAIDA_DIREITA)
                            saida = SAIDA_CIMA;
                        else if (entrada == SAIDA_CIMA)
                            saida = SAIDA_DIREITA;
                        else return SAIDA_ERRADA;
                        break;
                    case -180:
                        if (entrada == SAIDA_CIMA)
                            saida = SAIDA_ESQUERDA;
                        else if (entrada == SAIDA_ESQUERDA)
                            saida = SAIDA_CIMA;
                        else return SAIDA_ERRADA;
                        break;
                    case -90:
                        if (entrada == SAIDA_ESQUERDA)
                            saida = SAIDA_BAIXO;
                        else if (entrada == SAIDA_BAIXO)
                            saida = SAIDA_ESQUERDA;
                        else return SAIDA_ERRADA;
                        break;
                    default:
                        return SAIDA_ERRADA;
                }
                break;

            case "cano-cruz-2(Clone)":
                if (rotation == 0 || rotation == 90 || rotation == -180 || rotation == -90)
                {
                    if (entrada == SAIDA_CIMA)
                        saida = SAIDA_BAIXO;
                    else if (entrada == SAIDA_BAIXO)
                        saida = SAIDA_CIMA;
                    else if (entrada == SAIDA_DIREITA)
                        saida = SAIDA_ESQUERDA;
                    else if (entrada == SAIDA_ESQUERDA)
                        saida = SAIDA_DIREITA;
                }
                break;

            case "cano-omega-01(Clone)":
                switch (rotation)
                {
                    case 0:
                        if (entrada == SAIDA_CIMA)
                            saida = SAIDA_CORRETA;
                        else saida = SAIDA_ERRADA;
                        break;
                    case 90:
                        if (entrada == SAIDA_ESQUERDA)
                            saida = SAIDA_CORRETA;
                        else saida = SAIDA_ERRADA;
                        break;
                    case -180:
                        if (entrada == SAIDA_BAIXO)
                            saida = SAIDA_CORRETA;
                        else saida = SAIDA_ERRADA;
                        break;
                    case -90:
                        if (entrada == SAIDA_DIREITA)
                            saida = SAIDA_CORRETA;
                        else saida = SAIDA_ERRADA;
                        break;
                }
                break;

        }
        return -1;
    }




}