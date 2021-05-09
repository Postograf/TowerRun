using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<Human> _humans = new List<Human>();

    public int Count => _humans.Count;

    public Human[] TakeHumans(Transform distanceCheker, float fixationMaxDistance)
    {
        for (int i = 0; i < _humans.Count; i++)
        {
            float distance = distanceCheker.position.CheckDistanceY(_humans[i].FixationPoint.position);

            if (distance < fixationMaxDistance)
            {
                var takedHumans = _humans.Take(i + 1).ToArray();
                _humans.RemoveRange(0, i + 1);
                return takedHumans;
            }
        }

        return new Human[0];
    }

    public void PushHuman(Human human)
    {
        SetHumanPosition(human);

        if (_humans.Count > 0)
            human.transform.position = _humans[_humans.Count - 1].FixationPoint.position;
        
        _humans.Add(human);
    }

    public void Break()
    {
        Destroy(gameObject);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = Vector3.zero;
        human.transform.localRotation = Quaternion.identity;
    }
}
