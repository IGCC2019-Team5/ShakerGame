﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockSystem : MonoBehaviour
{
    // Array we expose to inspector / editor, use this instead of the old arrays to define block types.
    //[SerializeField]
    //[Obsolete]
    //private BlockType[] allBlockTypes;
    public BlocksPalette blockTypes;

    // Array to store all blocks created in Awake()
    //[HideInInspector]
    //public Block[] allBlocks;


    private void Awake()
    {
        //// Initialise allBlocks array.
        //allBlocks = new Block[allBlockTypes.Length];



        //// For loops to populate main allBlocks array.
        //for (int i = 0; i < allBlockTypes.Length; i++)
        //{
        //    // Instead of referencing multiple arrays, we just create a new BlockType object and get values from that.
        //    BlockType newBlockType = allBlockTypes[i];
        //    allBlocks[i] = new Block(i, newBlockType.blockName, newBlockType.blockSprite, newBlockType.blockIsSolid);
        //    Debug.Log("Solid block: allBlocks[" + i + "] = " + newBlockType.blockName);
        //}
    }


}


// We still use the Block class to store the final Block type data.
[Serializable]
public class Block
{
    public int id;
    public bool isSolid;

    public Block(int id, bool isSolid)
    {
        this.id = id;
        this.isSolid = isSolid;
    }
}

// Custom struct for Block type.
//[Serializable]
//public struct BlockType
//{
//    // Main, differing variables for each block type.
//    public string blockName;
//    public Sprite blockSprite;
//    public bool blockIsSolid;
//}





