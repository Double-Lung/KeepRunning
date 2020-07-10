using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public Animator crossfade;
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private Coroutine AudioFadeInstance;
    public bool started;
    // Start is called before the first frame update
    private void Awake() {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
        started = false;
        videoPlayer.loopPointReached += EndReached;
    }

    public void StartPlayback() {
        videoPlayer.Play();
        AudioFadeIn();
        crossfade.SetTrigger("playback");
        started = true;
    }

    void AudioFadeIn() {
        AudioFadeInstance = StartCoroutine(FadeInRoutine(1f, audioSource));
    }

    IEnumerator FadeInRoutine(float time, AudioSource s) {
        s.volume = 0;
        while (s.volume < 1) {
            s.volume += Time.deltaTime / time;
            yield return null;
        }
    }

    void EndReached(VideoPlayer vp) {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu() {
        crossfade.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    public void SkipVideo() {
        if (AudioFadeInstance != null) {
            StopCoroutine(AudioFadeInstance);
        }
        StartCoroutine(SkipRoutine());
    }

    IEnumerator SkipRoutine() {
        videoPlayer.loopPointReached -= EndReached;
        crossfade.SetTrigger("start");
        AudioFadeOut(1);
        yield return new WaitForSeconds(1.1f);
        videoPlayer.Stop();
        SceneManager.LoadScene(0);
    }

    void AudioFadeOut(float time) {
        StartCoroutine(AuidoLinearFade(time, audioSource));
    }

    IEnumerator AuidoLinearFade(float time, AudioSource source) {
        float amount = source.volume;
        while (source.volume > 0) {
            source.volume -= amount * Time.deltaTime / time;
            yield return null;
        }
        source.Stop();
    }
}
