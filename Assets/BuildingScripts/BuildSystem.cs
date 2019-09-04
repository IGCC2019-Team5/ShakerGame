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
    private float blockSizeMod = 0.25f;

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

    float rotation = 0;

    //[SerializeField]
    //private float maxBuildDist;
    public BoxCollider2D maxBuildArea;

    //s public bool isMobile = false;
    private readonly bool moveAllowed = false;

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
            OnNewBlock(blockSys.blockTypes.blocks[currentBlockID]);
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float newPosX = Mathf.Round(mousePos.x / blockSizeMod) * blockSizeMod;
        float newPosY = Mathf.Round(mousePos.y / blockSizeMod) * blockSizeMod;
        Vector2 newPos = new Vector2(newPosX, newPosY);
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
                Debug.Log(rayhit.collider.gameObject);
                buildBlocked = true;
            }
            else
            {
                buildBlocked = false;
            }
            
            if (!maxBuildArea.OverlapPoint(blockTemplate.transform.position))
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

            //Debug.Log($"newPosX: {newPos.x}, newPosY: {newPos.y}");
            //Debug.Log($"mousePosX: {Input.mousePosition.x}, mousePosY: {Input.mousePosition.x}");
            blockTemplate.transform.position = newPos;

            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseWheel != 0)
            {
                selectableBlocksTotal = blockSys.blockTypes.blocks.Count - 1;
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
                GameObject block = blockSys.blockTypes.blocks[currentBlockID];
                currentBlock = block.GetComponent<BlockInfo>().info;
                currentRend.sprite = block.GetComponent<SpriteRenderer>().sprite;

            }
            if (Input.GetKeyDown("q"))
            {
                Rotate();
            }

            {
                var angle = blockTemplate.transform.localEulerAngles;
                angle.z = rotation;
                blockTemplate.transform.localEulerAngles = angle;
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    OnPlace();
            //}
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D destroyHIt = Physics2D.Raycast(newPos, Vector2.zero, Mathf.Infinity, AllBlocksLayer);

            if (destroyHIt.collider != null)
            {
                var prefab = blockSys.blockTypes.blocks[destroyHIt.collider.gameObject.GetComponent<BlockInfo>().info.id];
                OnNewBlock(prefab);
                Destroy(destroyHIt.collider.gameObject);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPlace();
        }
    }

    public void Rotate()
    {
        rotation += 90f;
        Rotate(rotation);
    }

    public void Rotate(float rotation)
    {
        foreach (var button in GameObject.FindObjectsOfType<ItemButton>())
            button.Rotate(rotation);
    }

    public void OnNewBlock(GameObject newObject)
    {
        //Flip bool
        buildModeOn = true;

        //if we have a current template, destroy it
        if (blockTemplate != null)
        {
            Destroy(blockTemplate);
        }

        if (buildModeOn)
        {
            //Create a new object for blockTemplate.
            blockTemplate = Instantiate(newObject);
            blockTemplate.name = "CurrentBlockTemplate";
            blockTemplate.layer = 0;
            //Add and store reference to a SpriteRenderer on the template object
            currentRend = blockTemplate.GetComponent<SpriteRenderer>();
            currentRend.sortingOrder = 1;
            currentBlock = blockTemplate.GetComponent<BlockInfo>().info;
        }
    }

    public void OnPlace()
    {
        if (buildModeOn)
        {
            //Flip bool
            buildModeOn = false;

            if (buildBlocked == false)
            {
                var prefab = blockSys.blockTypes.blocks[currentBlock.id];
                GameObject newBlock = Instantiate(prefab);
                newBlock.transform.position = blockTemplate.transform.position;
                newBlock.transform.rotation = blockTemplate.transform.rotation;
                SpriteRenderer newRend = newBlock.GetComponent<SpriteRenderer>();
                newRend.sortingOrder = 1;

                if (currentBlock.isSolid == true)
                {
                    //newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 9;
                    newRend.sortingOrder = 1;
                }
                else
                {
                    //newBlock.AddComponent<BoxCollider2D>();
                    newBlock.layer = 10;
                    newRend.sortingOrder = 1;
                }
            }

            //if we have a current template, destroy it
            if (blockTemplate != null)
            {
                Destroy(blockTemplate);
            }
        }
    }
}
