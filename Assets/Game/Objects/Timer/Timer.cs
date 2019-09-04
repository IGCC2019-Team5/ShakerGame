using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameState workState;
    public bool stateChangeFeatureEnabled = true;
    public GameState nextState;

    public float maxTime = 10;
    private float timeLeft;
    public Text timer;
    private GameManager manager;

    // Start is called before the first frame update
    private void Start()
    {
        manager = GameManager.Get();
    }

    // Update is called once per frame
    private void Update()
    {
        if (manager.state == workState)
        {
            timeLeft += Time.deltaTime;
            timer.text = $"{maxTime - timeLeft}";
            if (stateChangeFeatureEnabled)
            {
                if (timeLeft > maxTime)
                {
                    manager.state = nextState;
                }
            }
        }
    }
}
