using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    // boolean to check if gyro is working
    public static bool isGyroEnabled;
    public static Gyroscope gyroRef;

    void Awake()
    {
        isGyroEnabled = ActivateGyro();
    }

    /// <summary>
    /// Checks on start up if gryo is supported
    /// if it is, enable gyro
    /// </summary>
    /// <returns></returns>
    public static bool ActivateGyro()
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
}
