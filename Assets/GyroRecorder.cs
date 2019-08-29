using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GyroRecorder : MonoBehaviour
{
    public static Shake.ShakeMovie lastMovie;

    public float timeLimit = 30;
    public float time;

    Shake.ShakeMovieBuilder movie = new Shake.ShakeMovieBuilder();

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        movie.AddFrame(Shake.ShakeFrame.CreateFromGyro(Input.gyro));
        if (time > timeLimit)
        {
            lastMovie = movie.Build();
            SceneManager.LoadScene("PlayScene");
        }
    }
}
