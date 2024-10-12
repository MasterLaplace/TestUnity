using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static List<GravityManager> Bodies { get; private set; }
    public const float gravityConstant = 6.67430e-11f;
    public MyRigidBody MyRigidBody;

    private void OnEnable()
    {
        Bodies ??= new List<GravityManager>();

        Bodies.Add(this);
    }

    private void OnDisable()
    {
        Bodies.Remove(this);
    }

    private void FixedUpdate()
    {
        foreach (GravityManager otherBody in Bodies)
        {
            if (this == otherBody)
                continue;

            MyRigidBody otherMyRigidBody = otherBody.MyRigidBody;

            Vector3 direction = otherBody.transform.position - transform.position;
            float distance = direction.magnitude;

            float forceMagnitude = MyRigidBody.Mass * otherMyRigidBody.Mass / (distance * distance);
            Vector3 force = forceMagnitude * direction.normalized;

            MyRigidBody.AddForce(force);
        }
    }
}
