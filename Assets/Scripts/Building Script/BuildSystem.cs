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
                    if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(new Vector2(touchPosX, touchPosY)))
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
        else if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                blockTemplate.transform.Rotate(Vector3.forward * -90);
            }
        }

        RaycastHit2D rayhit;
        if (currentBlock.isSolid == true)
        {
            rayhit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, solidNoBuildLayer);
        }
        else
        {
            rayhit = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, backingNoBuildLayer);
        }

        if (rayhit.collider != null)
        {
            buildBlocked = true;
        }
        else
        {
            buildBlocked = false;
        }
        if (buildBlocked)
        {
            currentRend.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        {
            currentRend.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void SetItem(int currentBlockID)
    {
        //Flip bool
        buildModeOn = !buildModeOn;

        //if we have current block type, destroy it
        if (blockTemplate != null)
        {
            Destroy(blockTemplate);
        }

        //if we dont have a current block type set
        if (currentBlock == null)
        {
            //Ensure allBlocks aray is ready
            if (blockSys.allBlocks[currentBlockID] != null)
            {
                //Get a new currentBlock using the ID variable
                currentBlock = blockSys.allBlocks[currentBlockID];
            }
        }
        if (buildModeOn)
        {
            //Create a new object for block Template/
            blockTemplate = new GameObject("CurrentBlockTemplate");
            //Add and store reference to a SpriteRenderer on the template object
            currentRend = blockTemplate.AddComponent<SpriteRenderer>();
            //Set the sprite of the template object to match current block type
            currentRend.sprite = currentBlock.blockSprite;
        }
    }

}
