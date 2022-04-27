using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<AudioClip> growlSounds;

    [SerializeField] float m_MinTimeBeforeGrowl;
    [SerializeField] float m_MaxTimeBeforeGrowl;

    private void Start()
    {
        StartCoroutine(Growl());
    }

    IEnumerator Growl()
    {
        yield return new WaitForSeconds(Random.Range(m_MinTimeBeforeGrowl, m_MaxTimeBeforeGrowl));
        AudioSource.PlayClipAtPoint(growlSounds[Random.Range(0, growlSounds.Count)], this.transform.position);
        StartCoroutine(Growl());
    }
}
