using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundsSO : ScriptableObject
{
    public AudioClip[] deliverySuccess; //
    public AudioClip[] deliveryFailed; //
    public AudioClip[] chopping; //
    public AudioClip[] footsteps;
    public AudioClip[] objectDrop; //
    public AudioClip[] objectPick; //
    public AudioClip stoveSizzle;
    public AudioClip[] trash; //
    public AudioClip[] warning;
}
