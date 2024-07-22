using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance;

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    private void Awake() {

        if (Instance == null) {
            Instance = this;

            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();

            playerInputActions.Player.Interact.performed += Interact;
            playerInputActions.Player.InteractAlternate.performed += InteractAlternate;
            playerInputActions.Player.Pause.performed += PauseAction;

            DontDestroyOnLoad(Instance.gameObject);
        } else {
            Destroy(this);
        }
    }

    private void PauseAction(InputAction.CallbackContext context) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact(InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate(InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();  

        inputVector.Normalize();

        return inputVector;
    }
}