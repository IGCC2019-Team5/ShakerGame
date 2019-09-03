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
        chart.ApplyData(data, immediately);
    }
}
