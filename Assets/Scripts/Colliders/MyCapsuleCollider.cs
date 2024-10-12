using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCapsuleCollider : MyCollider
{
    public Vector3 center;
    public float radius;
    public float elasticity = 1.0f;

    void Start()
    {
        center = new Vector3(transform.position.x, transform.position.y);
        CollisionManager.Instance.AddCollider(this);
    }

    void OnDestroy()
    {
        CollisionManager.Instance.RemoveCollider(this);
    }

    void Update()
    {
        this.center = new Vector3(transform.position.x, transform.position.y);
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
        return Vector2.Distance(this.center, otherCapsule.center) <= (this.radius + otherCapsule.radius);
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
        Vector3 direction = (this.center - otherCapsule.center).normalized;
        this.transform.position += direction * (this.radius + otherCapsule.radius - Vector2.Distance(this.center, otherCapsule.center));
    }

    private void ResolveCollisionWithCapsuleBounce(MyCapsuleCollider otherCapsule)
    {
        Vector3 direction = (this.center - otherCapsule.center).normalized;
        this.transform.position += direction * (this.radius + otherCapsule.radius - Vector2.Distance(this.center, otherCapsule.center));

        Vector3 relativeVelocity = otherCapsule.rigidBody.velocity - this.rigidBody.velocity;
        Vector3 bounceVelocity = relativeVelocity.magnitude * this.elasticity * direction;

        this.rigidBody.velocity += bounceVelocity;
    }
}
