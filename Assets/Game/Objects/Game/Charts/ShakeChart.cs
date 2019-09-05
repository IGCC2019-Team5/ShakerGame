using System.Collections.Generic;
using UnityEngine;

public class ShakeChart : MonoBehaviour
{
    private UCharts.RadarChart chart;

    // Start is called before the first frame update
    private void Awake()
    {
        chart = GetComponent<UCharts.RadarChart>();
    }

    // Update is called once per frame
    public void UpdateChart(GameSettings settings, Shake.ShakePower shakePower, bool immediately = false)
    {
        List<float> data = new List<float>() {
                shakePower.y.power * settings.chartMultiplier,
                shakePower.x.power * settings.chartMultiplier,
                shakePower.rotZ.power * settings.chartMultiplier,
                0
            };
        var colorA = new Color32(244, 12, 12, 100);
        var colorB = new Color32(244, 244, 12, 100);
        List<Color32> color = new List<Color32>() {
                Color.Lerp(colorA, colorB, shakePower.y.freq * settings.rotationFreqMultiplier),
                Color.Lerp(colorA, colorB, shakePower.x.freq * settings.rotationFreqMultiplier),
                Color.Lerp(colorA, colorB, shakePower.rotZ.freq * settings.rotationFreqMultiplier),
            };
        chart.ApplyData(data, color, immediately);
    }
}
