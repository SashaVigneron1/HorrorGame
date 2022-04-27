using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_EnableGameObject : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects;
    [SerializeField] AudioSource enableSound;

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
            }
        }
    }
}
