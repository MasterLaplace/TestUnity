using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySphereCollider : MyCollider
{
    public Vector3 center;
    public float radius;
    public float elasticity = 1.0f;

    void Start()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        CollisionManager.Instance.AddCollider(this);
    }

    void OnDestroy()
    {
        CollisionManager.Instance.RemoveCollider(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.center, this.radius);
    }

    void Update()
    {
        this.center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        CollisionManager.Instance.CheckCollisions(this);
    }


    public override bool IsColliding(MyCollider other)
    {
        if (other is MySphereCollider otherSphere)
            return IsCollidingWithSphere(otherSphere);

        return false;
    }

    private bool IsCollidingWithSphere(MySphereCollider otherSphere)
    {
        return Vector2.Distance(this.center, otherSphere.center) <= (this.radius + otherSphere.radius);
    }

    public override void ResolveCollision(MyCollider other)
    {
        if (other is MySphereCollider otherSphere)
        {
            if (this.elasticity == 0)
                ResolveCollisionWithSphereImobilize(otherSphere);
            else
                ResolveCollisionWithSphereBounce(otherSphere);
        }
    }

    private void ResolveCollisionWithSphereImobilize(MySphereCollider otherSphere)
    {
        Vector3 direction = (this.center - otherSphere.center).normalized;
        this.transform.position += direction * (this.radius + otherSphere.radius - Vector2.Distance(this.center, otherSphere.center));
    }

    private void ResolveCollisionWithSphereBounce(MySphereCollider otherSphere)
    {
        Vector3 direction = (this.center - otherSphere.center).normalized;
        this.transform.position += direction * (this.radius + otherSphere.radius - Vector2.Distance(this.center, otherSphere.center));

        Vector3 relativeVelocity = otherSphere.rigidBody.velocity - this.rigidBody.velocity;
        Vector3 bounceVelocity = relativeVelocity.magnitude * this.elasticity * direction;

        this.rigidBody.velocity += bounceVelocity;
    }
}
