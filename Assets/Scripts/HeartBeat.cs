using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    [SerializeField] float m_MaxRangeForHeartBeat;
    [SerializeField] AudioSource heartBeat;
    GameManager gameManager;

    bool isPlayingSound = false;
    float clipLength;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        clipLength = heartBeat.clip.length;
        StartCoroutine(WaitForCheck());
    }


    IEnumerator WaitForCheck()
    {
        yield return new WaitForSeconds(clipLength);
        float distance = gameManager.GetDistanceToNearestEnemy();
        if (distance < m_MaxRangeForHeartBeat && !isPlayingSound)
        {
            heartBeat.Play();
            float volume = 1 - (distance / m_MaxRangeForHeartBeat);
            //ToDo: Set Speed
            heartBeat.volume = volume;
            isPlayingSound = true;
        }
        else
        {
            heartBeat.Stop();
            isPlayingSound = false;
        }
            isPlayingSound = false;
        StartCoroutine(WaitForCheck());
    }
}
