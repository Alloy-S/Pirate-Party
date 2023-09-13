using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreWindow : MonoBehaviour
{
    public GameObject blueWin, redWin;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        string jsonString = FileHandler.ReadFromJSON("PlayerScore.json");
        ScorePos scores = JsonUtility.FromJson<ScorePos>(jsonString);
        float blueScore = scores.playerList[0].score;
        float redScore = scores.playerList[1].score;
        scoreText.text = blueScore + " : " + redScore;

        if (blueScore >= 4) {
            blueWin.SetActive(true);
        } else if (redScore >= 4) {
            redWin.SetActive(true);
        }


    }

    public void toMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    private class ScorePos
    {
        public List<PlayerInfo> playerList = new List<PlayerInfo>();
    }

    [System.Serializable]
    private class PlayerInfo
    {
        public string name;
        public float score;
        public float point;

        public PlayerInfo(string name, float score)
        {
            this.name = name;
            this.score = score;
        }
    }
}
