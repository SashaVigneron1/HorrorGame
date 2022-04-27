using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light flashLight;
    [SerializeField] AudioSource toggleSound;

    [Header("Flicker")]
    [SerializeField] float minTimeBetweenFlicker;
    [SerializeField] float maxTimeBetweenFlicker;
    [SerializeField] float minFlickerTime;
    [SerializeField] float maxFlickerTime;
    [SerializeField] float minTimeActive;
    [SerializeField] float maxTimeActive;
    bool isUsingFlashLight = false;
    bool flashLightActive = false;
    bool isFlickering = false;

    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleSound.Play();
            if (isUsingFlashLight)
            {
                flashLight.gameObject.SetActive(false);
                isUsingFlashLight = false;
                playerMovement.SetCanBeSeenByEnemies(false);
                flashLightActive = false;
                isFlickering = false;
                StopAllCoroutines();
            }
            else
            {
                flashLight.gameObject.SetActive(true);
                isUsingFlashLight = true;
                playerMovement.SetCanBeSeenByEnemies(true);
                StartCoroutine(WaitForFlicker());
            }
        }

        if (isUsingFlashLight && !playerMovement.HasJustRespawned()) playerMovement.SetCanBeSeenByEnemies(true);
    }

    IEnumerator WaitForFlicker()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBetweenFlicker, maxTimeBetweenFlicker));
        if (isUsingFlashLight)
        {
            isFlickering = true;
            StartCoroutine(FlashLightFlicker());
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            isFlickering = false;
            flashLightActive = true;
            flashLight.gameObject.SetActive(true);
            StartCoroutine(WaitForFlicker());
        }
    }

    IEnumerator FlashLightFlicker()
    {
        yield return new WaitForSeconds(Random.Range(minTimeActive, maxTimeActive));
        if (isUsingFlashLight && isFlickering)
        {
            if (flashLightActive)
            {
                flashLightActive = false;
                flashLight.gameObject.SetActive(false);
            }
            else
            {
                flashLightActive = true;
                flashLight.gameObject.SetActive(true);
            }
            StartCoroutine(FlashLightFlicker());
        }
    }

    public bool IsUsingFlashLight()
    {
        return isUsingFlashLight;
    }
}
