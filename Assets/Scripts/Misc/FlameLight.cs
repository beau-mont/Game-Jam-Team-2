using UnityEngine;

public class FlameLight : MonoBehaviour
{
    public float flickerStrength;
    public float minFlickerFrequency;
    public float maxFlickerFrequency;
    public float returnSpeed;
    private float ticker;
    private float defaultIntensity;
    private new Light light;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        light = GetComponent<Light>();
        defaultIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (ticker <= 0)
        {
            ticker = Random.Range(minFlickerFrequency, maxFlickerFrequency);
            light.intensity = UnityEngine.Random.Range(defaultIntensity - (defaultIntensity * flickerStrength), defaultIntensity + (defaultIntensity * flickerStrength));
        }
        else
        {
            ticker -= Time.deltaTime;
            light.intensity += returnSpeed * (defaultIntensity - light.intensity) * Time.deltaTime;
        }
    }
}
