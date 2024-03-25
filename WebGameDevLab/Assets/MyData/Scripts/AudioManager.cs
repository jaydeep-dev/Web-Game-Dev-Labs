using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private List<AudioData> audioClipsList;

    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(string sfxName)
    {
        var audioData = audioClipsList.Find(x => x.name == sfxName);
        var clip = audioData.clips[Random.Range(0, audioData.clips.Length)];
        audioSource.PlayOneShot(clip);
    }
}

[System.Serializable]
class AudioData
{
    public string name;
    public AudioClip[] clips;
}