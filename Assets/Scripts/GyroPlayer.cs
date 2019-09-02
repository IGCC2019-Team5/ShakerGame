using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GyroPlayer : MonoBehaviour
{
    Shake.ShakeMovie playing;
    Rigidbody2D target;

    // Start is called before the first frame update
    void Start()
    {
        playing = GyroRecorder.lastMovie;
        target = GetComponent<Rigidbody2D>();
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
            Debug.Log($"Force:{frame.userAcceleration}, Torque:{deltaRotation}");
            yield return new WaitForFixedUpdate();
        }

        SceneManager.LoadScene("TestScene");

        yield break;
    }
}
