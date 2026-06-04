using UnityEngine;

public class SoundPlayerScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip moveSound;

    [SerializeField]
    private AudioClip cannotMoveSound;

    [SerializeField]
    private AudioClip resourcesCollectedSound;

    [SerializeField]
    private AudioClip levelFinishedSound;

    private void OnEnable()
    {
        PlayerMovementScript.OnMoved += PlayMoveSound;
        PlayerMovementScript.OnCannotMove += PlayCannotMoveSound;
        PlayerResourceScript.OnResourcesCollected += PlayResourcesCollectedSound;
        LevelManager.OnLevelFinished += PlayLevelFinishedSound;
    }

    private void OnDisable()
    {
        PlayerMovementScript.OnMoved -= PlayMoveSound;
        PlayerMovementScript.OnCannotMove -= PlayCannotMoveSound;
        PlayerResourceScript.OnResourcesCollected -= PlayResourcesCollectedSound;
        LevelManager.OnLevelFinished -= PlayLevelFinishedSound;
    }

    private void PlayMoveSound(Transform position)
    {
        SoundFXManager.Instance.PlayPitchedSoundFXClip(moveSound, position);
    }

    private void PlayCannotMoveSound(Transform position)
    {
        SoundFXManager.Instance.PlayPitchedSoundFXClip(cannotMoveSound, position);
    }

    private void PlayResourcesCollectedSound(Transform position)
    {
        SoundFXManager.Instance.PlayPitchedSoundFXClip(resourcesCollectedSound, position);
    }

    private void PlayLevelFinishedSound(Transform position)
    {
        SoundFXManager.Instance.PlayPitchedSoundFXClip(levelFinishedSound, position);
    }
}
