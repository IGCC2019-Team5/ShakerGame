﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformGyro : MonoBehaviour
{
    // How long they have to shake
    public float shakingTime = 5.0f;

    public Slider sliderRef;

    // Store reference to Gyroscope
    Gyroscope gyroRef;
    // If Gryo is working
    bool isGyroEnabled;
    // Stores how much the player shakes
    float shakeAmount;
    // testing bool
    bool canShake = false;
    // elapsed time
    float timeElapsed = 0.0f;

    // Store phone rotation
    Quaternion phoneRotation;

    // Start is called before the first frame update
    void Start()
    {
        isGyroEnabled = EnableGyro();
        //phoneRotation = new Quaternion;
    }

    // Update is called once per frame
    void Update()
    {
        // If there isnt gyro, return
        if (!isGyroEnabled)
            return;

        // Shake amount is from 0 to 10
        // multiply by 10 and divide by 100 to get 0 - 1 value
        sliderRef.value = (shakeAmount * 10) / 100;

        // If shaking isnt on
        if (!canShake)
            return;

        // Elapsed time counter
        timeElapsed += Time.deltaTime;
        
        // if it reaches the shaking time, stop shaking
        if(timeElapsed >= shakingTime)
        {
            // Reset
            canShake = false;
            timeElapsed = 0;
            
        }
        // Stores phone rotation
        phoneRotation.w += Input.gyro.attitude.w * Time.deltaTime;
        phoneRotation.x += Input.gyro.attitude.x * Time.deltaTime;
        phoneRotation.y += Input.gyro.attitude.y * Time.deltaTime;
        phoneRotation.z += Input.gyro.attitude.z * Time.deltaTime;

        shakeAmount += Input.gyro.userAcceleration.magnitude * Time.deltaTime;
        // Clamp the value
        shakeAmount = Mathf.Clamp(shakeAmount, 0, 10);

        //Debug.Log("Shake Amount : " + shakeAmount);

    }

    bool EnableGyro()
    {

        if (SystemInfo.supportsGyroscope)
        {
            gyroRef = Input.gyro;
            gyroRef.enabled = true;
            return true;
        }

        return false;
    }

    public void enableShaking()
    {
        canShake = true;
        // Reset shake amount
        shakeAmount = 0.0f;
    }
}