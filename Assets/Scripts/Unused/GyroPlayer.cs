﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GyroPlayer : MonoBehaviour
{
    public float shakeDuration = 5f;
    public float rotationMultiplier = 2f;
    public Shake.ShakePower shakePower;


    Shake.ShakeMovie playing;


    Rigidbody2D target;

    // Start is called before the first frame update
    void Start()
    {
        playing = GyroRecorder.lastMovie;
        //shakePower = new Shake.ShakePower();
        target = GetComponent<Rigidbody2D>();
        CalculatePower();
        StartCoroutine(Play(this.playing));
    }

    IEnumerator Play(Shake.ShakeMovie playing)
    {
        float lastRotation = playing.frames.Count > 0 ? playing.frames[0].attitude.eulerAngles.z : 0f;
        foreach (var frame in playing.frames)
        {
            float rotation = frame.attitude.eulerAngles.z;
            float deltaRotation = rotation - lastRotation;
            lastRotation = rotation;

            target.AddForce(frame.userAcceleration);
            target.AddTorque(deltaRotation);
            //Debug.Log($"Force:{frame.userAcceleration}, Torque:{deltaRotation}");
            yield return new WaitForFixedUpdate();
        }

        SceneManager.LoadScene("TestScene");

        yield break;
    }

    /// <summary>
    /// Calculates the Shake.ShakePower 
    /// Call at the end of shake recording
    /// </summary>
    void CalculatePower()
    {
        float horizontalMagnitude = 0.0f;
        float verticalMagnitude = 0.0f;
        float zRotation = 0.0f;
        for(int i = 0; i < playing.frames.Count; ++i)
        {
            horizontalMagnitude += Mathf.Sqrt(Vector2.SqrMagnitude(playing.frames[i].userAcceleration)) * Time.fixedDeltaTime;
            verticalMagnitude = playing.frames[i].userAcceleration.z * Time.fixedDeltaTime;
            //zRotation += ((playing.frames[i].attitude.x + playing.frames[i].attitude.y + playing.frames[i].attitude.z) * Time.deltaTime);
            zRotation += (playing.frames[i].attitude.eulerAngles.magnitude  * Time.fixedDeltaTime);
        }
        // Divide by the total number of frames
        // to get the average
        zRotation /= playing.frames.Count;
        // Apply a multiplier to adjust
        zRotation *= rotationMultiplier;

        // If there is any resistance
        // add it here
        float xPower = horizontalMagnitude;
        float yPower = verticalMagnitude;
        float zRot = zRotation;
        float frequency = 1 / shakeDuration;
        shakePower = new Shake.ShakePower(xPower, yPower, zRot, frequency);
        //shakePower.xPower = shakeMagnitude;
        //shakePower.yPower = shakeMagnitude;
        //shakePower.zRot = zRotation;

        //// Frequency = 1 / T
        //shakePower.xFrequency = shakePower.yFrequency = shakePower.rotFrequency = 1 / shakeDuration;

        Debug.Log("xPower : " + shakePower.x.power);
        Debug.Log("yPower : " + shakePower.y.power);
        Debug.Log("zRot : " + shakePower.rotZ.power);
        Debug.Log("Frequency : " + frequency);

        GameObject.Find("ShakePowerChart").GetComponent<ShakeChart>().UpdateChart(this);
    }

}