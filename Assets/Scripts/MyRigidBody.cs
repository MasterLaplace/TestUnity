using UnityEngine;

[RequireComponent(typeof(Transform))]
public class MyRigidBody : MonoBehaviour
{
    [SerializeField] private Vector3 velocity = Vector3.zero; // Vitesse de l'objet
    [SerializeField] private Vector3 angularVelocity = Vector3.zero; // Vitesse angulaire de l'objet
    [SerializeField] private Vector3 acceleration = Vector3.zero; // Accélération de l'objet
    [SerializeField] private Vector3 angularAcceleration = Vector3.zero; // Accélération angulaire de l'objet
    [SerializeField] private Vector3 impulse = Vector3.zero; // Impulsion appliquée à l'objet
    [SerializeField] private Vector3 gravityForce = Vector3.zero; // Force de gravité appliquée à l'objet
    public float Mass = 1; // Masse de l'objet
    public float Inertia = 1; // Inertie de l'objet

    // [SerializeField] private float drag; // Coefficient de trainée
    [SerializeField] private float gravity = 9.81f; // Gravité terrestre
    [SerializeField] private float friction = 0.1f; // Coefficient de frottement


    [SerializeField] private Vector3 force = Vector3.zero; // Force appliquée à l'objet
    [SerializeField] private Vector3 torque = Vector3.zero; // Couple appliqué à l'objet


    public void AddForce(Vector3 newForce)
    {
        gravityForce = newForce;
        Debug.Log("AddForce : " + gravityForce);
    }

    private void FixedUpdate()
    {
        transform.position += velocity * Time.fixedDeltaTime + 1/2 * acceleration * (Time.fixedDeltaTime * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + angularVelocity * Time.fixedDeltaTime + 1/2 * angularAcceleration * (Time.fixedDeltaTime * Time.fixedDeltaTime));

        velocity += acceleration * Time.fixedDeltaTime;
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;

        // // Apply force
        // Debug.Log("ApplyForce : " + force);
        // acceleration = (force / Mass) /*- (friction * velocity)*/;

        // // Apply torque
        // angularAcceleration = torque / Mass;

        // // Reset forces and torques
        // force = Vector3.zero;
        // torque = Vector3.zero;

    }
}

// [SerializeField] private float staticFrictionCoefficient = 0.5f;
// [SerializeField] private float normalForce = 9.81f; // Par exemple, pour un objet de 1 kg sur une surface horizontale

// Vector3 frictionForce = Vector3.zero;
// if (velocity.magnitude > 0)
// {
//     frictionForce = friction * velocity.normalized * normalForce;
// }

// acceleration = (force / mass) - frictionForce;
