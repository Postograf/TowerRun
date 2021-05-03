using PathCreation;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private float _distanceTraveled;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.MovePosition(_pathCreator.path.GetPointAtDistance(_distanceTraveled));
    }

    private void Update()
    {
        _distanceTraveled += Time.deltaTime * _speed;

        var nextPoint = _pathCreator.path.GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Loop);
        nextPoint.y = transform.position.y;

        _rigidbody.MovePosition(nextPoint);
        transform.LookAt(nextPoint);
    }
}
