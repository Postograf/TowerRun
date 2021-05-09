using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterCreator : MonoBehaviour
{
    [SerializeField] private JumpBuster _jumpBuster;

    public JumpBuster Create(Tower tower)
    {
        var buster = Instantiate(_jumpBuster, Vector3.zero, Quaternion.identity);
        buster.Count = tower.Count;
        return buster;
    }
}
