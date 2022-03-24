using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointLight : MonoBehaviour
{
    Light pointLight;
    float normalRange;
    float normalIntensity;
    public float maxRange = 5f;
    public float lerpSpeed = 10f;
    public float secondInterval = 0.1f;

    public float maxIntensity = 2f;

    float currentRange;
    float targetRange;

    float currentIntensity;
    float targetIntensity;
    // Start is called before the first frame update
    void Start()
    {
        pointLight = GetComponent<Light>();
        normalRange = pointLight.range;
        currentRange = normalRange;
        normalIntensity = pointLight.intensity;
        currentIntensity = normalIntensity;
        Invoke(nameof(UpdateFire), secondInterval);
    }

    void UpdateFire()
    {
        targetRange = normalRange + Random.Range(0f, maxRange);
        targetIntensity = normalIntensity + Random.Range(0f, maxIntensity);
        Invoke(nameof(UpdateFire), secondInterval);
    }

    // Update is called once per frame
    void Update()
    {
        currentRange = Mathf.Lerp(currentRange, targetRange, lerpSpeed * Time.deltaTime);
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, lerpSpeed * Time.deltaTime);
        pointLight.range = currentRange;
        pointLight.intensity = currentIntensity;
    }
}
