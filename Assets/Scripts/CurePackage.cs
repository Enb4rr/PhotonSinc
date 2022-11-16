using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurePackage : MonoBehaviour
{
    private AudioSource sound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TankHealth health = collision.gameObject.GetComponent<TankHealth>();
            health.Heal(100);
            sound.Play();
            Destroy(gameObject);
        }

    }
}
