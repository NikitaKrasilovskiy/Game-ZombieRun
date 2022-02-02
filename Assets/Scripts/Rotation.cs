
using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    [System.Obsolete]
    void Start()
    {
        //Screen.lockCursor = false;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 20, -15) * Time.deltaTime);
    }
}