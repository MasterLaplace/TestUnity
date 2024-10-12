using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private readonly List<MyCollider> colliders = new();

    public void AddCollider(MyCollider collider)
    {
        colliders.Add(collider);
    }

    public void RemoveCollider(MyCollider collider)
    {
        colliders.Remove(collider);
    }

    public void CheckCollisions(MyCollider collider)
    {
        foreach (MyCollider other in colliders)
        {
            if (other == collider)
                continue;

            if (collider.IsColliding(other))
            {
                collider.ResolveCollision(other);
                other.ResolveCollision(collider);
            }
        }
    }

    public void CheckCollisions()
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            for (int j = i + 1; j < colliders.Count; j++)
            {
                if (colliders[i].IsColliding(colliders[j]))
                {
                    colliders[i].ResolveCollision(colliders[j]);
                    colliders[j].ResolveCollision(colliders[i]);
                }
            }
        }
    }

    private static CollisionManager instance;
    public static CollisionManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("CollisionManager").AddComponent<CollisionManager>();

            return instance;
        }
    }
}
