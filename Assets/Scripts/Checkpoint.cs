using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] bool hasInteracted = false;
    bool isCloseEnoughToInteract = false;
    bool isInteracting = false;

    [Header("General")]
    [SerializeField] float interactingTime;
    float accInteractingTime;

    [Header("Slider Animation")]
    [SerializeField] GameObject sliderAnim;
    [SerializeField] Slider slider;
    [SerializeField] Text sliderText;

    [Header("Other")]
    [SerializeField] GameObject interactMenu;
    [SerializeField] CheckpointManager checkpointManager;
    [SerializeField] Light statusLight;
    [SerializeField] Transform spawnPos;

    PlayerMovement playerMovement;

    private void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        sliderAnim.SetActive(false);
        interactMenu.SetActive(false);

        slider.maxValue = interactingTime;
        slider.minValue = 0.0f;
        if (!hasInteracted) statusLight.color = Color.red;
        else statusLight.color = Color.green;
    }

    private void Update()
    {
        if (isCloseEnoughToInteract && !isInteracting && playerMovement.IsAlive())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isInteracting = true;
                interactMenu.SetActive(false);
                sliderAnim.SetActive(true);
                playerMovement.enabled = false;
            }
        }

        if (isInteracting)
        {
            if (!playerMovement.IsAlive())
            {
                isInteracting = false;
                accInteractingTime = 0.0f;
                playerMovement.enabled = true;
                sliderAnim.SetActive(false);
            }

            accInteractingTime += Time.deltaTime;
            slider.value = accInteractingTime;
            sliderText.text = "Interacting Time: " + (Mathf.Round((interactingTime - accInteractingTime) * 100) / 100).ToString() + "s";

            if (accInteractingTime >= interactingTime)
            {
                FindObjectOfType<PlayerMovement>().enabled = true;
                isInteracting = false;
                hasInteracted = true;
                sliderAnim.SetActive(false);
                statusLight.color = Color.green;

                checkpointManager.SetLastCheckPoint(this);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasInteracted && playerMovement.IsAlive())
        {
            interactMenu.SetActive(true);
            isCloseEnoughToInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !hasInteracted)
        {
            interactMenu.SetActive(false);
            isCloseEnoughToInteract = false;
        }
    }

    public Transform GetSpawnPos()
    {
        return spawnPos;
    }
}
