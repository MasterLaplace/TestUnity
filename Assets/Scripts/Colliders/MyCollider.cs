using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyRigidBody))]
public abstract class MyCollider : MonoBehaviour
{
    public MyRigidBody rigidBody;

    public abstract bool IsColliding(MyCollider other);
    public abstract void ResolveCollision(MyCollider other);
}
