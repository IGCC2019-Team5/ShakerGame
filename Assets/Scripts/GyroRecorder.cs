using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GyroRecorder : MonoBehaviour
{
    public static Shake.ShakeMovie lastMovie = null;

    // Shaking time
    public float shakingTime = 30;
    // elapsed time
    [System.NonSerialized] public float elapsedTime;
    // Stores how much the player shakes
    [System.NonSerialized] public float shakeAmount;
    // Store reference to Gyroscope
    Gyroscope gyroRef;
    // boolean Check for shaking
    bool canShake = false;
    // boolean to check if gyro is working
    bool isGyroEnabled = false;

    Shake.ShakeMovieBuilder movie = new Shake.ShakeMovieBuilder();

    // Start is called before the first frame update
    void Start()
    {
        isGyroEnabled = CheckGyro();
        elapsedTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If gryo isnt supported, return
        // if there isnt a recorded last movie, return
        // if there is then it successfully recorded the shake
        if (!isGyroEnabled || lastMovie != null)
            return;

        // Check to make sure it is shaking time
        // if false, return because it is not time to shake
        // if true, start shaking
        if (!canShake)
            return;

        elapsedTime += Time.deltaTime;

        // Add the frames
        movie.AddFrame(Shake.ShakeFrame.CreateFromGyro(Input.gyro));

        // Increment the shake amount based on the magnitude of acceleration
        shakeAmount += gyroRef.userAcceleration.magnitude * Time.deltaTime;
        // Clamp the value
        shakeAmount = Mathf.Clamp(shakeAmount, 0, 10);

        if (elapsedTime > shakingTime)
        {
            elapsedTime = 0;
            lastMovie = movie.Build();
            // Need change this part
            // I dont think we should separate the scenes from shaking and playing
            SceneManager.LoadScene("PlayScene");
        }
    }

    /// <summary>
    /// Checks on start up if gryo is supported
    /// if it is, enable gyro
    /// </summary>
    /// <returns></returns>
    bool CheckGyro()
    {
        // if it supports gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            // Set reference
            gyroRef = Input.gyro;
            // Enable gyroscope
            gyroRef.enabled = true;
            return true;
        }

        // Gyroscope isnt enabled
        return false;
    }

    /// <summary>
    /// For the button press
    /// to enable shaking
    /// </summary>
    public void EnableShake()
    {
        // Enable canShake
        canShake = true;
        //Set elapsed time to 0
        elapsedTime = 0;
    }

    //void UpdateSlider()
    //{
    //    // if there isn't any slider reference
    //    // return
    //    if (sliderRef == null)
    //        return;

    //    // Shake amount os from 0 to 10
    //    // divide by 10 to get 0 to 1 values 
    //    sliderRef.value = shakeAmount / 10;

    //    // Increment the shake amount based on the magnitude of acceleration
    //    shakeAmount += gyroRef.userAcceleration.magnitude * Time.deltaTime;
    //    // Clamp the value
    //    shakeAmount = Mathf.Clamp(shakeAmount, 0, 10);
    //}
}
