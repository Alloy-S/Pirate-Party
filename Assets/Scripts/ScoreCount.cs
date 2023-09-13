using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public static ScoreCount instance;
    public Image Boat1, Boat2;
    public GameObject redWin, blueWin;

    public static ScoreCount getInstance()
    {
        return instance;
    }
    void Awake()
    {
        // resetScore();
        instance = this;
        string jsonString = FileHandler.ReadFromJSON("PlayerScore.json");
        Debug.Log(jsonString);
        if (jsonString == "")
        {
            resetScore();
            Debug.Log("reset");
        }
        else
        {
            ScorePos scores = JsonUtility.FromJson<ScorePos>(jsonString);
            Boat1.transform.position = new Vector3(Boat1.transform.position.x + (110f * scores.playerList[0].score), Boat1.transform.position.y, 0f);
            Boat2.transform.position = new Vector3(Boat2.transform.position.x + (110f * scores.playerList[1].score), Boat2.transform.position.y, 0f);

        }


    }

    public bool updateScore(int Boat1Score, int Boat2Score)
    {
        string jsonString = FileHandler.ReadFromJSON("PlayerScore.json");
        ScorePos scores = JsonUtility.FromJson<ScorePos>(jsonString);

        scores.playerList[0].score += Boat1Score;
        scores.playerList[1].score += Boat2Score;

        jsonString = JsonUtility.ToJson(scores);
        FileHandler.SaveToJSON<string>(jsonString, "PlayerScore.json");
        if (scores.playerList[0].score >= 4)
        {
            blueWin.SetActive(true);
            return true;
        }
        else if (scores.playerList[1].score >= 4)
        {
            redWin.SetActive(true);
            return true;
        }
        else
        {

            return false;
        }
    }

    public static void updateTeamName(string teamName, int teamid) {
        string jsonString = FileHandler.ReadFromJSON("PlayerScore.json");
        ScorePos scores = JsonUtility.FromJson<ScorePos>(jsonString);
        if (teamid == 1) {
            scores.playerList[0].name = teamName;
        } else if(teamid == 2) {
            scores.playerList[1].name = teamName;
        }
    
        jsonString = JsonUtility.ToJson(scores);
        FileHandler.SaveToJSON<string>(jsonString, "PlayerScore.json");
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

    public static void resetScore()
    {
        ScorePos scores = new ScorePos();
        scores.playerList.Add(new PlayerInfo("Boat1", 0f));
        scores.playerList.Add(new PlayerInfo("Boat2", 0f));
        string json = JsonUtility.ToJson(scores);
        Debug.Log(json);
        FileHandler.SaveToJSON<String>(json, "PlayerScore.json");
    }
}
