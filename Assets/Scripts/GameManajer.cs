using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManajer : MonoBehaviour
{

    public static GameManajer instance;
    private GameState state;
    public string[] mapList;

    public static GameManajer getInstance()
    {
        return instance;
    }

    void Awake()
    {
        
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        state = GameState.playing;
    }

    public void updateGameState(GameState newState) {
        state = newState;
    }

    public GameState getGameState() {
        return state;
    }


}

public enum GameState
{
    playing, 
    pause,
    countScore,
    waitingToStart
}

