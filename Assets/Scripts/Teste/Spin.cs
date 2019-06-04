using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class Spin : MonoBehaviour, IPointerClickHandler
{
    public int saidaAtual = 0;
    public int entradaAtual = 0;
    private string nomePipe;
    private Vector2 pivotPoint;
    private int angle = 90;

    private Vector3 target;
    private bool isRotating;

    public static event Action<GameObject> onAguaPassando = delegate { };
    public static event Action<GameObject> onGameOver = delegate { };

    private bool isRotatingEnable = true;
    public const int CIMA = 0;
    public const int DIREITA = 1;
    public const int BAIXO = 2;
    public const int ESQUERDA = 3;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isRotating && isRotatingEnable)
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

    public void PassaAgua(int entrada)
    {

        StartCoroutine(PassandoAgua(entrada));
    }

    IEnumerator PassandoAgua(int entrada)
    {
        isRotatingEnable = false;

        if (entrada == -1)
            switch (transform.rotation.z)
            {
                case 0:
                    saidaAtual = CIMA;
                    break;
                case 90:
                    saidaAtual = ESQUERDA;
                    break;
                case -180:
                    saidaAtual = BAIXO;
                    break;
                case 180:
                    saidaAtual = BAIXO;
                    break;
                case -90:
                    saidaAtual = DIREITA;
                    break;

            }
        else
        {
            saidaAtual = verificaSaida(entrada);
            if(saidaAtual == -1)
            {
                onGameOver(gameObject);
            }
            else
            {
                yield return new WaitForSeconds(10f);
                onAguaPassando(gameObject);
            }
   
        }
        onAguaPassando(gameObject);
        //TODO precisa-se colocar o assets da água caindo pelo cano
    }


    public int verificaSaida(int entrada)
    {
        Button btn = GetComponent<Button>();

        string nome = btn.image.sprite.name;

        switch (nome)
        {
            case "alfa":
                switch (transform.rotation.z)
                {
                    case 0:
                        return 0;
                    case 90:
                        return 3;
                    case -180:
                        return 2;
                    case 180:
                        return 2;
                    case -90:
                        return 1;

                }
                break;
            case "cruz":
                switch (entrada)
                {
                    case 0: return 2;
                    case 1: return 3;
                    case 2: return 0;
                    case 3: return 1;
                }
                break;
            case "curvo":
                switch (transform.rotation.z)
                {
                    case 0:
                        if (entrada == 1)
                            return 2;
                        else if (entrada == 2)
                            return 1;
                        else return -1;
                        
                    case 90:
                        if (entrada == CIMA)
                            return ESQUERDA;
                        else if (entrada == ESQUERDA)
                            return CIMA;
                        else return -1;
                        
                    case 180:
                        if (entrada == CIMA)
                            return ESQUERDA;
                        else if (entrada == ESQUERDA)
                            return CIMA;
                        else return -1;
                        
                    case -180:
                        if (entrada == CIMA)
                            return ESQUERDA;
                        else if (entrada == DIREITA)
                            return CIMA;
                        else return -1;

                    case -90:
                        if (entrada == ESQUERDA)
                            return BAIXO;
                        else if (entrada == BAIXO)
                            return ESQUERDA;
                        else return -1;
                }
                break;

            case "reto":
                switch (transform.rotation.z)
                {
                    case 0:
                        if (entrada == ESQUERDA)
                            return DIREITA;
                        else if (entrada == DIREITA)
                            return ESQUERDA;
                        else return -1;
                    case 90:
                        if (entrada == CIMA)
                            return BAIXO;
                        else if (entrada == BAIXO)
                            return CIMA;
                        else return -1;
                    case -90:
                        if (entrada == CIMA)
                            return BAIXO;
                        else if (entrada == BAIXO)
                            return CIMA;
                        else return -1;
                    case 180:
                        if (entrada == ESQUERDA)
                            return DIREITA;
                        else if (entrada == DIREITA)
                            return ESQUERDA;
                        else return -1;
                    case -180:
                        if (entrada == ESQUERDA)
                            return DIREITA;
                        else if (entrada == DIREITA)
                            return ESQUERDA;
                        else return -1;

                }
                break;

            case "omega":
                switch(transform.rotation.z)
                {
                    case 0:
                        if (entrada == CIMA)
                            return 100;
                        else return -1;
                    case 90:
                        if (entrada == ESQUERDA)
                            return 100;
                        else return -1;
                    case -90:
                        if (entrada == DIREITA)
                            return 100;
                        else return -1;
                    case 180:
                        if (entrada == BAIXO)
                            return 100;
                        else return -1;
                    case -180:
                        if (entrada == BAIXO)
                            return 100;
                        else return -1;
                }
                break;
        }


        return -10;
    }

}