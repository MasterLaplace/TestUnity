using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleCollider : Collider
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float radius;

    public CapsuleCollider(Vector3 pointA, Vector3 pointB, float radius)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.radius = radius;
    }

    public override bool IsColliding(Collider other) { /* Implémentation */ }
    public override void ResolveCollision(Collider other) { /* Implémentation */ }

    public abstract void OnCollisionEnter(Collision collision) { /* Implémentation */ }
    public abstract void OnCollisionStay(Collision collision) { /* Implémentation */ }
    public abstract void OnCollisionExit(Collision collision) { /* Implémentation */ }
}
