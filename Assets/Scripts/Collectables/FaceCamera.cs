using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    #region Fields

    private Camera mainCamera;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
            mainCamera = FindFirstObjectByType<Camera>();
    }

    private void FixedUpdate()
    {
        if (mainCamera != null)
        {
            // Make the canvas face the camera
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }

    #endregion Mono

}
