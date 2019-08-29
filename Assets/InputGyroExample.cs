//Attach this script to a GameObject in your Scene.
using UnityEngine;
using UnityEngine.UI;

public class InputGyroExample : MonoBehaviour
{
    private Text text;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float minZ;
    private float maxZ;

    private void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        Input.gyro.enabled = true;
        text = GetComponent<Text>();
    }

    private void Update()
    {
        minX = Mathf.Min(minX, Input.gyro.userAcceleration.x);
        maxX = Mathf.Max(maxX, Input.gyro.userAcceleration.x);
        minY = Mathf.Min(minY, Input.gyro.userAcceleration.y);
        maxY = Mathf.Max(maxY, Input.gyro.userAcceleration.y);
        minZ = Mathf.Min(minZ, Input.gyro.userAcceleration.z);
        maxZ = Mathf.Max(maxZ, Input.gyro.userAcceleration.z);
        
        //text.text = 
        //        "MinX " + minX + "\n" +
        //        "MaxX " + maxX + "\n" +
        //        "MinY " + minY + "\n" +
        //        "MaxY " + maxY + "\n" +
        //        "MinZ " + minZ + "\n" +
        //        "MaxZ " + maxZ + "\n" +
        //        "";
    }

    //This is a legacy function, check out the UI section for other ways to create your UI
    private void OnGUI()
    {
        // Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        //GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + Input.gyro.rotationRate);
        //GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + Input.gyro.attitude);
        //GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + Input.gyro.enabled);
        text.text = "Gyro rotation rate " + Input.gyro.rotationRate + "\n" +
                "Gyro attitude" + Input.gyro.attitude + "\n" +
                "Gyro enabled : " + Input.gyro.enabled + "\n" +
                "Gyro Acceleration" + Input.gyro.userAcceleration + "\n" +
                "Acceleration" + Input.acceleration + "\n" +
                "";
    }
}