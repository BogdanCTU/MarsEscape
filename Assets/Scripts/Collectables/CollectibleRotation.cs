using UnityEngine;

public class CollectibleRotation : MonoBehaviour
{

    #region Fields

    // Adjust this value to control the rotation speed.
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Rigidbody rigidBody;

    #endregion Fields

    #region Mono

    void FixedUpdate()
    {
        // Add a rotation force to the Rigidbody to make the coin spin on its own axis.
        rigidBody.AddTorque(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
    }

    #endregion Mono

}