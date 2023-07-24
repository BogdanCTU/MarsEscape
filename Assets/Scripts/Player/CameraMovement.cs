using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Fields

    public static CameraMovement instance;

    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;   // The max distance between Camera and desired GameObject
    [SerializeField] private float speed;

    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _endPos;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    [SerializeField] private bool isShaking = false;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            _startPos = transform.position;
            _endPos = _player.position + offset;

            this.transform.position = Vector3.SmoothDamp(_startPos, _endPos, ref velocity, damping, 2.0f, Time.deltaTime * speed);
            //this.transform.LookAt(_player.transform.position);
        }
    }

    #endregion Mono

    #region Methods

    // Resets camera position to Player Pos + Offset
    public void ResetCamera()
    {
        transform.position = _player.transform.position + offset;
    }

    // Method to shake the camera
    public void ShakeCamera()
    {
        originalPosition = transform.localPosition;

        if (!isShaking)
        {
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        isShaking = false;
    }

    #endregion

}