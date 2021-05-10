using PathCreation;

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private TowerCreator _towerCreator;
    [SerializeField] private BusterCreator _busterCreator;
    [SerializeField] private float _distanceToBusterPerHuman;
    [SerializeField] private ObstacleCreator _obstacleCreator;
    [SerializeField] private float _offsetToLookPoint = 0.1f;
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
        Vector3 lookPoint;

        for (int i = 0; i < _objectsCount; i++)
        {
            distanceTravelled += distanceBetweenObjects;

            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

            lookPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled + _offsetToLookPoint, EndOfPathInstruction.Stop);

            int random = Random.Range(0, countInPlayerTower);

            if (random == 0) 
            {
                var tower = _towerCreator.Create();
                tower.transform.position = spawnPoint;

                tower.transform.LookAt(lookPoint);

                countInPlayerTower += tower.Count;

                if (tower.Count > 1)
                {
                    var buster = _busterCreator.Create(tower);
                    var distanceToBuster = distanceTravelled - _distanceToBusterPerHuman * tower.Count;
                    buster.transform.position =
                        _pathCreator.path.GetPointAtDistance(distanceToBuster, EndOfPathInstruction.Stop);
                }
            }
            else
            {
                var obstacle = _obstacleCreator.Create(countInPlayerTower);
                obstacle.transform.position = spawnPoint;

                obstacle.transform.LookAt(lookPoint);

                countInPlayerTower -= obstacle.Height;
            }
        }
    }
}
