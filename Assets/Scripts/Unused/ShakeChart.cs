using System.Collections.Generic;
using UnityEngine;

public class ShakeChart : MonoBehaviour
{
    private UCharts.RadarChart chart;
    public GyroPlayer gyroPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        chart = GetComponent<UCharts.RadarChart>();
    }

    // Update is called once per frame
    public void UpdateChart(GyroPlayer gyroPlayer, bool immediately = false)
    {
        if (gyroPlayer != null)
        {
            List<float> data = new List<float>() {
                gyroPlayer.shakePower.x.power,
                gyroPlayer.shakePower.y.power,
                gyroPlayer.shakePower.rotZ.power,
                0
            };
            GetComponent<UCharts.RadarChart>().ApplyData(data, immediately);
        }
    }

    private void Update()
    {
        if (gyroPlayer)
            UpdateChart(gyroPlayer, true);
    }
}
