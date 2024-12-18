using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saddle : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Vector3 _scaleDown = new Vector3(1.2f, 0.8f, 1.2f);
    [SerializeField] private Vector3 _scaleUp = new Vector3(0.8f, 1.2f, 0.8f);

    [SerializeField] private float _scaleCoef;
    [SerializeField] private float _rotationCoef;
    private void Update()
    {
        Vector3 relativePosition = _playerTransform.InverseTransformPoint(transform.position);
        float interpolation = relativePosition.y * _scaleCoef;

        Vector3 scale = Lerp3Value(_scaleDown, Vector3.one, _scaleUp, interpolation);
        _playerBody.localScale = scale;
        _playerBody.localEulerAngles = new Vector3(relativePosition.z, 0, relativePosition.x) * _rotationCoef;
        
        Debug.Log($"relativePosition: {relativePosition}, interpolation: {interpolation}, scale: {scale}, localEulerAngles: {_playerBody.localEulerAngles}");
    }

    private Vector3 Lerp3Value(Vector3 first, Vector3 second, Vector3 third, float interplant)
    {
        if (interplant < 0)
        {
            return Vector3.LerpUnclamped(first, second, interplant + 1f);
        }
        else
        {
            return Vector3.LerpUnclamped(second, third, interplant);
        }
        
        
    }
}
