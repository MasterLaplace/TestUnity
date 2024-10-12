using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCapsuleCollider : MyCollider
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float length;

    public float radius;
    public float elasticity = 1.0f;

    void Start()
    {
        pointA = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pointB = new Vector3(transform.position.x, transform.position.y + length, transform.position.z);

        CollisionManager.Instance.AddCollider(this);
    }

    void OnDestroy()
    {
        CollisionManager.Instance.RemoveCollider(this);
    }

    void Update()
    {
        this.pointA = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        this.pointB = new Vector3(transform.position.x, transform.position.y + length, transform.position.z);
        CollisionManager.Instance.CheckCollisions(this);
    }


    public override bool IsColliding(MyCollider other)
    {
        if (other is MyCapsuleCollider otherCapsule)
            return IsCollidingWithCapsule(otherCapsule);

        return false;
    }

    private bool IsCollidingWithCapsule(MyCapsuleCollider otherCapsule)
    {
        return 0 <= 0;
    }

    public override void ResolveCollision(MyCollider other)
    {
        if (other is MyCapsuleCollider otherCapsule)
        {
            if (this.elasticity == 0)
                ResolveCollisionWithCapsuleImobilize(otherCapsule);
            else
                ResolveCollisionWithCapsuleBounce(otherCapsule);
        }
    }

    private void ResolveCollisionWithCapsuleImobilize(MyCapsuleCollider otherCapsule)
    {
    }

    private void ResolveCollisionWithCapsuleBounce(MyCapsuleCollider otherCapsule)
    {
    }
}
