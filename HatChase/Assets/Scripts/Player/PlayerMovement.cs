using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    float movementSpeed = 3f;
    float rotationSpeed = 10f;
    Vector3 moveDirection;
    public Transform camTransform;
    Rigidbody playerRb;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRb = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
    }

    public void HandleAllMovements()
    {
        HandleMovement();
        HandleRotation();
    }

    #region Movements

    void HandleMovement()
    {
        moveDirection = camTransform.forward * inputManager.verticalInput;
        moveDirection += camTransform.right * inputManager.horizontalInput;
        moveDirection.y = 0f; 
        moveDirection.Normalize();
        moveDirection *= movementSpeed;

        playerRb.linearVelocity = moveDirection;
    }

    void HandleRotation()
    {
        Vector3 targetDirection = camTransform.forward * inputManager.verticalInput;
        targetDirection += camTransform.right * inputManager.horizontalInput;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
    }

    #endregion
}
