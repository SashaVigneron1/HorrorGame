using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;
    Enemy[] enemies;

    CheckpointManager checkpointManager;

    private void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
        enemies = FindObjectsOfType<Enemy>();
        Time.timeScale = 1.0f;
    }

    public CheckpointManager GetCheckPointManager()
    {
        return checkpointManager;
    }

    public float GetDistanceToNearestEnemy()
    {
        float shortestDistance = float.MaxValue;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.gameObject.transform.position, player.position);
            if (distance < shortestDistance) shortestDistance = distance;
        }

        return shortestDistance;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

