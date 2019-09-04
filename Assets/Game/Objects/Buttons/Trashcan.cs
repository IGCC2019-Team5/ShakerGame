using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trashcan : MonoBehaviour
{
    public UnityEvent onTrashcan;
    bool inside;

    public void OnDragEnd()
    {
        if (inside)
            onTrashcan.Invoke();
    }

    public void OnPointerEnter()
    {
        inside = true;
    }

    public void OnPointerExit()
    {
        inside = false;
    }
}
