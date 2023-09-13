using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    void Awake()
    {
        ScoreCount.resetScore();
    }
    public void resetGameScore()
    {
        ScoreCount.resetScore();
        // SceneManager.LoadScene(GameManajer.getInstance().mapList[0]);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }


}
