using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionZ : MonoBehaviour
{
    public float speedX;
    public float speedZ;
    public float positZ;
    public float StartPositionZ = 265;
    void Update()
    {
        transform.Translate(speedX * Time.deltaTime, 0, speedZ * Time.deltaTime);

        if (transform.position.z < positZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, StartPositionZ);
        }
    }
}