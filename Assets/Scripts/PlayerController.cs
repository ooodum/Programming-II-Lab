using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Transform coughSpawnLocation;
    [SerializeField] GameObject coughHitbox;

    [SerializeField] float jumpHeight;
    [SerializeField] float walkSpeed;

    PlayerInput inputAction;

    Vector2 direction;
    Vector2 lookVector;
    Vector3 cameraRotation;

    private float distanceToGround;
    private bool isGrounded = true;
    private bool isWalking = false;

    private float coughArea, coughSpeed, lifetime;
    private bool canCough = true;
    [SerializeField] float timeBetweenCough;

    CharacterStats characterStats;

    public bool canBeAttacked = true;

    private void Awake() {
        inputAction = new PlayerInput();

        inputAction.Player.Move.performed += cntxt => direction = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => direction = Vector2.zero;

        inputAction.Player.Look.performed += cntxt => lookVector = cntxt.ReadValue<Vector2>();
        inputAction.Player.Look.canceled += cntxt => lookVector = Vector2.zero;

        inputAction.Player.Jump.performed += cntxt => Jump();

        inputAction.Player.Cough.performed += cntxt => Cough();

        characterStats = GetComponent<CharacterStats>();
        inputAction.Player.TakeDamage.performed += cntxt => characterStats.TakeDamage(2);

        cameraRotation = transform.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;

        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }
    private void OnEnable() {
        inputAction.Player.Enable();
    }
    private void Update() {
        cameraRotation = new Vector3(Mathf.Clamp(cameraRotation.x + lookVector.y,-40,40), cameraRotation.y + lookVector.x, 0);
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);

        transform.Translate(Vector3.forward * direction.y * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(Vector3.right * direction.x * Time.deltaTime * walkSpeed, Space.Self);
    }

    private void FixedUpdate() {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround + .05f);
    }

    private void LateUpdate() {
        cam.transform.rotation = Quaternion.Euler(cameraRotation);
    }
    private void OnDisable() {
        inputAction.Player.Disable();
    }
    private void Jump() {
        if (!isGrounded) return;
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
    private void Cough() {
        if (!canCough) return;
        canCough = false;
        GameObject coughObject = Instantiate(coughHitbox, coughSpawnLocation.position, Quaternion.identity);
        Cough cough = coughObject.GetComponent<Cough>();
        cough.bulletOwner = global::Cough.BulletOwner.Player;
        cough.StartCough(Vector3.forward, 2, 1, 2);

        Invoke(nameof(CoughTimer), timeBetweenCough);
    }

    private void CoughTimer() {
        canCough = true;
    }
}
