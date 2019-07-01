using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spin : MonoBehaviour, IPointerClickHandler
{
    public  List<int>   saidasAtuais = new List<int>();
    public  int         entradaAtual = 0;
    private string      nomePipe;
    private Vector2     pivotPoint;

    private Vector3 target;
    private bool isRotating;

    public static event Action<GameObject>  onAguaPassando  = delegate { };
    public static event Action              onGameOver      = delegate { };
    public static event Action              onOmegaFinished = delegate { };

    public bool isRotatingEnable   = true;
    public const int CIMA           = 0;
    public const int DIREITA        = 1;
    public const int BAIXO          = 2;
    public const int ESQUERDA       = 3;

    float tempoEsperaRegistroAnimacao = 1f;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isRotating && isRotatingEnable)
            StartCoroutine(Rotating(new Vector3(0, 0, 1), 90, 0.35f));
    }

    IEnumerator Rotating(Vector3 axis, float angle, float duration)
    {
        isRotating      = true;
        Quaternion from = transform.rotation;
        Quaternion to   = transform.rotation;
        to              *= Quaternion.Euler(axis * angle);

        float elapsed   = 0.0f;
        while (elapsed <= duration)
        {
            transform.rotation  = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed             += Time.deltaTime;
            yield return null;
        }
        transform.rotation      = to;
        isRotating              = false;
    }

    public void PassaAgua(int entrada)
    {

        StartCoroutine(PassandoAgua(entrada));
    }

    IEnumerator PassandoAgua(int entrada)
    {
        isRotatingEnable    = false;
        int rotacao         = (int)transform.eulerAngles.z;

        if (entrada == -1)
        {
            switch (rotacao)
            {
                case 0:
                    saidasAtuais.Add(CIMA);
                    break;
                case 90:
                    saidasAtuais.Add(ESQUERDA);
                    break;
                case -180:
                    saidasAtuais.Add(BAIXO);
                    break;
                case 180:
                    saidasAtuais.Add(BAIXO);
                    break;
                case 270:
                    saidasAtuais.Add(DIREITA);
                    break;
                case -90:
                    saidasAtuais.Add(DIREITA);
                    break;
            }
        }
        else
        {
            saidasAtuais = verificaPassagem(entrada);

            //Debug.Log("Saída calculada: " + saidaAtual);
            if (saidasAtuais.Contains(-1))
            {
                //Falta fazer a animação da água caindo
                    onGameOver();
                    Debug.Log("Está dando gameover aqui!!! " + name);

            }
            else

                yield return new WaitForSeconds(5f);
        }

        Button btn = GetComponent<Button>();

        string nome = btn.image.sprite.name;

        Debug.Log("água está saindo do pipe: " + name + " e de rotação: " + rotacao);
        onAguaPassando(gameObject);
        //TODO precisa-se colocar o assets da água caindo pelo cano
    }


    public List<int> verificaPassagem(int entrada)
    {
        Button btn = GetComponent<Button>();

        string nome = btn.image.sprite.name;
        int coordZ = Convert.ToInt32(transform.eulerAngles.z);
        Debug.Log("Verificando passagem do pipe: " + name + " com sprite: " + nome + " e entrada: " + entrada + " e rotação: " + transform.eulerAngles.z + " coordZ: " + coordZ);
        List<int> saidas = new List<int>();

        switch (nome)
        {
            case "alfa":
                if (entrada == -1)
                    switch (coordZ)
                    {
                        case 0:
                            saidas.Add(0);
                            break;
                        case 90:
                            saidas.Add(3);
                            break;
                        case 180:
                            saidas.Add(2);
                            break;
                        case 270:
                            saidas.Add(1);
                            break;
                        default:
                            onGameOver();
                            break;

                    }
                
                else saidas.Add(-1);
                break;
            case "cruz":
                switch (entrada)
                {
                    case 0: saidas.Add(2);  break;
                    case 1: saidas.Add(3);  break;
                    case 2: saidas.Add(0);  break;
                    case 3: saidas.Add(1);  break;
                }
                break;
            case "curvo":
                switch (coordZ)
                {
                    case 0:
                        if (entrada == DIREITA)
                            saidas.Add(BAIXO);
                        else if (entrada == BAIXO)
                            saidas.Add(DIREITA);
                        else
                            saidas.Add(-1);
                        break;
                    case 90:
                        if (entrada == CIMA)
                            saidas.Add(DIREITA);
                        else if (entrada == DIREITA)
                            saidas.Add(CIMA);
                        else saidas.Add(-1);
                        break;
                    case 180:
                        if (entrada == CIMA)
                            saidas.Add(ESQUERDA);
                        else if (entrada == ESQUERDA)
                            saidas.Add(CIMA);
                        else saidas.Add(-1);
                        break;
                    case 270:
                        if (entrada == ESQUERDA)
                            saidas.Add(BAIXO);
                        else if (entrada == BAIXO)
                            saidas.Add(ESQUERDA);
                        else saidas.Add(-1);
                        break;
                    default:
                        onGameOver();
                        break;
                }
                break;

            case "reto":
                switch (coordZ)
                {
                    case 0:
                        if (entrada == ESQUERDA)
                            saidas.Add(DIREITA);
                        else if (entrada == DIREITA)
                            saidas.Add(ESQUERDA);
                        else saidas.Add(-1);
                        break;
                    case 90:
                        if (entrada == CIMA)
                            saidas.Add(BAIXO);
                        else if (entrada == BAIXO)
                            saidas.Add(CIMA);
                        else saidas.Add(-1);
                        break;
                    case 180:
                        if (entrada == ESQUERDA)
                            saidas.Add(DIREITA);
                        else if (entrada == DIREITA)
                            saidas.Add(ESQUERDA);
                        else saidas.Add(-1);
                        break;
                    case 270:
                        if (entrada == CIMA)
                            saidas.Add(BAIXO);
                        else if (entrada == BAIXO)
                            saidas.Add(CIMA);
                        else saidas.Add(-1);
                        break;
                    default:
                        onGameOver();
                        break;

                }
                //Debug.Log("adicionada saída pelo " + saidas[0]);
                break;

            case "omega":
                switch (coordZ)
                {
                    case 0:
                        if (entrada == CIMA)
                        {
                            onOmegaFinished();
                            saidas.Add(100);
                        }
                        else saidas.Add(-1);
                        break;
                    case 90:
                        if (entrada == ESQUERDA)
                        {
                            onOmegaFinished();
                            saidas.Add(100);
                        }
                        else saidas.Add(-1);
                        break;
                    case 180:
                        if (entrada == BAIXO)
                        {
                            onOmegaFinished();
                            saidas.Add(100);
                        }
                        else saidas.Add(-1);
                        break;
                    case 270:
                        if (entrada == DIREITA)
                        {
                            onOmegaFinished();
                            saidas.Add(100);
                        }
                        else saidas.Add(-1);
                        break;
                    default:
                        onGameOver();
                        break;
                }
                break;
            case "triplo":
                switch (coordZ)
                {
                    case 0:
                        if (entrada == ESQUERDA)
                        {
                            saidas.Add(DIREITA);
                            saidas.Add(BAIXO);
                        }else if(entrada == BAIXO)
                        {
                            saidas.Add(DIREITA);
                            saidas.Add(ESQUERDA);
                        }else if(entrada == DIREITA)
                        {
                            saidas.Add(ESQUERDA);
                            saidas.Add(BAIXO);
                        }
                        break;
                    case 90:
                        if (entrada == BAIXO)
                        {
                            saidas.Add(DIREITA);
                            saidas.Add(CIMA);
                        }
                        else if (entrada == DIREITA)
                        {
                            saidas.Add(BAIXO);
                            saidas.Add(CIMA);
                        }
                        else if (entrada == CIMA)
                        {
                            saidas.Add(DIREITA);
                            saidas.Add(BAIXO);
                        }
                        else saidas.Add(-1);
                        break;
                    case 180:
                        if (entrada == ESQUERDA)
                        {
                            saidas.Add(CIMA);
                            saidas.Add(DIREITA);
                        }
                        else if (entrada == CIMA)
                        {
                            saidas.Add(ESQUERDA);
                            saidas.Add(DIREITA);
                        }
                        else if (entrada == DIREITA)
                        {
                            saidas.Add(ESQUERDA);
                            saidas.Add(CIMA);
                        }
                        else saidas.Add(-1);
                        break;
                    case 270:
                        if (entrada == ESQUERDA)
                        {
                            saidas.Add(CIMA);
                            saidas.Add(BAIXO);
                        }
                        else if (entrada == CIMA)
                        {
                            saidas.Add(ESQUERDA);
                            saidas.Add(BAIXO);
                        }
                        else if (entrada == BAIXO)
                        {
                            saidas.Add(ESQUERDA);
                            saidas.Add(CIMA);
                        }
                        else saidas.Add(-1);
                        break;
                    default:
                        onGameOver();
                        break;
                }
                break;
            default:
                saidas.Add(-1);
                break;
        }

        return saidas;
    }

    public void giraRegistroAlfa()
    {
        StartCoroutine( GiraRegistro() );
    }

    IEnumerator GiraRegistro()
    {
        StartCoroutine(Rotate(new Vector3(0, 0, 1), 45, 0.5f));

        yield return new WaitForSeconds(tempoEsperaRegistroAnimacao);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), 45, 0.5f));

        yield return new WaitForSeconds(tempoEsperaRegistroAnimacao);

        StartCoroutine(Rotate(new Vector3(0, 0, 1), 45, 0.5f));

        yield return new WaitForSeconds(tempoEsperaRegistroAnimacao);
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration)
    {
        yield return null;
        Image[] imgs = GetComponentsInChildren<Image>();
        Transform t = imgs[1].transform;

        isRotating = true;
        Quaternion from = t.rotation;
        Quaternion to = t.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed <= duration)
        {
            t.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        t.rotation = to;
        isRotating = false;
    }

}