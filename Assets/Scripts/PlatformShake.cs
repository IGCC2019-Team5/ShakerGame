using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public float freq;
    public float force;
}

[System.Serializable]
public struct Power
{
    public Wave x, y;
    public Wave rotZ;
}

public class PlatformShake : MonoBehaviour
{
    public Power power;
    Rigidbody2D target;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        float x = (Mathf.Sin(Mathf.PI * 2 * power.x.freq * t)/* > 0 ? 1 : -1*/) * power.x.force;
        float y = (Mathf.Sin(Mathf.PI * 2 * power.y.freq * t)/* > 0 ? 1 : -1*/) * power.y.force;
        float rotZ = (Mathf.Sin(Mathf.PI * 2 * power.rotZ.freq * t)/* > 0 ? 1 : -1*/) * power.rotZ.force;
        target.velocity = new Vector2(x, y);
        target.angularVelocity = rotZ;
    }
}
