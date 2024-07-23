using System;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;

    [SerializeField] private SoundsSO sounds;
    [SerializeField] private AudioSource music;

    public static string MUSIC_VOLUME = "Music_Volume";
    public static string SFX_VOLUME = "Sfx_Volume";

    private float sfxVolume;

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

        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME, 0.75f);
        music.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 1.0f);
    }

    public void ChangeMusicVolume(float newMusicVolume) {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, newMusicVolume);
        music.volume = newMusicVolume;
    }
    public void ChangeSFXVolume(float newSfxVolume) {
        sfxVolume = newSfxVolume;
        PlayerPrefs.SetFloat(SFX_VOLUME, sfxVolume);
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

    private void PlaySound(AudioClip clip, Vector3 position) {
        AudioSource.PlayClipAtPoint(clip, position, sfxVolume);
    }

    public void PlayFootstepSound(Vector3 position) {
        PlaySound(sounds.footsteps[UnityEngine.Random.Range(0, sounds.footsteps.Length)], position);
    }
}
