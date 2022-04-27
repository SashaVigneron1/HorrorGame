using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] Light lightComponent;

    [Header("Flicker")]
    [SerializeField] float minTimeBetweenFlicker;
    [SerializeField] float maxTimeBetweenFlicker;
    [SerializeField] float minFlickerTime;
    [SerializeField] float maxFlickerTime;
    [SerializeField] float minTimeActive;
    [SerializeField] float maxTimeActive;
    bool flashLightActive = false;
    bool isFlickering = false;

    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        
        StartCoroutine(WaitForFlicker());
    }

    private void Update()
    {

    }

    IEnumerator WaitForFlicker()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBetweenFlicker, maxTimeBetweenFlicker));
        isFlickering = true;
        StartCoroutine(FlashLightFlicker());
        yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        isFlickering = false;
        flashLightActive = true;
        lightComponent.enabled = true;
        StartCoroutine(WaitForFlicker());
    }

    IEnumerator FlashLightFlicker()
    {
        yield return new WaitForSeconds(Random.Range(minTimeActive, maxTimeActive));
        if (isFlickering)
        {
            if (flashLightActive)
            {
                flashLightActive = false;
                lightComponent.enabled = false;
            }
            else
            {
                flashLightActive = true;
                lightComponent.enabled = true;
            }
            StartCoroutine(FlashLightFlicker());
        }
    }

}
