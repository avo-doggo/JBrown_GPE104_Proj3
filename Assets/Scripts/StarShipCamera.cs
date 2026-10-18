using UnityEngine;
using UnityEngine.InputSystem;
public class StarshipCamera : MonoBehaviour
{
    public Key increaseDistanceKey;
    public Key decreaseDistanceKey;
    [Header("Ship")]
    public Transform starship;

    [Header("Camera Offset")]
    // Direction the camera sits from the ship.
    // This is set by the designer in the Inspector.
    public Vector3 offsetDirection;

    // How far away the camera can be from the ship.
    public float offsetMagnitude;

    // Designer-exposed limits.
    public float minimumOffset;
    public float maximumOffset;

    [Header("Camera Look")]
    // How far in front of the ship the camera looks.
    public Vector3 lookOffset;

    [Header("Smoothing")]
    public float followSpeed;
    public float lookSpeed;

    void Update()
    {
        // O moves the camera farther away.
        if (Keyboard.current[increaseDistanceKey].isPressed)
        {
            offsetMagnitude += 5f * Time.deltaTime;
        }

        // L moves the camera closer.
        if (Keyboard.current[decreaseDistanceKey].isPressed)
        {
            offsetMagnitude -= 5f * Time.deltaTime;
        }

        // Keep the offset within the limits.
        offsetMagnitude = Mathf.Clamp(
            offsetMagnitude,
            minimumOffset,
            maximumOffset
        );
    }

    void LateUpdate()
    {
        if (starship == null)
        {
            return;
        }

        Vector3 direction = starship.TransformDirection(offsetDirection.normalized);

        Vector3 targetPosition =
            starship.position + direction * offsetMagnitude;

        // Smoothly move the camera.
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        // Calculate the point in front of the ship
        // that the camera should look toward.
        Vector3 lookPoint =
            starship.position + starship.TransformDirection(lookOffset);

        // Find the direction from the camera to the look point.
        Vector3 lookDirection = lookPoint - transform.position;

        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(lookDirection);

            // Smoothly rotate the camera.
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                lookSpeed * Time.deltaTime
            );
        }
    }
}