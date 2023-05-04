using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private GameObject _hasProgressGameObject = null;
    private IHasProgress _hasProgress;

    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
        if (_hasProgress==null)
        {
            Debug.Log($"GameObject {_hasProgressGameObject.name} does not have IHasProgress");
        }
        _hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        _barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        _barImage.fillAmount = e.ProgressNormalized;
        if (e.ProgressNormalized == 0f || e.ProgressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
