using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    [Header("RESOURCES")]
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip Atmosph;
    [SerializeField] AudioSource audioSource;

    public void PlayHover() => audioSource.PlayOneShot(hoverSound);

    public void PlayClick() => audioSource.PlayOneShot(clickSound);

}
