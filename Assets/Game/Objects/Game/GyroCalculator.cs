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
        float shakeMagnitudeVertical = 0.0f;
        float shakeMagnitudeHorizontal = 0.0f;
        //int shakeDirectionVertical = 1;
        //int shakeDirectionHorizontal = 1;
        int turningCountHorizontal = 0;
        int turningCountVertical = 0;
        float zRotation = 0.0f;
        for (int i = 0; i < movie.frames.Count; ++i)
        {
            Quaternion rotation = movie.frames[i].attitude;
            Quaternion lastRotation = i > 0 ? movie.frames[i - 1].attitude : Quaternion.identity;
            Vector3 acceleration = movie.frames[i].userAcceleration;
            Vector3 lastAcceleration = i > 0 ? movie.frames[i - 1].userAcceleration : Vector3.zero;

            {
                var lastVec = new Vector2(lastAcceleration.x, 0);
                var vec = new Vector2(acceleration.x, 0);
                if (Vector2.Dot(lastVec, vec) < 0)
                    turningCountVertical++;
                shakeMagnitudeVertical += vec.magnitude;

            }
            {
                var lastVec = new Vector2(lastAcceleration.z, lastAcceleration.y);
                var vec = new Vector2(acceleration.z, acceleration.y);
                if (Vector2.Dot(lastVec, vec) < 0)
                    turningCountHorizontal++;
                shakeMagnitudeHorizontal += vec.magnitude;
            }
            //// Increment the shake amount based on the magnitude of acceleration
            //shakeMagnitude += SystemManager.gyroRef.userAcceleration.magnitude * manager.settings.shakeResistance * Time.deltaTime;
            //// Clamp the value
            //shakeMagnitude = Mathf.Clamp(shakeMagnitude, 0, 10);

            //zRotation += ((playing.frames[i].attitude.x + playing.frames[i].attitude.y + playing.frames[i].attitude.z) * Time.deltaTime);
            zRotation += Quaternion.Angle(rotation, lastRotation);
        }
        // Apply a multiplier to adjust
        shakeMagnitudeHorizontal *= settings.totalMultiplier * settings.xMultiplier;
        shakeMagnitudeVertical *= settings.totalMultiplier * settings.yMultiplier;
        zRotation *= settings.totalMultiplier * settings.rotationMultiplier;

        var maxValue = Mathf.Max(shakeMagnitudeHorizontal, shakeMagnitudeVertical, zRotation);
        if (maxValue > 1)
        {
            shakeMagnitudeHorizontal /= maxValue;
            shakeMagnitudeVertical /= maxValue;
            zRotation /= maxValue;
        }

        // If there is any resistance
        // add it here
        float xPower = shakeMagnitudeHorizontal;
        float yPower = shakeMagnitudeVertical;
        float zRot = zRotation;
        float xFrequency = turningCountHorizontal / settings.shakingTime;
        float yFrequency = turningCountVertical / settings.shakingTime;
        float zRotFrequency = ((turningCountHorizontal + turningCountVertical) / 2) / settings.shakingTime;
        var shakePower = new Shake.ShakePower(xPower, yPower, zRot, xFrequency, yFrequency, zRotFrequency);
        //shakePower.xPower = shakeMagnitude;
        //shakePower.yPower = shakeMagnitude;
        //shakePower.zRot = zRotation;

        //// Frequency = 1 / T
        //shakePower.xFrequency = shakePower.yFrequency = shakePower.rotFrequency = 1 / shakeDuration;

        return shakePower;
    }
}
