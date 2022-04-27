using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] Checkpoint lastCheckPoint;

    public void SetLastCheckPoint(Checkpoint checkpoint)
    {
        lastCheckPoint = checkpoint;
    }

    public Transform GetLastCheckPointSpawn()
    {
        return lastCheckPoint.GetSpawnPos();
    }
}
