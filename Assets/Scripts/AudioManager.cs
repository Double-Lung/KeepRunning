using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public Sound[] sounds;
    private Coroutine bgmCrossfadeRoutine;
    private float bgmMaxVolume = 0.5f;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            return;
        }
        s.source.Play();
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void FadeOut(string name, float time, bool stop=true) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(AuidoLinearFade(time,s.source, stop));
    }

    IEnumerator AuidoLinearFade(float time, AudioSource source, bool stop) {
        float amount = source.volume;
        while (source.volume > 0) {
            source.volume -= amount * Time.deltaTime / time;
            yield return null;
        }
        if (stop) {
            source.Stop();
        }
        source.volume = amount;
    }

    public void FadeIn(string name, float time) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(FadeInRoutine(time, s.source));
    }

    IEnumerator FadeInRoutine(float time, AudioSource s) {
        s.volume = 0;
        s.Play();
        while (s.volume < 1) {
            s.volume += Time.deltaTime / time;
            yield return null;
        }
    }
    public void CrossFade(string name1, string name2, float time, bool stop) {
        Sound s1 = Array.Find(sounds, sound => sound.name == name1);
        Sound s2 = Array.Find(sounds, sound => sound.name == name2);
        if (bgmCrossfadeRoutine != null) {
            StopCoroutine(bgmCrossfadeRoutine);
        }
        bgmCrossfadeRoutine = StartCoroutine(CrossFadeRoutine(s1.source,s2.source, time, stop));
    }

    // s1 fade in, s2 fade out
    IEnumerator CrossFadeRoutine(AudioSource s1, AudioSource s2, float time, bool stop) {
        float fadeOutVolume = s2.volume;
        s1.volume = 0;
        if (!s1.isPlaying) {
            s1.Play();
        }

        while (s1.volume < bgmMaxVolume || s2.volume > 0) {
            s1.volume += Time.deltaTime* bgmMaxVolume / time;
            s2.volume -= fadeOutVolume * Time.deltaTime / time;
            yield return null;
        }

        if (stop) {
            s2.Stop();
        } else {
            s2.Pause();
        }
    }

    [Serializable]
    public class Sound {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
        public bool loop = false;
        public bool playOnAwake = false;

        [HideInInspector]
        public AudioSource source;
    }
}
