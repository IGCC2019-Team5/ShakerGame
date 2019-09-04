using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/BlocksPalette", fileName = "BlocksPalette")]
public class BlocksPalette : ScriptableObject
{
    public List<GameObject> blocks;
}
