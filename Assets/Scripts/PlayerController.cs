using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
    }

    public void Move(Vector3 moveVelocity)
    {
        velocity = moveVelocity;
    }

    internal void LookAt(Vector3 lookPoint)
    {
        //var heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        var heightCorrectedPoint = lookPoint.WithY(transform.position.y);
        transform.LookAt(heightCorrectedPoint);
    }
}
