using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{
    public static AudioManager instance;
    [Header("------ Audio Sourses ------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SfxSource;

    [Header("------ Audio Clips ------")]
    [SerializeField] public AudioClip background;
    [SerializeField] public AudioClip shoot;
    [SerializeField] public AudioClip enemyDeath;
    [SerializeField] public AudioClip collidingEnemysHead;
    [SerializeField] public AudioClip collidingPlatform;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public void PlayBackGroundMusic() {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sfx) {
        SfxSource.PlayOneShot(sfx);
    }

    public void Muted() {
        musicSource.Pause();
    }

    public void UnMuted() {
        musicSource.Play();
    }
}
