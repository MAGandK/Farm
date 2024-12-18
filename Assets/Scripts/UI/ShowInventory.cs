using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowInventory : MonoBehaviour
{
    [SerializeField] private GameObject _imageInventory;

    private bool _isShow;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowInventoryWindow();
        }
    }

    private void ShowInventoryWindow()
    {
        _isShow = !_isShow;
       _imageInventory.SetActive(_isShow);
    }
}
