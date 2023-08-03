using UnityEngine;

/// <summary>
/// Auth : Bob
/// A simple script that controls player movement
/// </summary>

public class PlayerMovement : MonoBehaviour
{

    #region Fields

    [Header("Player Stats:")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    [Space]
    [Header("Other Components:")]
    [SerializeField] private ParticleSystem _engineParticles;

    // Player Components
    private Rigidbody _rigidbody;

    // Movement Variables
    private Vector3 _movement = Vector3.zero;
    private Vector3 _moveDirection = Vector3.zero;
    private Quaternion _targetRotation;
    private float _zMovement, _xMovement;

    #endregion Fields

    #region Mono

    void FixedUpdate()
    {
        PlayerMove();
    }

    #endregion Mono

    #region Methods

    private void PlayerMove()
    {
        // Get the input from the W, S, A, and D keys
        _zMovement = Input.GetAxis("Vertical");
        _xMovement = Input.GetAxis("Horizontal");

        // Calculate the movement direction
        _moveDirection.Set(_xMovement, 0f, _zMovement);

        // Normalize the movement direction so diagonal movement isn't faster
        _moveDirection.Normalize();

        // Rotate the player based on the movement direction
        if (_moveDirection != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
            if (!_engineParticles.isPlaying) _engineParticles.Play();
        }
        else if (_engineParticles.isPlaying)
            _engineParticles.Stop();

        // Move the player based on the calculated direction and the move speed
        _movement = _moveDirection * _speed * Time.deltaTime;
        _rigidbody.velocity = new Vector3(_movement.x, _rigidbody.velocity.y, _movement.z);
    }

    #region Get/Set

    public float GetSpeed { get => _speed; }
    public float SetSpeed { set => _speed = value; }

    public float GetRotationSpeed { get => _rotationSpeed; }
    public float SetRotationSpeed { set => _rotationSpeed = value; }

    public Rigidbody GetRigidbody { get => _rigidbody; }
    public Rigidbody SetRigidbody { set => _rigidbody = value; }

    #endregion Get/Set

    #endregion Methods

}
