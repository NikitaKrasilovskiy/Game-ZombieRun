using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaerEnable : MonoBehaviour
{
    private ParticleSystem effects;
    void Start()
    {
        effects = GetComponent<ParticleSystem>();
        effects.Stop();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);        
        effects.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pepper"))
        {
            effects.Play();            
            StartCoroutine(Wait());
        }
    }
}