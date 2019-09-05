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
        public List<ShakeFrame> frames = new List<ShakeFrame>();

        // Add frame to movie
        public void AddFrame(ShakeFrame frame)
        {
            frames.Add(frame);
        }
    }

    [System.Serializable]
    public struct ShakeWave
    {
        public float time;
        public int count;
        public float freq { get { return count / time; } }
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
    }
}
