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
    public void UpdateChart(Shake.ShakePower shakePower, bool immediately = false)
    {
        List<float> data = new List<float>() {
                shakePower.x.power,
                shakePower.y.power,
                shakePower.rotZ.power,
                0
            };
        chart.ApplyData(data, immediately);
    }
}
