using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoxCollider : MyCollider
{
    public Vector3 center;
    public Vector3 size;
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
        Gizmos.DrawWireCube(this.center, this.size);
    }

    void Update()
    {
        this.center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        CollisionManager.Instance.CheckCollisions(this);
    }


    public override bool IsColliding(MyCollider other)
    {
        if (other is MyBoxCollider otherBox)
            return IsCollidingWithBox(otherBox);

        if (other is MySphereCollider otherSphere)
            return IsCollidingWithSphere(otherSphere);

        return false;
    }

    private bool IsCollidingWithBox(MyBoxCollider otherBox)
    {
        return (Mathf.Abs(this.center.x - otherBox.center.x) * 2 < (this.size.x + otherBox.size.x)) &&
               (Mathf.Abs(this.center.y - otherBox.center.y) * 2 < (this.size.y + otherBox.size.y)) &&
               (Mathf.Abs(this.center.z - otherBox.center.z) * 2 < (this.size.z + otherBox.size.z));
    }

    private bool IsCollidingWithSphere(MySphereCollider otherSphere)
    {
        float x = Mathf.Max(this.center.x - this.size.x / 2, Mathf.Min(otherSphere.center.x, this.center.x + this.size.x / 2));
        float y = Mathf.Max(this.center.y - this.size.y / 2, Mathf.Min(otherSphere.center.y, this.center.y + this.size.y / 2));
        float z = Mathf.Max(this.center.z - this.size.z / 2, Mathf.Min(otherSphere.center.z, this.center.z + this.size.z / 2));

        float distance = Mathf.Sqrt((x - otherSphere.center.x) * (x - otherSphere.center.x) +
                                    (y - otherSphere.center.y) * (y - otherSphere.center.y) +
                                    (z - otherSphere.center.z) * (z - otherSphere.center.z));

        return distance < otherSphere.radius;
    }

    public override void ResolveCollision(MyCollider other)
    {
        if (other is MyBoxCollider otherBox)
        {
            if (this.elasticity == 0)
                ResolveCollisionWithBoxImobilize(otherBox);
            else
                ResolveCollisionWithBoxBounce(otherBox);
        }

        if (other is MySphereCollider otherSphere)
        {
            if (this.elasticity == 0)
                ResolveCollisionWithSphereImobilize(otherSphere);
            else
                ResolveCollisionWithSphereBounce(otherSphere);
        }
    }

    private void ResolveCollisionWithBoxImobilize(MyBoxCollider otherBox)
    {
        Vector3 direction = (otherBox.center - this.center).normalized;
        float overlapX = (this.size.x + otherBox.size.x) / 2 - Mathf.Abs(this.center.x - otherBox.center.x);
        float overlapY = (this.size.y + otherBox.size.y) / 2 - Mathf.Abs(this.center.y - otherBox.center.y);
        float overlapZ = (this.size.z + otherBox.size.z) / 2 - Mathf.Abs(this.center.z - otherBox.center.z);

        Vector3 displacement = new Vector3(
            overlapX * direction.x,
            overlapY * direction.y,
            overlapZ * direction.z
        );

        this.transform.position -= displacement / 2;
        otherBox.transform.position += displacement / 2;
    }

    private void ResolveCollisionWithBoxBounce(MyBoxCollider otherBox)
    {
        Vector3 direction = (otherBox.center - this.center).normalized;
        float overlapX = (this.size.x + otherBox.size.x) / 2 - Mathf.Abs(this.center.x - otherBox.center.x);
        float overlapY = (this.size.y + otherBox.size.y) / 2 - Mathf.Abs(this.center.y - otherBox.center.y);
        float overlapZ = (this.size.z + otherBox.size.z) / 2 - Mathf.Abs(this.center.z - otherBox.center.z);

        Vector3 displacement = new Vector3(
            overlapX * direction.x,
            overlapY * direction.y,
            overlapZ * direction.z
        );

        this.transform.position -= displacement / 2;
        otherBox.transform.position += displacement / 2;

        Vector3 relativeVelocity = this.rigidBody.velocity - otherBox.rigidBody.velocity;
        float velocityAlongNormal = Vector3.Dot(relativeVelocity, direction);

        if (velocityAlongNormal > 0)
            return;

        float j = -(1 + this.elasticity) * velocityAlongNormal;
        j /= 1 / this.rigidBody.Mass + 1 / otherBox.rigidBody.Mass;

        Vector3 impulse = j * direction;
        this.rigidBody.velocity -= 1 / this.rigidBody.Mass * impulse;
        otherBox.rigidBody.velocity += 1 / otherBox.rigidBody.Mass * impulse;

        float percent = 0.2f;
        float slop = 0.01f;
        Vector3 correction = Mathf.Max(overlapX - slop, 0) / (1 / this.rigidBody.Mass + 1 / otherBox.rigidBody.Mass + percent) * direction;
        this.transform.position -= 1 / this.rigidBody.Mass * correction;
        otherBox.transform.position += 1 / otherBox.rigidBody.Mass * correction;
    }

    private void ResolveCollisionWithSphereImobilize(MySphereCollider otherSphere)
    {
        Vector3 direction = (otherSphere.center - this.center).normalized;
        float distance = Vector3.Distance(this.center, otherSphere.center);
        float overlap = this.size.x / 2 + otherSphere.radius - distance;

        Vector3 displacement = overlap * direction;
        this.transform.position -= displacement / 2;
        otherSphere.transform.position += displacement / 2;

        Vector3 relativeVelocity = otherSphere.rigidBody.velocity - this.rigidBody.velocity;
        float velocityAlongNormal = Vector3.Dot(relativeVelocity, direction);

        if (velocityAlongNormal > 0)
            return;

        float j = -(1 + this.elasticity) * velocityAlongNormal;
        j /= 1 / this.rigidBody.Mass + 1 / otherSphere.rigidBody.Mass;

        Vector3 impulse = j * direction;
        this.rigidBody.velocity -= 1 / this.rigidBody.Mass * impulse;
        otherSphere.rigidBody.velocity += 1 / otherSphere.rigidBody.Mass * impulse;

        float percent = 0.2f;
        float slop = 0.01f;
        Vector3 correction = Mathf.Max(overlap - slop, 0) / (1 / this.rigidBody.Mass + 1 / otherSphere.rigidBody.Mass + percent) * direction;
        this.transform.position -= 1 / this.rigidBody.Mass * correction;
        otherSphere.transform.position += 1 / otherSphere.rigidBody.Mass * correction;
    }

    private void ResolveCollisionWithSphereBounce(MySphereCollider otherSphere)
    {
        Vector3 direction = (otherSphere.center - this.center).normalized;
        float distance = Vector3.Distance(this.center, otherSphere.center);
        float overlap = this.size.x / 2 + otherSphere.radius - distance;

        Vector3 displacement = overlap * direction;
        this.transform.position -= displacement / 2;
        otherSphere.transform.position += displacement / 2;

        Vector3 relativeVelocity = otherSphere.rigidBody.velocity - this.rigidBody.velocity;
        float velocityAlongNormal = Vector3.Dot(relativeVelocity, direction);

        if (velocityAlongNormal > 0)
            return;

        float j = -(1 + this.elasticity) * velocityAlongNormal;
        j /= 1 / this.rigidBody.Mass + 1 / otherSphere.rigidBody.Mass;

        Vector3 impulse = j * direction;
        this.rigidBody.velocity -= 1 / this.rigidBody.Mass * impulse;
        otherSphere.rigidBody.velocity += 1 / otherSphere.rigidBody.Mass * impulse;

        float percent = 0.2f;
        float slop = 0.01f;
        Vector3 correction = Mathf.Max(overlap - slop, 0) / (1 / this.rigidBody.Mass + 1 / otherSphere.rigidBody.Mass + percent) * direction;
        this.transform.position -= 1 / this.rigidBody.Mass * correction;
        otherSphere.transform.position += 1 / otherSphere.rigidBody.Mass * correction;
    }
}
