using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance = null;

    [SerializeField]
    private AudioSource soundFXObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float pitch = 1f, float volume = 1f)
    {
        // spawn in gameObject
        AudioSource audioSource = Instantiate(
            soundFXObject,
            spawnTransform.position,
            Quaternion.identity);

        // assign the audioClip
        audioSource.clip = audioClip;

        // assign volume
        audioSource.volume = volume;

        // assign pitch
        audioSource.pitch = pitch;

        // play sound
        audioSource.Play();

        // get length of sound FX clip
        var clipLength = audioSource.clip.length;

        // destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform spawnTransform, float volume = 1f)
    {
        // assign the random audioClip
        int random = Random.Range(0, audioClips.Length);

        PlaySoundFXClip(audioClips[random], spawnTransform, volume);
    }
    public void PlayPitchedSoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume = 1f)
    {
        float pitch = Random.Range(0.85f, 1.15f);
        PlaySoundFXClip(audioClip, spawnTransform, pitch, volume);
        
    }
}
