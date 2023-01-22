using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] Animator playerAnimator;

    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float walkSpeed = 5f;

    PlayerInput inputAction;

    Vector2 direction;
    Vector2 lookVector;
    Vector3 cameraRotation;

    private float distanceToGround;
    private bool isGrounded;
    private bool isWalking = false;

    private float coughArea;

    private void Awake() {
        inputAction = new PlayerInput();

        inputAction.Player.Move.performed += cntxt => direction = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => direction = Vector2.zero;

        inputAction.Player.Jump.performed += cntxt => Jump();

        print("done");
    }
    private void OnEnable() {
        
    }
    private void Update() {
        
    }
    private void OnDisable() {
        
    }
    private void Jump() { 
    
    }

}
