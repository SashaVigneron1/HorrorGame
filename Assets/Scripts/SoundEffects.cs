using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] float m_MinTimeBetweenSound;
    [SerializeField] float m_MaxTimeBetweenSound;

    private void Start()
    {
        StartCoroutine(PlayRandomClip());
    }

    IEnumerator PlayRandomClip()
    {
        yield return new WaitForSeconds(Random.Range(m_MinTimeBetweenSound, m_MaxTimeBetweenSound));
        int audioSourceIndex = Random.Range(0, audioSources.Count);
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Count)], audioSources[audioSourceIndex].gameObject.transform.position);
        StartCoroutine(PlayRandomClip());
    }
}
