using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : Collider
{
    public Vector3 center;
    public float radius;

    public CircleCollider(Vector3 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }

    public override bool IsColliding(Collider other)
    {
        if (other is CircleCollider otherCircle)
        {
            return IsCollidingWithCircle(otherCircle);
        }
        if (other is BoxCollider otherBox)
        {
            return IsCollidingWithBox(otherBox);
        }
        return false;
    }

    private bool IsCollidingWithCircle(CircleCollider otherCircle)
    {
        return Vector2.Distance(this.center, otherCircle.center) <= (this.radius + otherCircle.radius);
    }

    private bool IsCollidingWithBox(BoxCollider otherBox)
    {
        Vector3 closestPoint = new Vector3(
            Mathf.Clamp(this.center.x, otherBox.center.x - otherBox.size.x / 2, otherBox.center.x + otherBox.size.x / 2),
            Mathf.Clamp(this.center.y, otherBox.center.y - otherBox.size.y / 2, otherBox.center.y + otherBox.size.y / 2),
            Mathf.Clamp(this.center.z, otherBox.center.z - otherBox.size.z / 2, otherBox.center.z + otherBox.size.z / 2)
        );

        return Vector3.Distance(this.center, closestPoint) <= this.radius;
    }

    public override void ResolveCollision(Collider other)
    {
        if (other is CircleCollider otherCircle)
        {
            ResolveCollisionWithCircle(otherCircle);
        }
        if (other is BoxCollider otherBox)
        {
            ResolveCollisionWithBox(otherBox);
        }
    }

    private void ResolveCollisionWithCircle(CircleCollider otherCircle)
    {
        Vector3 direction = (this.center - otherCircle.center).normalized;
        this.center += direction * (this.radius + otherCircle.radius - Vector2.Distance(this.center, otherCircle.center));
    }

    private void ResolveCollisionWithBox(BoxCollider otherBox)
    {
        Vector3 closestPoint = new Vector3(
            Mathf.Clamp(this.center.x, otherBox.center.x - otherBox.size.x / 2, otherBox.center.x + otherBox.size.x / 2),
            Mathf.Clamp(this.center.y, otherBox.center.y - otherBox.size.y / 2, otherBox.center.y + otherBox.size.y / 2),
            Mathf.Clamp(this.center.z, otherBox.center.z - otherBox.size.z / 2, otherBox.center.z + otherBox.size.z / 2)
        );

        Vector3 direction = (this.center - closestPoint).normalized;
        this.center += direction * (this.radius - Vector3.Distance(this.center, closestPoint));
    }

    public abstract void OnCollisionEnter(Collision collision) { /* Implémentation */ }
    public abstract void OnCollisionStay(Collision collision) { /* Implémentation */ }
    public abstract void OnCollisionExit(Collision collision) { /* Implémentation */ }
}
