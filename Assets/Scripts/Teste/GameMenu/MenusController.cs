using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusController : MonoBehaviour
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelsMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadRestartLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadNextLevel()
    {
        int lvlAtual = PlayerPrefs.GetInt(ButtonsMenuManager.IDFASEATUAL);
        lvlAtual++;
        PlayerPrefs.SetInt(ButtonsMenuManager.IDFASEATUAL, lvlAtual);
        SceneManager.LoadScene(2);
    }
}
