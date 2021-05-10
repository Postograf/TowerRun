using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;

    private static string[] _possibleAnimations;

    private Animator _animator;

    public static float Size { get; set; }

    public Transform FixationPoint => _fixationPoint;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_possibleAnimations == null)
            _possibleAnimations = _animator.parameters
                .Select(x => x.name)
                .Where(x => x != "isRunning" && x != "isDead" && x != "isWalking")
                .ToArray();
    }

    private void Start()
    {
        if(Size == 0)
            Size = _fixationPoint.position.CheckDistanceY(transform.position);
    }

    public void Run()
    {
        StopAnimations();

        _animator.SetBool("isRunning", true);
    }

    public void StartRandomAnimation()
    {
        StopAnimations();

        var randomName = _possibleAnimations[Random.Range(0, _possibleAnimations.Length - 1)];

        _animator.SetBool(randomName, true);
    }

    public void StopAnimations()
    {
        foreach (var parameter in _animator.parameters)
            _animator.SetBool(parameter.name, false);
    }
}
