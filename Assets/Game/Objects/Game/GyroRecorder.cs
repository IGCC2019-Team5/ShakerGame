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
        {
            manager.movie = new Shake.ShakeMovie();
        }
        if (oldState == GameState.RECORDING)
        {
            SoundManager.sm_Instance.PlayShaking(false);
            manager.power = GyroCalculator.CalculatePower(manager.settings, manager.movie);
        }
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

        // Make sure its moving
        if (SoundManager.sm_Instance != null)
            if (SystemManager.gyroRef.userAcceleration.magnitude > 1)
            {
                SoundManager.sm_Instance.PlayShaking(true, 1);
            }
            else
            {
                SoundManager.sm_Instance.PlayShaking(false, 1);

            }

        // Add the frames
        manager.movie.AddFrame(Shake.ShakeFrame.CreateFromGyro(SystemManager.gyroRef));

        if (manager.elapsedTime > manager.settings.shakingTime)
            manager.state = GameState.PLAYING;

        if (manager.chart != null)
        {
            manager.chart.UpdateChart(manager.settings, GyroCalculator.CalculatePower(manager.settings, manager.movie), true);
        }
    }

    public void StartGame()
    {
        manager.state = GameState.RECORDING;
    }
}
