using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdate : MonoBehaviour
{
    public Slider sliderRef;

    GyroRecorder recorderRef;


    // Start is called before the first frame update
    void Start()
    {
        // get the recorder ref reference
        recorderRef = GetComponent<GyroRecorder>();
    }

    // Update is called once per frame
    void Update()
    {
        // if there isn't any slider reference
        // return
        if (sliderRef == null)
            return;

        // Shake amount os from 0 to 10
        // divide by 10 to get 0 to 1 values 
        sliderRef.value = recorderRef.shakeAmount / 10;

    }
}
