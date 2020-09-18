using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoise;
    public AudioSource backgroundMuisic;

    private void Start() {
        if (PlayerPrefs.HasKey("Sound")) {
            if (PlayerPrefs.GetInt("Sound") == 0) {
                backgroundMuisic.Play();
                backgroundMuisic.volume = 0;
            } else {
                backgroundMuisic.Play();
                backgroundMuisic.volume = 1;
            }
        } else {
            backgroundMuisic.Play();
            backgroundMuisic.volume = 1;
        }
    }

    public void adjustVolume() {
        if (PlayerPrefs.HasKey("Sound")) {
            if (PlayerPrefs.GetInt("Sound") == 0) {
                backgroundMuisic.volume = 0;
            } else {
                backgroundMuisic.volume = 1;
            }
        }
    }

    public void PlayRandomDestroyNoise() {
        if (PlayerPrefs.HasKey("Sound")) {
            if (PlayerPrefs.GetInt("Sound") == 1) {
            int clopToPlay = Random.Range(0, destroyNoise.Length);
            destroyNoise[clopToPlay].Play();
            }
        } else {
            int clopToPlay = Random.Range(0, destroyNoise.Length);
            destroyNoise[clopToPlay].Play();
        }
    }
}
