using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceCheker;
    [SerializeField] private float _fixationMaxDistantion;

    public event UnityAction<int> TowerChanged;

    private List<Human> _humans = new List<Human>();
    private HashSet<Obstacle> _collisionedObstacles = new HashSet<Obstacle>();

    private void Start()
    {
        var human = Instantiate(_startHuman, Vector3.zero, Quaternion.identity);
        AddHumans(human);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Human human))
        {
            var tower = human.GetComponentInParent<Tower>();
            var takedHumans = tower.TakeHumans(_distanceCheker, _fixationMaxDistantion);

            if(takedHumans.Length > 0)
            {
                AddHumans(takedHumans);
                tower.Break();
            }
        }
        else if (other.TryGetComponent(out Block block))
        {
            var obstacle = block.GetComponentInParent<Obstacle>();

            if (_collisionedObstacles.Add(obstacle))
            {
                var collisionedCount = obstacle.GetCollisionedCount(_distanceCheker);
                RemoveHumans(collisionedCount);
            }
        }
    }

    private void AddHumans(params Human[] humans)
    {
        if(_humans.Count > 0)
            _humans[0].Stop();

        transform.position = humans[0].transform.position;

        foreach (var human in humans)
            SetHumanPosition(human);

        var previousHuman = humans[humans.Length - 1];
        foreach (var towerHuman in _humans)
        {
            towerHuman.transform.position = previousHuman.FixationPoint.position;
            previousHuman = towerHuman;
        }

        _humans.InsertRange(0, humans);
        _humans[0].Run();

        TowerChanged?.Invoke(_humans.Count);
    }

    private void RemoveHumans(int count)
    {
        foreach (var collisionedHuman in _humans.Take(count))
            Destroy(collisionedHuman.gameObject);

        _humans.RemoveRange(0, count);

        _humans.ForEach(x => x.transform.parent = null);

        transform.position = _humans[0].transform.position;

        _humans.ForEach(x => x.transform.parent = transform);

        _humans[0].Run();

        TowerChanged?.Invoke(_humans.Count);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }
}
