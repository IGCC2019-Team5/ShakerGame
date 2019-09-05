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
        float xShake = 0.0f;
        float yShake = 0.0f;
        float zRotShake = 0.0f;
        int xShakeCount = 0;
        int yShakeCount = 0;
        int zRotShakeCount = 0;
        int xShakeFrame = 0;
        int yShakeFrame = 0;
        int zRotShakeFrame = 0;
        for (int i = 0; i < movie.frames.Count; ++i)
        {
            Quaternion rotation = movie.frames[i].attitude;
            Quaternion lastRotation = i > 0 ? movie.frames[i - 1].attitude : Quaternion.identity;
            Quaternion last2Rotation = i > 1 ? movie.frames[i - 2].attitude : Quaternion.identity;
            Vector3 acceleration = movie.frames[i].userAcceleration;
            Vector3 lastAcceleration = i > 0 ? movie.frames[i - 1].userAcceleration : Vector3.zero;

            // Calculate power
            float xShakeTmp, yShakeTmp, zRotShakeTmp;
            {
                var lastVec = new Vector2(lastAcceleration.z, lastAcceleration.y);
                var vec = new Vector2(acceleration.z, acceleration.y);
                if (Vector2.Dot(lastVec, vec) < 0)
                    xShakeCount++;
                xShakeTmp = vec.magnitude;
            }
            {
                var lastVec = new Vector2(lastAcceleration.x, 0);
                var vec = new Vector2(acceleration.x, 0);
                if (Vector2.Dot(lastVec, vec) < 0)
                    yShakeCount++;
                yShakeTmp = vec.magnitude;
            }
            {
                var diffRotation = rotation * Quaternion.Inverse(lastRotation);
                var vec = (diffRotation * Vector3.up) - Vector3.up;
                var diff2Rotation = lastRotation * Quaternion.Inverse(last2Rotation);
                var vec2 = (diff2Rotation * Vector3.up) - Vector3.up;
                if (Vector2.Dot(vec, vec2) < 0)
                    zRotShakeCount++;
                zRotShakeTmp = Quaternion.Angle(rotation, lastRotation);
            }

            // Multiplier
            xShakeTmp *= settings.yMultiplier;
            yShakeTmp *= settings.xMultiplier;
            zRotShakeTmp *= settings.rotationMultiplier;
            var xShakeTimeTmp = 1;
            var yShakeTimeTmp = 1;
            var zRotShakeTimeTmp = 1;

            // Choose biggest one
            if (xShakeTmp < yShakeTmp || xShakeTmp < zRotShakeTmp)
            {
                xShakeTmp = 0;
                xShakeTimeTmp = 0;
            }
            if (yShakeTmp < xShakeTmp || yShakeTmp < zRotShakeTmp)
            {
                yShakeTmp = 0;
                yShakeTimeTmp = 0;
            }
            if (zRotShakeTmp < yShakeTmp || zRotShakeTmp < xShakeTmp)
            {
                zRotShakeTmp = 0;
                zRotShakeTimeTmp = 0;
            }

            // Add value
            xShake += xShakeTmp;
            yShake += yShakeTmp;
            zRotShake += zRotShakeTmp;

            xShakeFrame += xShakeTimeTmp;
            yShakeFrame += yShakeTimeTmp;
            zRotShakeFrame += zRotShakeTimeTmp;
        }
        // Apply a multiplier to adjust
        xShake *= settings.totalMultiplier;
        yShake *= settings.totalMultiplier;
        zRotShake *= settings.totalMultiplier;

        float xShakeTime = (float)xShakeFrame / (float)movie.frames.Count * settings.shakingTime;
        float yShakeTime = (float)yShakeFrame / (float)movie.frames.Count * settings.shakingTime;
        float zRotShakeTime = (float)zRotShakeFrame / (float)movie.frames.Count * settings.shakingTime;

        var maxValue = Mathf.Max(xShake, yShake, zRotShake);
        if (maxValue > 1)
        {
            xShake /= maxValue;
            yShake /= maxValue;
            zRotShake /= maxValue;
        }

        // If there is any resistance
        // add it here
        float xPower = xShake;
        float yPower = yShake;
        float zRot = zRotShake;
        // Frequency = 1 / T
        float xFrequency = xShakeCount / xShakeTime;
        float yFrequency = yShakeCount / yShakeTime;
        float zRotFrequency = zRotShakeCount / zRotShakeTime;

        var shakePower = new Shake.ShakePower()
        {
            x = new Shake.ShakeWave() { power = xShake, count = xShakeCount, time = xShakeTime },
            y = new Shake.ShakeWave() { power = yShake, count = yShakeCount, time = yShakeTime },
            rotZ = new Shake.ShakeWave() { power = zRotShake, count = zRotShakeCount, time = zRotShakeTime },
        };

        return shakePower;
    }
}
