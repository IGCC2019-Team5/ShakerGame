using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GyroRecorder : MonoBehaviour
{
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Get();
        manager.stateChangeEvents += OnStateChanged;
    }

    void OnStateChanged(GameState oldState, GameState newState)
    {
        if (newState == GameState.RECORDING)
            manager.movie = new Shake.ShakeMovie();
        if (oldState == GameState.RECORDING)
            manager.power = GyroCalculator.CalculatePower(manager.settings, manager.movie);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If gryo isnt supported, return
        // if there isnt a recorded last movie, return
        // if there is then it successfully recorded the shake
        if (!SystemManager.isGyroEnabled)
            return;

        // Check to make sure it is shaking time
        // if false, return because it is not time to shake
        // if true, start shaking
        if (manager.state != GameState.RECORDING)
            return;

        manager.elapsedTime += Time.fixedDeltaTime;

        // Add the frames
        manager.movie.AddFrame(Shake.ShakeFrame.CreateFromGyro(SystemManager.gyroRef));

        if (manager.elapsedTime > manager.settings.shakingTime)
            manager.state = GameState.PLAYING;
    }

    public void StartGame()
    {
        manager.state = GameState.RECORDING;
    }
}
