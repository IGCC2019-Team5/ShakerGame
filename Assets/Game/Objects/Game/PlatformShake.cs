﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShake : MonoBehaviour
{
    GameManager manager;

    Rigidbody2D target;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Get();
        manager.stateChangeEvents += OnStateChanged;

        target = GetComponent<Rigidbody2D>();
    }

    void OnStateChanged(GameState oldState, GameState newState)
    {
        if (newState == GameState.PLAYING)
            manager.chart.UpdateChart(manager.power, false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (manager.state != GameState.PLAYING)
            return;

        manager.elapsedTime += Time.fixedDeltaTime;
        float x = (Mathf.Sin(Mathf.PI * 2 * manager.power.x.freq * manager.elapsedTime)/* > 0 ? 1 : -1*/) * manager.power.x.power;
        float y = (Mathf.Sin(Mathf.PI * 2 * manager.power.y.freq * manager.elapsedTime)/* > 0 ? 1 : -1*/) * manager.power.y.power;
        float rotZ = (Mathf.Sin(Mathf.PI * 2 * manager.power.rotZ.freq * manager.elapsedTime)/* > 0 ? 1 : -1*/) * manager.power.rotZ.power;
        target.velocity = new Vector2(x, y);
        target.angularVelocity = rotZ;

        if (manager.elapsedTime > manager.settings.shakingTime)
        {
            manager.state = GameState.FINISHED;
        }
    }
}
