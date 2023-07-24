using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Fields

    private float _moveSpeed;
    private float _rotationSpeed;
    private Rigidbody _rigidbody;
    public ParticleSystem _engineParticles;

    Vector3 movement = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private Quaternion targetRotation;

    // Tmp values
    float zMovement, xMovement;

    #endregion Fields

    #region Mono

    private void Start()
    {
        // Getting Stats
        _moveSpeed = PlayerController.instance.moveSpeed;
        _rotationSpeed = PlayerController.instance.rotationSpeed;
        _rigidbody = PlayerController.instance.playerRigidbody;

        // Disable rotation from physics forces
        _rigidbody.freezeRotation = true;
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        // Get the input from the W, S, A, and D keys.
        zMovement = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys
        xMovement = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys

        // Calculate the movement direction.
        moveDirection.Set(xMovement, 0f, zMovement);

        // Normalize the movement direction so diagonal movement isn't faster.
        moveDirection.Normalize();

        // Rotate the player based on the movement direction.
        if (moveDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            if(!_engineParticles.isPlaying) _engineParticles.Play();
        }
        else
            if (_engineParticles.isPlaying) _engineParticles.Stop();

        // Move the player based on the calculated direction and the move speed.
        movement = moveDirection * _moveSpeed * Time.deltaTime;
        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
    }

    #endregion Mono

    #region Methods

    public void SetMovementSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    public void SetRotationSpeed(float speed)
    {
        _rotationSpeed = speed;
    }

    public void SetRigidbody(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void SetEngineParticles(ParticleSystem particle)
    {
        _engineParticles = particle;
    }

    public float GetMovementSpeed()
    {
        return _moveSpeed;
    }

    public float GetRotationSpeed()
    {
        return _rotationSpeed;
    }

    #endregion Methods

}
