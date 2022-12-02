using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public new Light light;
    public float frequency = 10f;
    public float amplitude = 20f;
    public float probability = .7f;
    private float initialIntensity = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (light == null)
        {
            gameObject.GetComponent<Light>();
        }

        if (light != null)
        {
            initialIntensity = light.intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (light != null)
        {
            float random = Random.Range(0f, 1f);
            float randomRandom = Random.Range(0f, 1f);
            if (random < probability || randomRandom < .99)
            {
                light.intensity = initialIntensity + Mathf.Sin(Time.time * frequency) * amplitude;
            } else {
                light.intensity = 0f;
            }
        }
    }
}
