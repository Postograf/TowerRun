using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private Block _blockPrefab;
    
    public Obstacle Create(int maxHeight)
    {
        var obstacle = Instantiate(_obstaclePrefab, Vector3.zero, Quaternion.identity);
        var height = Random.Range(1, maxHeight);

        for (int i = 0; i < maxHeight; i++)
        {
            var block = Instantiate(_blockPrefab, Vector3.zero, Quaternion.identity);
            obstacle.PushBlock(block);
        }

        return obstacle;
    }
}
