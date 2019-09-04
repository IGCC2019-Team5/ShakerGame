using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shake
{
    public struct ShakeFrame
    {
        public readonly Vector3 userAcceleration;
        public readonly Quaternion attitude;

        public ShakeFrame(Vector3 translation, Quaternion rotation)
        {
            this.userAcceleration = translation;
            this.attitude = rotation;
        }

        public static ShakeFrame CreateFromGyro(Gyroscope gyro)
        {
            return new ShakeFrame(gyro.userAcceleration, gyro.attitude);
        }
    }

    public class ShakeMovie
    {
        public readonly List<ShakeFrame> frames = new List<ShakeFrame>();

        public ShakeMovie(List<ShakeFrame> frames)
        {
            this.frames = frames;
        }
    }

    public class ShakeMovieBuilder
    {
        List<ShakeFrame> frames = new List<ShakeFrame>();
        float startTime;

        // Add frame to movie
        public void AddFrame(ShakeFrame frame)
        {
            frames.Add(frame);
        }

        // Create ShakeMovie Instance
        public ShakeMovie Build()
        {
            return new ShakeMovie(frames);
        }
    }

    [System.Serializable]
    public struct ShakeWave
    {
        public float freq;
        public float power;
    }

    [System.Serializable]
    public struct ShakePower
    {
        public ShakeWave x, y;
        public ShakeWave rotZ;

        public ShakePower(ShakeWave x, ShakeWave y, ShakeWave rotZ)
        {
            this.x = x;
            this.y = y;
            this.rotZ = rotZ;
        }

        public ShakePower(float _xPower, float _yPower, float _zRot, float _frequency)
        {
            this.x = new ShakeWave() { power = _xPower, freq = _frequency };
            this.y = new ShakeWave() { power = _yPower, freq = _frequency };
            this.rotZ = new ShakeWave() { power = _zRot, freq = _frequency };
        }
    }
}
