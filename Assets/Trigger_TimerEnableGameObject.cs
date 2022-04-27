using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_TimerEnableGameObject : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects;
    [SerializeField] AudioSource enableSound;
    [SerializeField] float timeActive;

    bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            if (other.tag == "Player")
            {
                foreach (GameObject obj in gameObjects)
                {
                    obj.SetActive(true);
                }
                isActive = true;
                enableSound.Play();
                StartCoroutine(WaitForDisable());
            }
        }
    }

    IEnumerator WaitForDisable()
    {
        yield return new WaitForSeconds(timeActive);
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }
}
