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
        // If E key pressed, toggle build mode
        if (Input.GetKeyDown("e"))
        {
            //Flip bool
            buildModeOn = !buildModeOn;

            //if we have a current template, destroy it
            if (blockTemplate != null)
            {
                Destroy(blockTemplate);
            }


            //If we dont have a current block type set
            if (currentBlock == null)
            {
                //Ensure allBlocks array is ready
                if (blockSys.allBlocks[currentBlockID] != null)
                {
                    //Get a new currentBlock using the ID variable
                    currentBlock = blockSys.allBlocks[currentBlockID];
                }
            }

            if (buildModeOn)
            {
                //Create a new object for blockTemplate.
                blockTemplate = new GameObject("CurrentBlockTemplate");
                //Add and store reference to a SpriteRenderer on the template object
                currentRend = blockTemplate.AddComponent<SpriteRenderer>();
                //Set the sprite of the template object to match crrent block type
                currentRend.sprite = currentBlock.blockSprite;
                currentRend.sortingOrder = 1;
            }
        }

        if (buildModeOn && blockTemplate != null)
        {

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
            if (Vector2.Distance(playerObject.transform.position, blockTemplate.transform.position) > maxBuildDist)
            {
                buildBlocked = true;
            }

            if (buildBlocked)
            {
                currentRend.color = new Color(1f, 0f, 0f, 1f);
            }
            else
            {
                currentRend.color = new Color(1f, 1f, 1f, 1f);
            }

            float newPosX = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / blockSizeMod) * blockSizeMod;
            float newPosY = Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / blockSizeMod) * blockSizeMod;
            blockTemplate.transform.position = new Vector2(newPosX, newPosY);
            // Debug.Log("newPosX: " + blockTemplate.transform.position.x + "newPosY: " + blockTemplate.transform.position.y);
            //Debug.Log("mousePosX: " + Input.mousePosition.x + "mousePosY: " + Input.mousePosition.x);

            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseWheel != 0)
            {
                selectableBlocksTotal = blockSys.allBlocks.Length - 1;
                if (mouseWheel > 0)
                {
                    currentBlockID--;
                    if (currentBlockID < 0)
                    {
                        currentBlockID = selectableBlocksTotal;
                    }
                }
                else if (mouseWheel < 0)
                {
                    currentBlockID++;
                    if (currentBlockID > selectableBlocksTotal)
                    {
                        currentBlockID = 0;
                    }
                }
                currentBlock = blockSys.allBlocks[currentBlockID];
                currentRend.sprite = currentBlock.blockSprite;

            }
            if (Input.GetKeyDown("q"))
            {

                blockTemplate.transform.Rotate(Vector3.forward * -90);
            }

            if (Input.GetMouseButtonDown(0) && buildBlocked == false)
            {
                GameObject newBlock = new GameObject(currentBlock.blockName);
                newBlock.transform.position = blockTemplate.transform.position;
                newBlock.transform.rotation = blockTemplate.transform.rotation;
                SpriteRenderer newRend = newBlock.AddComponent<SpriteRenderer>();
                newRend.sprite = currentBlock.blockSprite;
                newRend.sortingOrder = 1;

                if (currentBlock.isSolid == true)
                {
                    newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 9;
                    newRend.sortingOrder = 1;
                }
                else
                {
                    newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 10;
                    newRend.sortingOrder = 1;
                }
            }

            if (Input.GetMouseButtonDown(1) && blockTemplate != null)
            {
                RaycastHit2D destroyHIt = Physics2D.Raycast(blockTemplate.transform.position, Vector2.zero, Mathf.Infinity, AllBlocksLayer);

                if (destroyHIt.collider != null)
                {
                    Destroy(destroyHIt.collider.gameObject);
                }
            }

        }
    }

    public void OnNewBlock(int id)
    {
        //Flip bool
        buildModeOn = !buildModeOn;

        //if we have a current template, destroy it
        if (blockTemplate != null)
        {
            Destroy(blockTemplate);
        }

        if (buildModeOn)
        {
            //Create a new object for blockTemplate.
            blockTemplate = Instantiate(blockSys.blockTypes.blocks[id]);
            blockTemplate.name = "CurrentBlockTemplate";
            //Add and store reference to a SpriteRenderer on the template object
            currentRend = blockTemplate.GetComponent<SpriteRenderer>();
            currentRend.sortingOrder = 1;
            currentBlock = blockTemplate.GetComponent<BlockInfo>().info;
        }
    }
}
