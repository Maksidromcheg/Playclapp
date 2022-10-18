using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed;
    public float distance;
    private Vector3 startPos;
        
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position += new Vector3(1f, 0, 0) * Time.deltaTime * speed;
        if (Vector3.Distance(startPos, transform.position) >= distance)
        {
            Destroy(gameObject);
        }
    }
}
