using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineEnabled : MonoBehaviour
{
    private ParticleSystem effects;
    void Start()
    {
        effects = GetComponent<ParticleSystem>();
        effects.Stop();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6f);
        effects.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wine"))
        {
            effects.Play();            
            StartCoroutine(Wait());
        }
    }
}