using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputTeam : MonoBehaviour
{
    public TMP_InputField inputTeamField;
    public GameObject failedWindow, inputTeam;
    private string team1, team2;

    [System.Obsolete]
    public void enterTeam()
    {
        if (inputTeamField.text == "-")
        {
            SceneManager.LoadScene(GameManajer.getInstance().mapList[0]);

        }
        Debug.Log(inputTeamField.text);
        StartCoroutine(checkTeam());

    }

    public void hello()
    {

    }

    [System.Obsolete]
    IEnumerator checkTeam()
    {
        WWWForm form = new WWWForm();
        form.AddField("nama_tim", inputTeamField.text);
        // form.AddField("point", 1000);
        // g4jaht3rbang
        string url = "https://irgl.petra.ac.id/main/api_cek_tim";
        WWW w = new WWW(url, form);
        yield return w;

        if (w.error != null)
        {
            Debug.Log("submit gagal");
            Debug.Log(w.error);
        }
        else
        {
            if (w.isDone)
            {
                // Debug.Log(w.text);

                if (w.text == "berhasil")
                {
                    team1 = inputTeamField.text;
                    Debug.Log("nama ada");

                    if (gameObject.name == "Input Team1")
                    {
                        ScoreCount.updateTeamName(inputTeamField.text, 1);
                        inputTeam.SetActive(true);
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        ScoreCount.updateTeamName(inputTeamField.text, 2);
                        SceneManager.LoadScene(GameManajer.getInstance().mapList[0]);
                    }


                }
                else
                {
                    Debug.Log("nama tidak ada");
                    failedWindow.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }

        w.Dispose();
    }
}
