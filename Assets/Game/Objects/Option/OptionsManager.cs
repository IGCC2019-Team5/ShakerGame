using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    SoundManager soundInstance;

    public GameObject BGMusicButton;
    public GameObject SFXMusicButton;
    public GameObject BGSlider;
    public GameObject SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        soundInstance = SoundManager.sm_Instance;

        // Set the initial slider values
        SFXSlider.GetComponent<Slider>().value = soundInstance.SFXVolume;
        BGSlider.GetComponent<Slider>().value = soundInstance.BGVolume;

        // Set up the starting buttons
        // If it is true when toggled
        if (!soundInstance.BGMusic)
        {
            // set it to false
            soundInstance.BGMusic = false;
            // Change the text
            BGMusicButton.GetComponentInChildren<Text>().text = "OFF";
        }
        // If it is false when toggled
        else if (soundInstance.BGMusic)
        {
            // Set it to true
            soundInstance.BGMusic = true;
            // Change the text
            BGMusicButton.GetComponentInChildren<Text>().text = "ON";
        }

        // if it is true when toggled
        if (!soundInstance.SFXMusic)
        {
            // Toggle it to false
            soundInstance.SFXMusic = false;
            // change the text
            SFXMusicButton.GetComponentInChildren<Text>().text = "OFF";
        }
        // if it is false when toggled
        else if (soundInstance.SFXMusic)
        {
            // Toggle it to true
            soundInstance.SFXMusic = true;
            SFXMusicButton.GetComponentInChildren<Text>().text = "ON";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set the SFX volume in sound manager to the slider value
        soundInstance.SFXVolume = SFXSlider.GetComponent<Slider>().value;
        // Set the BG volume in the sound manager to the slider value
        soundInstance.BGVolume = BGSlider.GetComponent<Slider>().value;
    }

    public void ToggleBGMusic()
    {
        // If it is true when toggled
        if (soundInstance.BGMusic)
        {
            // set it to false
            soundInstance.BGMusic = false;
            // Change the text
            BGMusicButton.GetComponentInChildren<Text>().text = "OFF";
        }
        // If it is false when toggled
        else if (!soundInstance.BGMusic)
        {
            // Set it to true
            soundInstance.BGMusic = true;
            // Change the text
            BGMusicButton.GetComponentInChildren<Text>().text = "ON";
        }

        // Play tap sound
        soundInstance.PlayTap();
    }

    public void ToggleSFXMusic()
    {
        // if it is true when toggled
        if (soundInstance.SFXMusic)
        {
            // Toggle it to false
            soundInstance.SFXMusic = false;
            // change the text
            SFXMusicButton.GetComponentInChildren<Text>().text = "OFF";
        }
        // if it is false when toggled
        else if (!soundInstance.SFXMusic)
        {
            // Toggle it to true
            soundInstance.SFXMusic = true;
            SFXMusicButton.GetComponentInChildren<Text>().text = "ON";
        }

        // Play tap sound
        soundInstance.PlayTap();

    }
}
