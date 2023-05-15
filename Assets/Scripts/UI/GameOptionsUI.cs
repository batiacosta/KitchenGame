using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour
{

    public static GameOptionsUI Instance { get; private set; }
    
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI moveUpButtonText;
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactAltButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;
    [SerializeField] private TextMeshProUGUI soundEffectsButtonText;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Transform rebindingScreenTransform;
    
    
    

    private void Awake()
    {
        Instance = this;
        
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        { 
            UpdateVisual();
            Hide();
        });
        moveUpButton.onClick.AddListener(() =>  { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() =>  { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() =>  { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() =>  { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() =>  { RebindBinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() =>  { RebindBinding(GameInput.Binding.Interact_Alternate); });
        pauseButton.onClick.AddListener(() =>  { RebindBinding(GameInput.Binding.Pause); });
        
        
        
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        var soundEffectsVolumeValue = Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        soundEffectsButtonText.text = $"Sound Effects: {soundEffectsVolumeValue}";
        var musicVolumeValue = Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        musicButtonText.text = $"Music: {musicVolumeValue}";

        moveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    private void ShowPressToRebindKey()
    {
        rebindingScreenTransform.gameObject.SetActive(true);
    }
    
    private void HidePressToRebindKey()
    {
        rebindingScreenTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
    
}
