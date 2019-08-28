using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float times = .05f;
    Vector2 acc;
    Vector2 vel;
    Vector2 pos;

    private void Update()
    {
        vel = (Vector2)Input.gyro.userAcceleration;
        //vel += acc;
        pos += vel;
        transform.position = (Vector3)pos * times;
    }
}
