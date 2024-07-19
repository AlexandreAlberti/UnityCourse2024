using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private float footstepTimerMax;
    
    private float footstepTimer;

    private void Awake() {
        footstepTimer = 0.0f;
    }

    private void Update() {
        if (!player.IsWalking())
        {
            return;
        }
        footstepTimer += Time.deltaTime;
        if (footstepTimer > footstepTimerMax) {
            footstepTimer -= footstepTimerMax;
            SoundsManager.Instance.PlayFootstepSound(transform.position);
        }
    }
}