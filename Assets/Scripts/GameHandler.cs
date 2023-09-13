using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject CountScoreWindow;
    public float delayTime = 2f;
    private bool player1, player2;
    public Image Boat1, Boat2;
    public int mapIndex;
    private bool IsSetTarget = false;
    private float targetPos;
    public GameObject pauseWindow;

    private bool gameIsPause = false;
    void Start()
    {
        GameManajer.getInstance().updateGameState(GameState.playing);
        Debug.Log("sizemap: " +  GameManajer.getInstance().mapList.Length);
        // Boat1.transform.position = new Vector3(110f, Boat1.transform.position.y, 0f);
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
            {
                resume();
            }
            else
            {
                pause();
            }
        }

        if (GameManajer.getInstance().getGameState() == GameState.playing)
        {
            player1 = Player1Boat.getInstance().isAlive();
            player2 = Player2Boat.getInstance().isAlive();
            // player2 = false;
            if (!player1 || !player2)
            {
                if (delayTime > 0)
                {
                    delayTime -= Time.deltaTime;
                }
                else
                {
                    // GameManajer.getInstance().updateGameState(GameState.countScore);
                    CountScoreWindow.SetActive(true);

                    if (player1)
                    {
                        Transform boat1Pos = Boat1.transform;

                        // Debug.Log(boat1Pos.position.x);
                        if (!IsSetTarget)
                        {
                            targetPos = boat1Pos.position.x + 110f;
                            IsSetTarget = true;
                        }
                        if (boat1Pos.position.x <= targetPos)
                        {
                            // boat1Pos.Translate(new Vector3(0f, 0f, 0));
                            boat1Pos.Translate(new Vector3(0f, -0.5f, 0));
                        }
                        else
                        {
                            StartCoroutine(NextRound(1, 0));
                        }
                    }
                    else
                    {
                        Transform boat2Pos = Boat2.transform;

                        // Debug.Log(boat2Pos.position.x);
                        if (!IsSetTarget)
                        {
                            targetPos = boat2Pos.position.x + 110f;
                            IsSetTarget = true;
                        }
                        if (boat2Pos.position.x <= targetPos)
                        {
                            // boat1Pos.Translate(new Vector3(0f, 0f, 0));
                            boat2Pos.Translate(new Vector3(0f, -0.5f, 0));
                        }
                        else
                        {
                            StartCoroutine(NextRound(0, 1));
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(submitScore());
        }

    }

    [System.Obsolete]
    IEnumerator submitScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("nama_tim", "g4jaht3rban");
        // form.AddField("point", 1000);
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
                Debug.Log(w.text);

                if (w.text == "berhasil")
                {

                }
                else
                {

                }
            }
        }

        w.Dispose();
    }

    IEnumerator NextRound(int boat1Score, int boat2Score)
    {
        GameManajer.getInstance().updateGameState(GameState.countScore);

        yield return new WaitForSeconds(1.5f);
        bool win = ScoreCount.getInstance().updateScore(boat1Score, boat2Score);

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (!win)
        {
            string mapName = GameManajer.getInstance().mapList[getMap()];
            Debug.Log(mapName + " sizemap: " +  GameManajer.getInstance().mapList.Length);
            SceneManager.LoadScene(mapName);
        } else {
            StartCoroutine(endingGame());
        }

    }

    IEnumerator endingGame() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("ShowFinalScore");
    }

    private int getMap()
    {
        int randomMap;
        while (true)
        {
            randomMap = Random.Range(0, GameManajer.getInstance().mapList.Length);
            if (randomMap != mapIndex)
            {
                break;
            }
        }

        return randomMap;
    }

    public void resume()
    {
        Time.timeScale = 1f;
        pauseWindow.SetActive(false);
        gameIsPause = false;
    }

    public void pause()
    {
        Time.timeScale = 0f;
        pauseWindow.SetActive(true);
        gameIsPause = true;
    }

    public void surrend() {
        SceneManager.LoadScene("MainMenu");
    }
}
