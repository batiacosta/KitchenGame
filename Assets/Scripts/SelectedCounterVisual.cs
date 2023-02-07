using System;
using UnityEditor.Search;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter _clearCounter;
    [SerializeField] private GameObject _visualGameObject = null;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
    }
    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == _clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        _visualGameObject.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _visualGameObject.gameObject.SetActive(false);
    }
}