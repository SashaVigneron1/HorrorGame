using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_MoveGameObject : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] Transform destTransform;

    bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            if (other.tag == "Player")
            {
                gameObject.transform.position = destTransform.position;
                isActive = true;
            }
        }
    }
}
