using System;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObjectArray = null;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
    }
    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == _baseCounter)
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
        foreach (var visualObject in _visualGameObjectArray)
        {
            visualObject.gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visualObject in _visualGameObjectArray)
        {
            visualObject.gameObject.SetActive(false);
        }
    }
}