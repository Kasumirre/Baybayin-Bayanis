using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Music Clips")]
    public List<AudioClip> musicTracks; // tracks to shuffle normally
    public AudioClip finalPuzzleTrack; // track to loop in FinalPuzzle scene

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float volume = 0.5f;
    public float volumeStep = 0.05f;

    private AudioSource audioSource;
    private List<int> trackIndices;
    private int currentIndex = 0;
    private bool isFinalPuzzle = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (!audioSource.isPlaying && musicTracks.Count > 0)
        {
            ShuffleTracks();
            PlayCurrentTrack();
        }
    }

    private void Update()
    {
        // Volume controls
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            volume += volumeStep;
            volume = Mathf.Clamp01(volume);
            audioSource.volume = volume;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            volume -= volumeStep;
            volume = Mathf.Clamp01(volume);
            audioSource.volume = volume;
        }

        if (!isFinalPuzzle && !audioSource.isPlaying)
        {
            NextTrack();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FinalPuzzle" && !isFinalPuzzle)
        {
            PlayFinalPuzzleTrack();
        }
    }

    private void PlayFinalPuzzleTrack()
    {
        if (finalPuzzleTrack != null)
        {
            isFinalPuzzle = true;
            audioSource.clip = finalPuzzleTrack;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void ShuffleTracks()
    {
        trackIndices = new List<int>();
        for (int i = 0; i < musicTracks.Count; i++)
            trackIndices.Add(i);

        for (int i = trackIndices.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = trackIndices[i];
            trackIndices[i] = trackIndices[j];
            trackIndices[j] = temp;
        }

        currentIndex = 0;
    }

    void PlayCurrentTrack()
    {
        if (musicTracks.Count == 0) return;

        audioSource.clip = musicTracks[trackIndices[currentIndex]];
        audioSource.loop = false;
        audioSource.Play();
    }

    void NextTrack()
    {
        currentIndex++;
        if (currentIndex >= trackIndices.Count)
        {
            ShuffleTracks();
        }
        PlayCurrentTrack();
    }
}