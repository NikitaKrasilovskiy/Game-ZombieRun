using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public GameObject[] spawnees;
    public float minDelay, maxDelay;
    float nextLaunchTime;
    int n;
    void Update()
    {
        float positionX = Random.Range(-transform.localScale.x/2, transform.localScale.x/2);

        if (Time.time > nextLaunchTime)
        {
            n = Random.Range(0, spawnees.Length);
            Instantiate(spawnees[n], new Vector3(positionX, transform.position.y, transform.position.z), Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}