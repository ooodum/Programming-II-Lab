using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] Animator playerAnimator;

    [SerializeField] float jumpHeight;
    [SerializeField] float walkSpeed;

    PlayerInput inputAction;

    Vector2 direction;
    Vector2 lookVector;
    Vector3 cameraRotation;

    private float distanceToGround;
    private bool isGrounded = true;
    private bool isWalking = false;

    private float coughArea;

    private void Awake() {
        inputAction = new PlayerInput();

        inputAction.Player.Move.performed += cntxt => direction = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => direction = Vector2.zero;

        inputAction.Player.Look.performed += cntxt => lookVector = cntxt.ReadValue<Vector2>();
        inputAction.Player.Look.canceled += cntxt => lookVector = Vector2.zero;

        inputAction.Player.Jump.performed += cntxt => Jump();

        inputAction.Player.Cough.performed += cntxt => Cough();

        cameraRotation = transform.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable() {
        inputAction.Player.Enable();
    }
    private void Update() {
        cameraRotation = new Vector3(cameraRotation.x + lookVector.y, cameraRotation.y + lookVector.x, 0);
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);

        transform.Translate(Vector3.forward * direction.y * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(Vector3.right * direction.x * Time.deltaTime * walkSpeed, Space.Self);
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

    }

}
