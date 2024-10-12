using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collider
{
    public abstract bool IsColliding(Collider other);
    public abstract void ResolveCollision(Collider other);
    public abstract void OnCollisionEnter(Collision collision);
    public abstract void OnCollisionStay(Collision collision);
    public abstract void OnCollisionExit(Collision collision);
}
