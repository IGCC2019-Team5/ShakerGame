using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionOnce
{
    public UnityEvent action;
    bool actionDone = false;

    public ActionOnce(UnityEvent action)
    {
        this.action = action;
    }

    public void Invoke()
    {
        if (!actionDone)
        {
            actionDone = true;
            action.Invoke();
        }
    }

    public void Reset()
    {
        actionDone = false;
    }
}