using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusController : MonoBehaviour
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LoadLevelsMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadRestartLevel()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void LoadNextLevel()
    {
        int lvlAtual = PlayerPrefs.GetInt(ButtonsMenuManager.IDFASEATUAL);
        lvlAtual++;
        PlayerPrefs.SetInt(ButtonsMenuManager.IDFASEATUAL, lvlAtual);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
