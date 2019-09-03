using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour
{

    public List<Collider2D> colliders = new List<Collider2D>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Building")
        {
            colliders.Add(collider);
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Building")
        {
            colliders.Remove(collider);
        }
    }
}

