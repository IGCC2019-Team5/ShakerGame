using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    //Make sure the Main Camera is not in Prespective mode

    //reference to the BlockSystem script
    private BlockSystem blockSys;

    //Variables to hold data regarding current block type
    private int currentBlockID = 0;
    private Block currentBlock;

    private int selectableBlocksTotal;
    //Variable for the block template
    [SerializeField]
    private GameObject blockTemplate;
    private SpriteRenderer currentRend;

    //Bools to control building system
    private bool buildModeOn = false;
    private bool buildBlocked = false;

    //Float to adjust the size of blocks when placing in world
    [SerializeField]
    private float blockSizeMod;

    //Layer mask to control raycasting
    [SerializeField]
    private LayerMask solidNoBuildLayer;
    [SerializeField]
    private LayerMask backingNoBuildLayer;
    [SerializeField]
    private LayerMask AllBlocksLayer;

    // Reference to the playerobject
    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private float maxBuildDist;

    //s public bool isMobile = false;
    private bool moveAllowed = false;

    private void Awake()
    {
        //Store reference to block system script.
        blockSys = GetComponent<BlockSystem>();

        //Find player and store 
        playerObject = GameObject.Find("Player");
    }

    private void Update()
    {

#if UNITY_ANDROID
        //  Initiating touch event
        // if touch event takes place
        if (Input.touchCount > 0)
        {
            //get touch position
            Touch touch = Input.GetTouch(0);
            //if you touches the screen
            //flo touchPos = Mathf.Round(Camera.main.ScreenToWorldPoint(touch.position / blockSizeMod)) * blockSizeMod;
            float touchPosX = Mathf.Round(Camera.main.ScreenToWorldPoint(touch.position).x / blockSizeMod) * blockSizeMod;
            float touchPosY = Mathf.Round(Camera.main.ScreenToWorldPoint(touch.position).y / blockSizeMod) * blockSizeMod;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosX,touchPosY)))
                    {
                     
                        moveAllowed = true;
                    }

                    break;

                case TouchPhase.Moved:
                    if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosX, touchPosY)) && moveAllowed)
                    {
                        blockTemplate.transform.position = new Vector2(touchPosX, touchPosY);
                        //blockTemplate.gameObjectMovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    }

                    break;
                case TouchPhase.Ended:
                    moveAllowed = false;
                    break;
            }
        }

#endif
    }
}
