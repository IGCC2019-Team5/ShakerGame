using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasActive : MonoBehaviour
{
    [SerializeField]
    private UnityEvent StateBegin;
    [SerializeField]
    private UnityEvent StateEnd;
    [SerializeField]
    private GameState State;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Get();
        manager.stateChangeEvents += OnStateChanged;
    }

    void OnStateChanged(GameState oldState, GameState newState)
    {
        if (newState == State)//Previous State
        {
            StateBegin.Invoke();
        }

        if (oldState == State)//Next State
        {
            StateEnd.Invoke();
        }
    }
}
