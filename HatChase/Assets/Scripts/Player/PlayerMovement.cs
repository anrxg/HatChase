using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCR playerInput;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform mainCamera;
    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float jumpHeight = .5f;
    [SerializeField] private float gravityValue = -9.81f;

    void Awake()
    {
        playerInput = new PlayerCR();
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        playerInput.Player.Jump.performed += ctx => Jump();
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 moveInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = (mainCamera.forward * moveInput.y + mainCamera.right * moveInput.x);
        move.y = 0f;
        move = Vector3.ClampMagnitude(move, 1f); // prevents faster diagonal movement

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);

        if (moveInput != Vector2.zero)
        {

        }
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
    }
}
