using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private PlaceableBuilding placeableBuilding;
    [SerializeField]
    private GameObject currentBuilding;


    // touch offset allows ball not to shake when it starts moving
    float deltaX, deltaY;

    [SerializeField]
    private float blockSizeMod;

    // ball movement not allowed if you touches not the ball at the first time
    bool moveAllowed = false;
    void Start()
    {
        //if (currentBuilding == null)
        //{
        //    GetComponent<Transform>();
        //}
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                        Debug.Log("Touch.Begin");

                        Vector2 m = touch.position;
                       // moveAllowed = true;

                    break;
                case TouchPhase.Moved:
                    {
                        m = new Vector2(touch.position.x, touch.position.y);
                        Vector2 pos = Mathf.Round(Camera.main.ScreenToWorldPoint(m);
                        currentBuilding.transform.position = new Vector2(pos.x / 2, pos.y * 5);
                        Debug.Log("Touch.Move");
                    }
                    //m = new Vector2(touch.position.x, touch.position.y);
                    //Vector3 pos = GetComponent<Camera>().ScreenToWorldPoint(m);
                    //currentBuilding.position = new Vector2(touchPos.x, touchPos.y );
                    //GetComponent<Camera>().transform.position = new Vector2(pos.x / 2, pos.y/2);
                    break;
                case TouchPhase.Ended:
                    //moveAllowed = false;
                    Debug.Log("Touch.Ended");
                    break;
            }



        }
    }

    public void SetItem(GameObject building)
    {
        currentBuilding = (Instantiate(building, new Vector2(0, 0), Quaternion.identity));
        //GetComponent<Camera>().transform.position = new Vector3(0, 0, -10);
        placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();

    }
}
