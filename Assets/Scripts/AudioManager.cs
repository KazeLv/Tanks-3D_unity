using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get { return _instance; }
    }

    public AudioSource audioSource;

    // Use this for initialization
    void Start () {
        _instance = this;
	}
	
    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
