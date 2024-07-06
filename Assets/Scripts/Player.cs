using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const float INTERACT_DISTANCE = 2.0f;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameInput input;
    [SerializeField] private LayerMask countersLayer;

    private bool isWalking;
    private Vector3 lastInteractionDir;

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleMovement() {
        Vector2 inputVector = input.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    private void HandleInteractions() {
        Vector2 inputVector = input.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

        if (isWalking) {
            lastInteractionDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, INTERACT_DISTANCE, countersLayer)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)){
                clearCounter.Interact();
            }
        }


    }
}