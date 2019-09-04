using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGSource;
    public AudioSource SFXSource;

    public static SoundManager sm_Instance;

    [System.NonSerialized] public bool BGMusic = true;
    [System.NonSerialized] public bool SFXMusic = true;
    [System.NonSerialized] public float BGVolume = 1.0f;
    [System.NonSerialized] public float SFXVolume = 1.0f;

    // Created 4 separated lists because the function calling handles the playing
    // I could create a struct that stores the clip and type of clip
    // and make One List only instead of Four Lists.

    // Holds bopClips
    public List<AudioClip> bopClips = new List<AudioClip>();
    // Holds AmbientClips
    public List<AudioClip> ambientClips = new List<AudioClip>();
    // Holds tappingClips
    public List<AudioClip> tappingClips = new List<AudioClip>();
    // Holds shakingClips
    public List<AudioClip> shakingClips = new List<AudioClip>();

    private void Awake()
    {
        // Create instance
        if (sm_Instance == null)
            sm_Instance = this;
        else if (sm_Instance != this)
            Destroy(this);

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        //DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        //FileInfo[] allFiles = directoryInfo.GetFiles("*.*");

        //foreach(FileInfo file in allFiles)
        //{
        //    // Check to make sure it has these 4 tags
        //    if (file.Name.Contains("Ambient") || file.Name.Contains("Bop") || file.Name.Contains("Shaking") || file.Name.Contains("Tapping"))
        //    {
        //        StartCoroutine("LoadMusic", file);
        //    }
        //}

        // Load all 4 types of sounds
        //loadBops();
        //loadAmbients();
        //loadShaking();
        //loadTapping();

        PlayAmbient(2);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there was a change
        if (!BGMusic)
        {
            if (BGSource.isPlaying)
                BGSource.Pause();
        }
        else if (BGMusic)
        {
            if (!BGSource.isPlaying)
                BGSource.Play();

            if (BGSource.volume != BGVolume)
            {
                BGSource.volume = BGVolume;
            }
        }

        // Check if there was a change
        if (!SFXMusic)
        {
            if (SFXSource.isPlaying)
                SFXSource.Pause();
        }
        else if (SFXSource.volume != SFXVolume)
        {
            SFXSource.volume = SFXVolume;
        }
    }

 #region Loading Functions

    void loadBops()
    {
        // Load all the audio clips for bopping
        Object[] audioClips;
        audioClips = Resources.LoadAll("Bop", typeof(AudioClip));

        // Store in the list
        foreach(var audioClip in audioClips)
        {
            bopClips.Add((AudioClip)audioClip);
        }
    }

    void loadAmbients()
    {
        // Load all the audio clips for bopping
        Object[] audioClips;
        audioClips = Resources.LoadAll("Ambient", typeof(AudioClip));

        // Store in the list
        foreach (var audioClip in audioClips)
        {
            ambientClips.Add((AudioClip)audioClip);
        }
    }

    void loadShaking()
    {
        // Load all the audio clips for bopping
        Object[] audioClips;
        audioClips = Resources.LoadAll("Shaking", typeof(AudioClip));

        // Store in the list
        foreach (var audioClip in audioClips)
        {
            shakingClips.Add((AudioClip)audioClip);
        }
    }

    void loadTapping()
    {
        // Load all the audio clips for bopping
        Object[] audioClips;
        audioClips = Resources.LoadAll("Tap", typeof(AudioClip));

        // Store in the list
        foreach (var audioClip in audioClips)
        {
            tappingClips.Add((AudioClip)audioClip);
        }
    }
    #endregion

    #region Play Functions
    /// <summary>
    /// Plays a Bop Sound
    /// </summary>
    /// <param name="index"></param>
    public bool PlayBop(int index)
    {
        // If the player disabled SFX music
        if (!SFXMusic)
        {
            SFXSource.Pause();

            return false;
        }

        // If the index passed in is more than the count for bop sounds 
        // return
        if (index >= bopClips.Count)
            return false;

        // Toggle looping off
        if (SFXSource.loop)
            SFXSource.loop = false;

        // Check if theres any sounds playing
        // if there is, pause it
        if (SFXSource.isPlaying)
            SFXSource.Pause();

        // Change the clip
        SFXSource.clip = bopClips[index];
        // Play the clip
        SFXSource.Play();

        return true;
    }

    /// <summary>
    /// Plays ambient music
    /// </summary>
    /// <param name="index"></param>
    public bool PlayAmbient(int index)
    {
        if (!BGMusic)
        {
            BGSource.Pause();
            return false;
        }
        // if the index is more than the count for ambient sounds
        // return
        if (index >= ambientClips.Count)
            return false;

        // Check if any BG Music is playing
        // if there is, pause it
        if (BGSource.isPlaying)
            BGSource.Pause();

        // Change the clip
        BGSource.clip = ambientClips[index];
        // Play the clip
        BGSource.Play();
        BGSource.loop = true;
        
        return true;
    }

    /// <summary>
    /// Plays tapping sound
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool PlayTap(int index)
    {
        // If the player disabled SFX music
        if (!SFXMusic)
        {
            SFXSource.Pause();

            return false;
        }

        // If the index is more than the count for tappign sounds
        // return
        if (index >= tappingClips.Count)
            return false;

        // Toggle looping off
        if (SFXSource.loop)
            SFXSource.loop = false;

        // If it is currently playing
        if (SFXSource.isPlaying)
            SFXSource.Pause();

        // Set the clip
        SFXSource.clip = tappingClips[index];

        // Play
        SFXSource.Play();
        return true;
    }

    public bool PlayTap()
    {
        // If the player disabled SFX music
        if (!SFXMusic)
        {
            SFXSource.Pause();

            return false;
        }
    
        int index = Random.Range(0, tappingClips.Count);
        // If the index is more than the count for tappign sounds
        // return
        if (index >= tappingClips.Count)
            return false;

        // Toggle looping off
        if (SFXSource.loop)
            SFXSource.loop = false;

        Debug.Log(index);

        //If it is currently playing
        if (SFXSource.isPlaying)
            SFXSource.Pause();

        // Set the clip
        SFXSource.clip = tappingClips[index];

        // Play
        SFXSource.Play();

        return true;
    }

    /// <summary>
    /// Plays shaking sound
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool PlayShaking(int index)
    {
        // if the index is more than the count for shaking sounds
        // return
        if (index >= shakingClips.Count)
            return false;

        if (SFXSource.isPlaying)
            SFXSource.Pause();

        // Set the clip
        SFXSource.clip = shakingClips[index];

        // Play
        SFXSource.Play();

        SFXSource.loop = true;

        return true;
    }
    #endregion

    IEnumerator LoadMusic(FileInfo musicFile)
    {
        if (musicFile.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string musicFilePath = musicFile.FullName.ToString();
            string fileURL = string.Format("file://{0}", musicFilePath);
            var www = new WWW(fileURL);
            yield return www;
            if (musicFile.Name.Contains("Ambient"))
            {
                ambientClips.Add(www.GetAudioClip(false, false));
            }
            else if (musicFile.Name.Contains("Bop"))
            {
                bopClips.Add(www.GetAudioClip(false, false));
            }
            else if (musicFile.Name.Contains("Shaking"))
            {
                shakingClips.Add(www.GetAudioClip(false, false));
            }
            else if (musicFile.Name.Contains("Tapping"))
            {
                tappingClips.Add(www.GetAudioClip(false, false));
            }
        }
    }
}
