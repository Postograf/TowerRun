using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;

    private Animator _animator;

    public static float Size { get; set; }

    public Transform FixationPoint => _fixationPoint;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(Size == 0)
            Size = _fixationPoint.position.CheckDistanceY(transform.position);
    }

    public void Run()
    {
        _animator.SetBool("isRunning", true);
    }

    public void Stop()
    {
        _animator.SetBool("isRunning", false);
    }
}
