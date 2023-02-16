using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    Light2D flickeringLight;
    public bool flicker;
    public float intensityRange = 0.5f;
    public float intensityTimeMin = 0.1f;
    public float intensityTimeMax = 0.5f;
    float baseIntensity;
    //

    void Start()
    {
        flickeringLight = GetComponent<Light2D>();
        baseIntensity = GetComponent<Light>().intensity;
        StartCoroutine(FlickIntensity());
    }

    void Update()
    {
        
    }

    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flicker)
            {
                t0 = Time.time;
                float r = Random.Range(baseIntensity - intensityRange, baseIntensity + intensityRange);
                flickeringLight.intensity = r;
                t = Random.Range(intensityTimeMin, intensityTimeMax);
                yield return wait;
            }
            else yield return null;
        }
    }
}
