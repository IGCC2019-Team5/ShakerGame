using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakedBox : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 acc;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        acc += (Vector2)Input.gyro.userAcceleration;
        acc *= .9f;

        rigid.AddForce(acc);
    }
}
