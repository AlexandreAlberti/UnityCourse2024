using System;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;

    [SerializeField] private SoundsSO sounds;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
        CuttingCounter.OnAnyCut += OnCut;
        TrashCounter.OnAnyTrash += OnTrash;
        Player.OnPlayerPick += OnPick;
        ClearCounter.OnPlayerDrop += OnDrop;
        CuttingCounter.OnPlayerDrop += OnDrop;
    }

    private void OnDrop(object sender, SoundPositionEventArgs soundPosition) {
        PlaySound(sounds.objectDrop[UnityEngine.Random.Range(0, sounds.objectDrop.Length)], soundPosition.position);
    }

    private void OnPick(object sender, SoundPositionEventArgs soundPosition) {
        PlaySound(sounds.objectPick[UnityEngine.Random.Range(0, sounds.objectPick.Length)], soundPosition.position);
    }

    private void OnTrash(object sender, SoundPositionEventArgs soundPosition) {
        PlaySound(sounds.trash[UnityEngine.Random.Range(0, sounds.trash.Length)], soundPosition.position);
    }

    private void OnCut(object sender, SoundPositionEventArgs soundPosition) {
        PlaySound(sounds.chopping[UnityEngine.Random.Range(0, sounds.chopping.Length)], soundPosition.position);
    }

    private void OnRecipeFailed(object sender, SoundPositionEventArgs soundPosition) {
        PlaySound(sounds.deliveryFailed[UnityEngine.Random.Range(0, sounds.deliveryFailed.Length)], soundPosition.position);
    }

    private void OnRecipeSuccess(object sender, SoundPositionEventArgs soundPosition) {
        PlaySound(sounds.deliverySuccess[UnityEngine.Random.Range(0, sounds.deliverySuccess.Length)], soundPosition.position);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1.0f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayFootstepSound(Vector3 position) {
        PlaySound(sounds.footsteps[UnityEngine.Random.Range(0, sounds.footsteps.Length)], position);
    }
}
