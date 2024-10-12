using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager
{
    private List<Collider> colliders = new List<Collider>();

    public void AddCollider(Collider collider)
    {
        colliders.Add(collider);
    }

    public void RemoveCollider(Collider collider)
    {
        colliders.Remove(collider);
    }

    private void FixedUpdate()
}
