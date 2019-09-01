using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroPlayer : MonoBehaviour
{
    Shake.ShakeMovie playing;
    Rigidbody2D target;

    // Start is called before the first frame update
    void Start()
    {
        playing = GyroRecorder.lastMovie;
        target = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Play(this.playing);
    }

    IEnumerator Play(Shake.ShakeMovie playing)
    {
        foreach (var frame in playing.frames)
        {
            target.AddForce(frame.translation);
            target.AddTorque(frame.rotation.eulerAngles.z);
            yield return null;
        }

        yield break;
    }
}
