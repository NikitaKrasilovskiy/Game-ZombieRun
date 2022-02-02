using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDecor : MonoBehaviour
{
    public GameObject[] spawnees;
    public float minDelay, maxDelay;
    float nextLaunchTime;
    int n;
    void Update()
    {
        if (Time.time > nextLaunchTime)
        {
            n = Random.Range(0, spawnees.Length);
            Instantiate(spawnees[n], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}