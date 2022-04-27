using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] GameObject jumpScareImage;
    [SerializeField] GameObject finishScreen;
    [SerializeField] AudioSource jumpScareSound;
    [SerializeField] AudioClip jumpScareClip;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            StartCoroutine(JumpScare());
        }
        else if (other.tag == "Checkpoint")
        {
            playerMovement.SetCanBeSeenByEnemies(true);
        }
        else if (other.tag == "Finish")
        {
            Cursor.visible = true;
            finishScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            playerMovement.SetCanBeSeenByEnemies(false);
        }
    }

    IEnumerator JumpScare()
    {
        AudioSource.PlayClipAtPoint(jumpScareClip, this.transform.position);
        jumpScareImage.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        jumpScareSound.Stop();
        jumpScareImage.SetActive(false);
        playerMovement.SetAlive(false);

        GetComponent<Player>().RespawnAtCheckpoint();
    }

}
