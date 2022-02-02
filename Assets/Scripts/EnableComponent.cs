using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableComponent : MonoBehaviour
{
    private ParticleSystem effect;
    void Start()
    {
        effect = GetComponent<ParticleSystem>();
        effect.Stop();
    }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
            effect.Play();
            }
        }
}
