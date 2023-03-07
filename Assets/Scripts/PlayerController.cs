using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 input;
    private Vector2 moveDirection;
    private Vector3 newPosition;
    private BoxCollider2D playerCollider;

    private InputAction move;
    private InputAction interact;

    private Animator animator;
    private PlayerInputActions playerControls;
    private LayerMask solidObjectsLayer;
    private LayerMask interactableLayer;

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        interact = playerControls.Player.Interact;
        interact.performed += ctx => PressedTest();
        interact.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        interact.performed -= ctx => PressedTest();
        interact.Disable();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControls = new PlayerInputActions();
        playerCollider = GetComponent<BoxCollider2D>();
        newPosition = transform.position;

        solidObjectsLayer = LayerMask.GetMask("SolidObjects");
        interactableLayer = LayerMask.GetMask("Interactable");
    }

    public void HandleUpdate()
    {
        input = move.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            moveDirection = input.normalized;
            float distance = moveSpeed * Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, distance, solidObjectsLayer | interactableLayer);

            // If there is a collision, adjust the distance of the movement vector
            if (hit.collider != null)
            {
                distance = hit.distance;
                moveDirection = hit.normal;
            }

            newPosition = transform.position + new Vector3(moveDirection.x, moveDirection.y, 0) * distance;
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
        }
        transform.position = newPosition;
        animator.SetBool("isMoving", input.magnitude > 0);
    }

    public void PressedTest()
    {
        //Vector3 inFront = transform.position + new Vector3(moveDirection.x, moveDirection.y, 0);
        //Debug.DrawLine(transform.position, inFront, Color.red, 10f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 1f, interactableLayer);

        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
        else
        {
            Debug.Log("Nothing to interact with");
        }
    }
}
