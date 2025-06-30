using UnityEngine;
using UnityEngine.InputSystem;

public class FireController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator playerAnimator;

    private PlayerCR playerInput;

    void Awake()
    {
        playerInput = new PlayerCR();
    }

    void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Fire.performed += ctx => Fire();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.Launch(playerTransform.forward);
        }
    }
}
