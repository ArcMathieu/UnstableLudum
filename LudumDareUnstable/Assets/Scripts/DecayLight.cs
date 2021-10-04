using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DecayLight : MonoBehaviour {
    private float lightIntensity;
    private Light2D light;

    private void Start() {
        light = GetComponent<Light2D>();
        lightIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update() {
        lightIntensity -= 0.1f * Time.deltaTime;
        light.intensity = lightIntensity;
        Debug.Log(lightIntensity);
    }
}
