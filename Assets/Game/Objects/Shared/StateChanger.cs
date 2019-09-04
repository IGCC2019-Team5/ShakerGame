using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{
    public GameState newState;
    GameManager manager;

    private void Start()
    {
        manager = GameManager.Get();
    }

    public void ChangeState()
    {
        manager.state = newState;
    }
}
