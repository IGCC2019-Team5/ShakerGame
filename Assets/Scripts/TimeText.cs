using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    GyroRecorder recorder;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        recorder = GetComponent<GyroRecorder>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Elapsed Time : {recorder.elapsedTime}";
    }
}
