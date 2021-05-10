using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class TowerCreator : MonoBehaviour
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private Human[] _humanPrefabs;
    [SerializeField] private Vector2Int _humanInTowerRange;

    public Vector2Int Range => _humanInTowerRange;

    public Tower Create()
    {
        var tower = Instantiate(_towerPrefab, Vector3.zero, Quaternion.identity);

        for(int i = 0; i < Random.Range(_humanInTowerRange.x, _humanInTowerRange.y); i++)
        {
            var humanPrefab = _humanPrefabs[Random.Range(0, _humanPrefabs.Length)];
            var human = Instantiate(humanPrefab, Vector3.zero, Quaternion.identity);

            human.StartRandomAnimation();

            tower.PushHuman(human);
        }

        return tower;
    }
}
