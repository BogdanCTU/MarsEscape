using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRandomMovementMenu : MonoBehaviour
{

    public List<Transform> positions = new List<Transform>(); // List of positions to move the camera to.
    public float moveDuration = 2f; // The duration of each movement.

    private Camera mainCamera;
    private int currentPositionIndex = 0;

    void Start()
    {
        mainCamera = Camera.main;
        MoveCameraToNextPosition();
    }

    void MoveCameraToNextPosition()
    {
        // Get the next position from the list.
        Transform targetPosition = positions[currentPositionIndex];

        // Move the camera to the target position smoothly.
        StartCoroutine(MoveCameraSmoothly(targetPosition.position));

        // Increment the current position index, and loop back to the beginning if necessary.
        currentPositionIndex = (currentPositionIndex + 1) % positions.Count;
    }

    IEnumerator MoveCameraSmoothly(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = mainCamera.transform.position;

        while (elapsedTime < moveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the camera reaches the exact target position at the end of the movement.
        mainCamera.transform.position = targetPosition;

        // Move the camera to the next position.
        MoveCameraToNextPosition();
    }

}
