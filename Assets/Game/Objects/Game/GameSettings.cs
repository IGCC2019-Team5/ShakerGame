using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    // Shaking time
    public float shakingTime = 30;
    // Shake Strength resistance
    // Lower number makes it harder for the player shake
    [Range(0.0f, 1.5f)]
    public float shakeResistance;

    public float shakeDuration = 5f;
    public float rotationMultiplier = 2f;
}
