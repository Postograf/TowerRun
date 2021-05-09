using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerTower _playerTower;
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Vector3 _offsetRotation;
    [SerializeField] private float _speed;

    private Vector3 _targetOffsetPosition;

    private void OnEnable()
    {
        _playerTower.TowerChanged += OnTowerChanged;
    }

    private void OnDisable()
    {
        _playerTower.TowerChanged -= OnTowerChanged;
    }

    private void Update()
    {
        UpdatePosition();
        _offsetPosition = Vector3.MoveTowards(_offsetPosition, _targetOffsetPosition, _speed * Time.deltaTime);
    }

    private void UpdatePosition()
    {
        transform.position = _playerTower.transform.position;
        transform.localPosition += _offsetPosition;

        transform.LookAt(_playerTower.transform.position + _offsetRotation);
    }

    private void OnTowerChanged(int count)
    {
        _targetOffsetPosition = _offsetPosition + new Vector3(1, 1, -1) * count / 2;
        UpdatePosition();
    }
}
