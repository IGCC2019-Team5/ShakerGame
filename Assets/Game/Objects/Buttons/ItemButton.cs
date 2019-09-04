using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public void Rotate(float rotation)
    {
        var angle = transform.localEulerAngles;
        angle.z = rotation;
        transform.localEulerAngles = angle;
    }
}
