using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    [SerializeField] ParticleSystem m_ParticleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TankHealth health = collision.gameObject.GetComponent<TankHealth>();
            health.TakeDamage(50);
            m_ParticleSystem.Play();
            Destroy(gameObject);
        }
        
    }
}
