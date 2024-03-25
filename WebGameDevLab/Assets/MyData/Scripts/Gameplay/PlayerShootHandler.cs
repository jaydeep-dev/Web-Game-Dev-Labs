using UnityEngine;

public class PlayerShootHandler : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    private PlayerInputController playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputController>();
    }

    private void OnEnable()
    {
        playerInput.OnFirePerformed += OnFirePerformed;
    }

    private void OnDisable()
    {
        playerInput.OnFirePerformed -= OnFirePerformed;
    }

    private void OnFirePerformed()
    {
        Debug.Log("Shoot");
        var bullet = ProjectileManager.Instance.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        var rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * 100f, ForceMode.Impulse);
    }
}
