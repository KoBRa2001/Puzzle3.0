using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;

    public static AudioManager Instance => _instance;
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private List<AudioSource> _sources = new List<AudioSource>();
    [SerializeField] private AudioSource _asPrefab;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }

    public void PlayAudio(AudioIndexes index)
    {
        var freeSource = GetFreeAudioSource();
        freeSource.clip = _clips[(int)index];
        freeSource.Play();

        //_sources[(int)index].isPlaying
    }

    public AudioSource GetFreeAudioSource()
    {
        if (_sources.Count > 0)
        {
            foreach (var s in _sources)
            {
                if (!s.isPlaying)
                {
                    return s;
                }
            }
        }        

        return CreateNewSource();
    }

    public AudioSource CreateNewSource()
    {
        AudioSource newSource = Instantiate(_asPrefab, transform);
        newSource.playOnAwake = false;
        //AudioSource newSourse = new AudioSource();
        //Instantiate(newSourse, transform);
        _sources.Add(newSource);

        return newSource;
    }
}
