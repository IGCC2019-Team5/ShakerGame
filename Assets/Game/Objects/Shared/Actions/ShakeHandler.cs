using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakeHandler : MonoBehaviour
{
    public UnityEvent action = new UnityEvent();

    ActionOnce actionOnce;
    public float shakeMagnitude = 1;

    void Start()
    {
        actionOnce = new ActionOnce(action);
    }

    void Update()
    {
        if (Input.gyro.userAcceleration.magnitude > shakeMagnitude)
            actionOnce.Invoke();
    }
}
