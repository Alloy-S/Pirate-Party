using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedWindow : MonoBehaviour
{

    public GameObject team1, team2;
    public void back() {
        if (gameObject.name == "Input Team1") {
            team1.SetActive(true);
        } else {
            team2.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
