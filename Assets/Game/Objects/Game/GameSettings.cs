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

    public float totalMultiplier = .001f;
    public float rotationMultiplier = 2f;
    public float xMultiplier = 2f;
    public float yMultiplier = 2f;

    public float chartMultiplier = .1f;

    public float xPowerMultiplier = 4f;
    public float yPowerMultiplier = 4f;
    public float rotationPowerMultiplier = 4f;
}
