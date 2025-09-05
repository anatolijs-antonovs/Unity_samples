using System.Collections;
using Misc;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ClosingEyesController : MonoBehaviour {
        
    [SerializeField] private Material eyesMaterial;
    [SerializeField] private CustomPassVolume volume;

    private const float eyesOpen = 0f;
    private const float eyesClosed = 1f;
    private const float defaultSmoothness = 0.03f;
    private const float sleepDuration = 2f;
    
    // properties from shader
    private readonly int eyesClosedPropertyId = Shader.PropertyToID("_Eyes_Closed");
    private readonly int smoothnessPropertyId = Shader.PropertyToID("_Smoothness");

    private bool eyesClosing;
    private bool eyesOpening;
    private float eyesClosingDelta;
    private float smoothnessDelta;

    private void Update() {
        // please put here your trigger
        if (ComponentsHolder.Instance.input.use) {
            ComponentsHolder.Instance.input.use = false;
            
            volume.customPasses[0].enabled = true;
            eyesClosing = true;
            smoothnessDelta = defaultSmoothness;
        }
        
        // change material parameters until eyes will be closed
        if (eyesClosing) {
            eyesClosingDelta += Time.deltaTime;

            if (eyesClosingDelta <= eyesClosed) {
                // reduce the smoothness in the end
                if (eyesClosingDelta > 0.9f) smoothnessDelta = 0f;
                
                eyesMaterial.SetFloat(eyesClosedPropertyId, eyesClosingDelta);
                eyesMaterial.SetFloat(smoothnessPropertyId, smoothnessDelta);
            } else {
                eyesClosing = false;
                // schedule eyes opening animation
                StartCoroutine(StartWakeUp());
            }
        }
        
        // change material parameters until eyes will be opened
        if (eyesOpening) {
            eyesClosingDelta -= Time.deltaTime;

            if (eyesClosingDelta > eyesOpen) {
                if (eyesClosingDelta < 0.9f) smoothnessDelta = defaultSmoothness;
                eyesMaterial.SetFloat(eyesClosedPropertyId, eyesClosingDelta);
                eyesMaterial.SetFloat(smoothnessPropertyId, smoothnessDelta);
            } else {
                eyesOpening = false;
            }
        }
    }

    private IEnumerator StartWakeUp() {
        yield return new WaitForSeconds(sleepDuration);
        eyesClosingDelta = eyesClosed;
        eyesOpening = true;
    }
}