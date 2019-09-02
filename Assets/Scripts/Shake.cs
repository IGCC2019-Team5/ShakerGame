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

    public class ShakePower
    {
        public float xPower, yPower;
        public float zRot;
        public float xFrequency, yFrequency, rotFrequency;

        public ShakePower(float _xPower, float _yPower, float _zRot, float _frequency)
        {
            xPower = _xPower;
            yPower = _yPower;
            zRot = _zRot;
            xFrequency = yFrequency = rotFrequency = _frequency;
        }
    }

}
