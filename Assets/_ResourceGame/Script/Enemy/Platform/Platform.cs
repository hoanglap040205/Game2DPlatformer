using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed;
    private Transform target;

    private void Start()
    {
        target = pointA;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position,target.position) <= 0.3f)
        {
            Debug.Log(target.gameObject.name);
           target = (target == pointA) ? pointB : pointA;
            Debug.Log(target.gameObject.name);
        }
    }
}
