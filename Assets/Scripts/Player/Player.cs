using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KitchenObjectParentAbstract {

    public static Player Instance { get; private set; }

    private const float INTERACT_DISTANCE = 2.0f;

    public static event EventHandler<SoundPositionEventArgs> OnPlayerPick;
    public event EventHandler OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs {
        public KitchenObjectParentAbstract selectedKitchenParent;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameInput input;
    [SerializeField] private LayerMask countersLayer;

    private bool isWalking;
    private Vector3 lastInteractionDir;
    private KitchenObjectParentAbstract selectedClearCounter;

    private void Awake() {
        if (Instance) {
            Debug.LogError("Something went wrong, 2 players in scene");
        }

        Instance = this;
    }

    private void Start() {
        input.OnInteractAction += OnInteractAction;
        input.OnInteractAlternateAction += OnInteractAlternateAction;
    }

    private void Update() {
        // if (!GameManager.Instance.IsGamePlaying()) { return; }
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
            if (raycastHit.transform.TryGetComponent(out KitchenObjectParentAbstract kitchenObjectParent)) {
                if (kitchenObjectParent != selectedClearCounter) {
                    SetSelectedCounter(kitchenObjectParent);
                    OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs {
                        selectedKitchenParent = selectedClearCounter
                    });
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }

    }
    private void SetSelectedCounter(KitchenObjectParentAbstract clearCounter) {
        selectedClearCounter = clearCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs {
            selectedKitchenParent = selectedClearCounter
        });
    }

    private void OnInteractAction(object sender, EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) { return; }
        if (selectedClearCounter) {
            selectedClearCounter.Interact(this);
        }
    }

    private void OnInteractAlternateAction(object sender, EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) { return; }
        if (selectedClearCounter) {
            selectedClearCounter.InteractAlternate(this);
        }
    }

    public override void SetKichenObject(KitchenObject kitchenObject) {
        base.SetKichenObject(kitchenObject);
        if (kitchenObject != null) {
            OnPlayerPick?.Invoke(this, new SoundPositionEventArgs {
                position = transform.position
            });
        }
    }
}