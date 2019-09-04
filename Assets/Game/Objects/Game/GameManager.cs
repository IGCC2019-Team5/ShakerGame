using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    
    INIT,
    BUILDING,
    RECORDING,
    PLAYING,
    FINISHED,
}

public class GameManager : MonoBehaviour
{
    public delegate void StateChangeEvent(GameState oldState, GameState newState);
    public event StateChangeEvent stateChangeEvents;

    public GameSettings settings;
    public float elapsedTime;
    [SerializeField]
    GameState _state = GameState.INIT;
    public GameState state {
        set
        {
            stateChangeEvents.Invoke(_state, value); 
            _state = value;
        }
        get
        {
            return _state;
        }
    }
    public Shake.ShakeMovie movie;
    public Shake.ShakePower power;
    public ShakeChart chart;

    private void Start()
    {
        stateChangeEvents += OnStateChanged;
        StartCoroutine(OnStarting());
    }

    private IEnumerator OnStarting()
    {
        yield return new WaitForEndOfFrame();
        state = GameState.BUILDING;
        yield break;
    }

    void OnStateChanged(GameState oldState, GameState newState)
    {
       elapsedTime = 0;
    }

    public static GameManager Get()
    {
        return GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
