using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GyroCalculator
{
    /// <summary>
    /// Calculates the Shake.ShakePower 
    /// Call at the end of shake recording
    /// </summary>
    public static Shake.ShakePower CalculatePower(GameSettings settings, Shake.ShakeMovie movie)
    {
        float shakeMagnitude = 0.0f;
        float zRotation = 0.0f;
        for(int i = 0; i < movie.frames.Count; ++i)
        {
            shakeMagnitude += movie.frames[i].userAcceleration.magnitude * Time.deltaTime;
            //// Increment the shake amount based on the magnitude of acceleration
            //shakeMagnitude += SystemManager.gyroRef.userAcceleration.magnitude * manager.settings.shakeResistance * Time.deltaTime;
            //// Clamp the value
            //shakeMagnitude = Mathf.Clamp(shakeMagnitude, 0, 10);

            //zRotation += ((playing.frames[i].attitude.x + playing.frames[i].attitude.y + playing.frames[i].attitude.z) * Time.deltaTime);
            zRotation += (movie.frames[i].attitude.eulerAngles.magnitude  * Time.deltaTime);
        }
        // Divide by the total number of frames
        // to get the average
        zRotation /= movie.frames.Count;
        // Apply a multiplier to adjust
        zRotation *= settings.rotationMultiplier;

        // If there is any resistance
        // add it here
        float xPower = shakeMagnitude;
        float yPower = shakeMagnitude;
        float zRot = zRotation;
        float frequency = 1 / settings.shakeDuration;
        var shakePower = new Shake.ShakePower(xPower, yPower, zRot, frequency);
        //shakePower.xPower = shakeMagnitude;
        //shakePower.yPower = shakeMagnitude;
        //shakePower.zRot = zRotation;

        //// Frequency = 1 / T
        //shakePower.xFrequency = shakePower.yFrequency = shakePower.rotFrequency = 1 / shakeDuration;

        return shakePower;
    }
}
