using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInventory : MonoBehaviour
{
    private BuildSystem buildSys;
    //[SerializeField]
    //private Image invIcon;
    //[SerializeField]
    //private Text invText;

    //private BlockSystem blockSys;
    //private BuildSystem buildSys;

    //private int selectedBlockID = 0;

    //// Start is called before the first frame update
    //private void Awake()
    //{
    //    blockSys = GetComponent<BlockSystem>();
    //    buildSys = GetComponent<BuildSystem>();
    //    ToggleInv(false);
    //}
    //public void ToggleInv(bool invUp)
    //{
    //    if (invUp)
    //    {
    //        invIcon.gameObject.SetActive(true);
    //        UpdateGUI();
    //    }
    //    else
    //    {
    //        invIcon.gameObject.SetActive(false);
    //    }
    //}

    //public bool CheckInvEmpty()
    //{
    //    bool isEmpty = true;
    //    for (int i = 0; i < blockSys.allBlocks.Length; i++)
    //    {
    //        if (blockSys.allBlocks[i].am > 0)
    //        {
    //            isEmpty = false;
    //        }
    //    }
    //    return isEmpty;
    //}


}
