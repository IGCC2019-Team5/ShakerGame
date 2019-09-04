using UnityEngine;

public class BlockGravitySwitcher : MonoBehaviour
{
    private GameManager manager;
    public GameObject foodPool;

    // Start is called before the first frame update
    private void Start()
    {
        manager = GameManager.Get();
        manager.stateChangeEvents += OnStateChanged;
    }

    private void OnStateChanged(GameState oldState, GameState newState)
    {
        if (newState == GameState.PLAYING)
        {
            var foods = foodPool.GetComponentsInChildren<Rigidbody2D>();
            foreach (var food in foods)
            {
                if (food.tag == "Food")
                {
                    food.isKinematic = false;
                }
            }
        }

        if (oldState == GameState.PLAYING)
        {
            var foods = foodPool.GetComponentsInChildren<Rigidbody2D>();
            foreach (var food in foods)
            {
                if (food.tag == "Food")
                {
                    food.isKinematic = true;
                }
            }
        }
    }
}
