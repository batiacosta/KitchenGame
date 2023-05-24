using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [FormerlySerializedAs("audioClipSO")] [SerializeField] private AudioClipSO audioClipSo;

    private float _volume = 1f;
    private const string PlayerPrefsSoundEffectsVolume = "SoundEffectsVolume";

    private void Awake()
    {
        Instance = this;
        _volume = PlayerPrefs.GetFloat(PlayerPrefsSoundEffectsVolume);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSucceed += DeliveryManager_OnRecipeSucceed;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPLacedHere += BaseCounter_OnAnyObjectPLaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipSo.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPLaced(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipSo.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound( audioClipSo.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipSo.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        var position = DeliveryCounter.Instance.transform.position;
        PlaySound(audioClipSo.deliveryPail, position);
    }

    private void DeliveryManager_OnRecipeSucceed(object sender, EventArgs e)
    {
        var position = DeliveryCounter.Instance.transform.position;
        PlaySound(audioClipSo.deliverySuccess, position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * _volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipSo.footsteps, position, volume);
    }
    public void PlayCountdownSound()
    {
        PlaySound(audioClipSo.warning, Vector3.zero);
    }

    public void ChangeVolume()
    {
        _volume += 0.1f;
        if (_volume > 1f)
        {
            _volume = 0;
        }
        PlayerPrefs.SetFloat(PlayerPrefsSoundEffectsVolume, _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return _volume;
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipSo.warning, position);
    }
}
