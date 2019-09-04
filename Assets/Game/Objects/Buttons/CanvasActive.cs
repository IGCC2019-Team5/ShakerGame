using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasActive : MonoBehaviour
{
    public static CanvasActive instance;
    [SerializeField]
    private GameObject CanvasObject;
    [SerializeField]
    private GameObject CanvasShake;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = null;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (CanvasObject == null)
        {
            CanvasObject = GameObject.Find("FoodCanvas");
        }
        if (CanvasShake == null)
        {
            CanvasShake = GameObject.Find("ShakeCanvas");
        }

        manager = GameManager.Get();
       manager.stateChangeEvents += OnStateChanged;

    }

    // Update is called once per frame
    public void ShakeCanvas()
    {
        CanvasShake.SetActive(true);
        CanvasObject.SetActive(false);
    }
    public void FoodCanvas()
    {
        CanvasShake.SetActive(false);
        CanvasObject.SetActive(true);
    }

    void OnStateChanged(GameState oldState, GameState newState)
    {
        if (newState == GameState.BUILDING)//Previous State
        {
            Debug.Log("Test");
            FoodCanvas();
            ChangeBGSize.instance.ZoomInBG();
        }

        if (oldState == GameState.RECORDING)//Next State
        {
            ShakeCanvas();
            ChangeBGSize.instance.ZoomOutBG();
        }
    }

    public static void Destory()
    {
        if (instance)
            Destroy(instance.gameObject);
    }
}
