using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWatcher : MonoBehaviour
{
    public BoxCollider2D area;
    GameManager manager;

    private void Start()
    {
        manager = GameManager.Get();
    }

    // Update is called once per frame
    void Update()
    {
        if (!area.OverlapPoint(transform.position))
            if (manager.state != GameState.DIED)
            {
                if (SoundManager.sm_Instance != null)
                    SoundManager.sm_Instance.PlayAmbient(1);

                manager.state = GameState.DIED;
            }

    }
}
