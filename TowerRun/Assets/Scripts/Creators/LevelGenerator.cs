using PathCreation;

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private TowerCreator _towerCreator;
    [SerializeField] private BusterCreator _busterCreator;
    [SerializeField] private ObstacleCreator _obstacleCreator;
    [SerializeField] private float distanceToBusterPerHuman;
    [SerializeField] private int _objectsCount;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float distanceBetweenObjects = _pathCreator.path.length / _objectsCount;
        float distanceTravelled = 0;
        int countInPlayerTower = 0;
        Vector3 spawnPoint;

        for (int i = 0; i < _objectsCount; i++)
        {
            distanceTravelled += distanceBetweenObjects;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

            int random = Random.Range(0, countInPlayerTower);

            if (random == 0) 
            {
                var tower = _towerCreator.Create();
                tower.transform.position = spawnPoint;

                countInPlayerTower += tower.Count;

                if (tower.Count > 1)
                {
                    var buster = _busterCreator.Create(tower);
                    var distanceToBuster = distanceTravelled - distanceToBusterPerHuman * tower.Count;
                    buster.transform.position =
                        _pathCreator.path.GetPointAtDistance(distanceToBuster, EndOfPathInstruction.Stop);
                }
            }
            else
            {
                var obstacle = _obstacleCreator.Create(countInPlayerTower);
                obstacle.transform.position = spawnPoint;

                countInPlayerTower -= obstacle.Height;
            }
        }
    }
}
