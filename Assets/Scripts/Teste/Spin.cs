﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spin : MonoBehaviour, IPointerClickHandler
{
    public int saidaAtual = 0;
    public int entradaAtual = 0;
    private string nomePipe;
    private Vector2 pivotPoint;

    private Vector3 target;
    private bool isRotating;

    public static event Action<GameObject>  onAguaPassando  = delegate { };
    public static event Action<GameObject>  onGameOver      = delegate { };
    public static event Action              onOmegaFinished = delegate { };

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
        isRotatingEnable = false;
        int rotacao = (int)transform.eulerAngles.z;

        if (entrada == -1)
        {
            switch (rotacao)
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
                case 270:
                    saidaAtual = DIREITA;
                    break;
                case -90:
                    saidaAtual = DIREITA;
                    break;
            }
        }
        else
        {
            saidaAtual = verificaPassagem(entrada);
            Debug.Log("Saída calculada: " + saidaAtual);
            if (saidaAtual == -1)
            {
                onGameOver(gameObject);
                Debug.Log("Está dando gameover aqui!!! " + name);
            }
            else
            
                yield return new WaitForSeconds(5f);
            

        }

        Button btn = GetComponent<Button>();

        string nome = btn.image.sprite.name;

        Debug.Log("água está saindo do pipe: " + name + " e de rotação: " + rotacao + " pela: " + saidaAtual);
        onAguaPassando(gameObject);
        //TODO precisa-se colocar o assets da água caindo pelo cano
    }


    public int verificaPassagem(int entrada)
    {
        Button btn = GetComponent<Button>();

        string nome = btn.image.sprite.name;
        int myInt = Convert.ToInt32(transform.eulerAngles.z);
        Debug.Log("Verificando passagem do pipe: " + name + "com sprite: " + nome + " e entrada: " + entrada + " e rotação: " + transform.eulerAngles.z + " myInt: " + myInt);

        switch (nome)
        {
            case "alfa":
                switch (myInt)
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
                    case 270:
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
                switch (myInt)
                {
                    case 0:
                        if (entrada == DIREITA)
                            return BAIXO;
                        else if (entrada == BAIXO)
                            return DIREITA;
                        else return -1;

                    case 90:
                        if (entrada == CIMA)
                            return DIREITA;
                        else if (entrada == DIREITA)
                            return CIMA;
                        else return -1;

                    case 180:
                        if (entrada == CIMA)
                            return ESQUERDA;
                        else if (entrada == ESQUERDA)
                            return CIMA;
                        else return -1;

                    case 270:
                        if (entrada == ESQUERDA)
                            return BAIXO;
                        else if (entrada == BAIXO)
                            return ESQUERDA;
                        else return -1;
                }
                break;

            case "reto":
                switch (myInt)
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
                    case 270:
                        if (entrada == CIMA)
                            return BAIXO;
                        else if (entrada == BAIXO)
                            return CIMA;
                        else return -1;

                }
                break;

            case "omega":
                switch (myInt)
                {
                    case 0:
                        if (entrada == CIMA)
                        {
                            onOmegaFinished();
                            return 100;
                        }
                        else return -1;
                    case 90:
                        if (entrada == ESQUERDA)
                        {
                            onOmegaFinished();
                            return 100;
                        }
                        else return -1;
                    case 180:
                        if (entrada == BAIXO)
                        {
                            onOmegaFinished();
                            return 100;
                        }
                        else return -1;
                    case 270:
                        if (entrada == DIREITA)
                        {
                            onOmegaFinished();
                            return 100;
                        }
                        else return -1;
                }
                break;
        }

        return -1;
    }

}