using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    // public TouchCamera touchCamera;
    public GameObject[] buildings;
    private BuildingPlacement BuildingPlacement;
    // Start is called before the first frame update
    void Start()
    {
        BuildingPlacement = GetComponent<BuildingPlacement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PickBuidling(int number)
    {
        // touchCamera.isBuilding = true;
        BuildingPlacement.SetItem(buildings[number]);
    }
}
