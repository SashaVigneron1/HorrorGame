using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void RespawnAtCheckpoint()
    {
        this.transform.position = gameManager.GetCheckPointManager().GetLastCheckPointSpawn().position;
        playerMovement.Respawn();
    }
}
