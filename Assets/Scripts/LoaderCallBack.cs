using System;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;


public class LoaderCallBack : MonoBehaviour
{
    private bool _isFirstUpdatel = true;

    private void Update()
    {
        if (_isFirstUpdatel)
        {
            _isFirstUpdatel = false;
            Loader.LoaderCallback();
        }
    }
}
