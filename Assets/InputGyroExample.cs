//Attach this script to a GameObject in your Scene.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputGyroExample : MonoBehaviour
{
    private Text text;

    float time;
    public float defaultTime = 30f;

    private Bounds bounds;

    private void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        Input.gyro.enabled = true;
        text = GetComponent<Text>();
        time = defaultTime;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        bounds.Encapsulate(Input.acceleration);

        //text.text = 
        //        "MinX " + minX + "\n" +
        //        "MaxX " + maxX + "\n" +
        //        "MinY " + minY + "\n" +
        //        "MaxY " + maxY + "\n" +
        //        "MinZ " + minZ + "\n" +
        //        "MaxZ " + maxZ + "\n" +
        //        "";

        text.text = bounds.extents.magnitude.ToString("F2") + "\nTime: " + time.ToString("F2");

        if (time < 0)
        {
            //SceneManager.LoadScene("GameScene");
        }
    }

    //This is a legacy function, check out the UI section for other ways to create your UI
    private void OnGUI()
    {
        // Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        //GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + Input.gyro.rotationRate);
        //GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + Input.gyro.attitude);
        //GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + Input.gyro.enabled);
        //text.text = "Gyro rotation rate " + Input.gyro.rotationRate + "\n" +
        //        "Gyro attitude" + Input.gyro.attitude + "\n" +
        //        "Gyro enabled : " + Input.gyro.enabled + "\n" +
        //        "Gyro Acceleration" + Input.gyro.userAcceleration + "\n" +
        //        "Acceleration" + Input.acceleration + "\n" +
        //        "";
    }
}