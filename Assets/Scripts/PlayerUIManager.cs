using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] Slider staminaSlider;

    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        //staminaSlider.minValue = playerMovement.GetMinStaminaToSprint();
    }

    private void Update()
    {
        staminaSlider.value = playerMovement.GetStamina() / playerMovement.GetMaxStamina();
    }
}
