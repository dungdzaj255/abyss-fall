using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{
    public static AudioManager instance;
    [Header("------ Audio Sourses ------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SfxSource;

    [Header("------ Audio Clips ------")]
    [SerializeField] private AudioClip background;

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

    public void Muted() {
        musicSource.Pause();
    }

    public void UnMuted() {
        musicSource.Play();
    }
}
