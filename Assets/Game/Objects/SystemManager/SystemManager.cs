using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    void Awake()
    {
        Input.gyro.enabled = true;
    }
}
