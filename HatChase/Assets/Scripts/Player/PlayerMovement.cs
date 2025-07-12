using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    private PlayerCR playerInput;
    private Animator animator;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
   private Transform mainCamera;

    [SerializeField] private float playerSpeed = 3.0f;
    private float originalSpeed;

    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    void Awake()
    {
        
        playerInput = new PlayerCR();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        originalSpeed = playerSpeed;
    }

    protected new void OnEnable()
    {
        playerInput.Player.Jump.performed += ctx => Jump();
        playerInput.Enable();
    }

    protected new void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        Movement();
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
    }

    private void Movement()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            coyoteTimeCounter = coyoteTime; // reset coyote timer if grounded
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // count down otherwise
        }

        Vector2 moveInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = (mainCamera.forward * moveInput.y + mainCamera.right * moveInput.x);
        move.y = 0f;
        move = Vector3.ClampMagnitude(move, 1f);

        // Animation
        animator.SetBool("isRunning", move.magnitude > 0.1f);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Final movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Bullet"))
        {
            Destroy(hit.gameObject);
            SlowDownTemporarily();
        }
    }

    private void SlowDownTemporarily()
    {
        playerSpeed = originalSpeed / 2f;
        Invoke(nameof(RestoreSpeed), 2f);
    }

    private void RestoreSpeed()
    {
        playerSpeed = originalSpeed;
    }
}
