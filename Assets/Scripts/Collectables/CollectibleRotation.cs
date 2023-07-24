using UnityEngine;

public class CollectibleRotation : MonoBehaviour
{

    #region Fields

    public float rotationSpeed = 100f; // Adjust this value to control the rotation speed.

    public Rigidbody rigidbodyCollectible;

    #endregion Fields

    #region Mono

    void FixedUpdate()
    {
        // Add a rotation force to the Rigidbody to make the coin spin on its own axis.
        rigidbodyCollectible.AddTorque(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
    }

    #endregion Mono

}