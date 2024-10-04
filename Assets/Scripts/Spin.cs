// using System;
// using UnityEngine;

// public class Spin : MonoBehaviour
// {
//     [SerializeField] private float speed = 1.0f;
//     [SerializeField] private Vector3 axis = Vector3.up;

//     // private void Awake()
//     // {
//     // }

//     // // Start is called before the first frame update
//     // private void Start()
//     // {
//     // }

//     // private void OnEnable()
//     // {
//     //     throw new NotImplementedException();
//     // }

//     // void FixedUpdate()
//     // {
//     // }

//     // Update is called once per frame
//     private void Update()
//     {
//         transform.Rotate(axis, speed * Time.deltaTime);
//     }

//     // private void LateUpdate()
//     // {
//     //     throw new NotImplementedException();
//     // }

//     // private void OnDisable()
//     // {
//     //     throw new NotImplementedException();
//     // }

//     // private void OnDestroy()
//     // {
//     //     throw new NotImplementedException();
//     // }
// }

using UnityEngine;

public class CustomBoxCollider : MonoBehaviour
{
    // Public variables for collider size and center
    public Vector3 boxSize = Vector3.one;
    public Vector3 boxCenter = Vector3.zero;

    // The object this collider is attached to
    private Transform objectTransform;

    private void Start()
    {
        objectTransform = transform;
    }

    // Visualize the custom collider in the Scene view for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + boxCenter, boxSize);
    }

    // Check for collision with another CustomBoxCollider
    public bool IsColliding(CustomBoxCollider other)
    {
        Vector3 thisMin = objectTransform.position + boxCenter - boxSize / 2;
        Vector3 thisMax = objectTransform.position + boxCenter + boxSize / 2;

        Vector3 otherMin = other.objectTransform.position + other.boxCenter - other.boxSize / 2;
        Vector3 otherMax = other.objectTransform.position + other.boxCenter + other.boxSize / 2;

        // AABB collision check
        bool isCollidingX = thisMax.x > otherMin.x && thisMin.x < otherMax.x;
        bool isCollidingY = thisMax.y > otherMin.y && thisMin.y < otherMax.y;
        bool isCollidingZ = thisMax.z > otherMin.z && thisMin.z < otherMax.z;

        return isCollidingX && isCollidingY && isCollidingZ;
    }

    private void Update()
    {
        // Example usage: Check if colliding with another box collider
        // Note: You would likely manage this check more efficiently in a manager class
        CustomBoxCollider[] colliders = FindObjectsOfType<CustomBoxCollider>();
        foreach (var otherCollider in colliders)
        {
            if (otherCollider != this && IsColliding(otherCollider))
            {
                Debug.Log(gameObject.name + " is colliding with " + otherCollider.gameObject.name);
            }
        }
    }
}
