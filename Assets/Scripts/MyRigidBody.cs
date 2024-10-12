using UnityEngine;

[RequireComponent(typeof(Transform))]
public class MyRigidBody : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero; // Vitesse de l'objet
    [SerializeField] private Vector3 angularVelocity = Vector3.zero; // Vitesse angulaire de l'objet
    [SerializeField] private Vector3 acceleration = Vector3.zero; // Accélération de l'objet
    [SerializeField] private Vector3 angularAcceleration = Vector3.zero; // Accélération angulaire de l'objet
    public float Mass = 1; // Masse de l'objet
    [SerializeField] private float friction = 0.1f; // Coefficient de frottement


    [SerializeField] private Vector3 force = Vector3.zero; // Force appliquée à l'objet

    [SerializeField] private bool isKinematic = false; // Est-ce que l'objet est kinématique


    public void AddForce(Vector3 newForce)
    {
        if (!isKinematic)
            return;

        force += newForce;
    }

    public float GetKineticEnergy()
    {
        return 0.5f * Mass * velocity.sqrMagnitude;
    }

    public float GetPotentialEnergy()
    {
        return Mass * Vector3.Dot(force, transform.position);
    }

    public float GetMechanicalEnergy()
    {
        return GetKineticEnergy() + GetPotentialEnergy();
    }

    private void FixedUpdate()
    {
        if (!isKinematic)
            return;
        transform.position += velocity * Time.fixedDeltaTime + 1/2 * acceleration * (Time.fixedDeltaTime * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + angularVelocity * Time.fixedDeltaTime + 1/2 * angularAcceleration * (Time.fixedDeltaTime * Time.fixedDeltaTime));

        velocity += acceleration * Time.fixedDeltaTime;
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;

        Debug.Log("Energie cinétique: " + GetKineticEnergy());
        Debug.Log("Energie potentielle: " + GetPotentialEnergy());
        Debug.Log("Energie totale: " + GetMechanicalEnergy());

        acceleration = (force / Mass) - (friction * velocity);
        angularAcceleration = (force / Mass) - (friction * angularVelocity);

        force = Vector3.zero;

    }
}
