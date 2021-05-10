using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Vector3 _scatterForce;

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
        ScatterHumans(_humans);

        Destroy(gameObject);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = Vector3.zero;
        human.transform.localRotation = Quaternion.identity;
    }

    private void ScatterHumans(IEnumerable<Human> humans)
    {
        foreach (var human in humans)
        {
            human.transform.parent = null;

            human.GetComponent<Collider>().isTrigger = true;
            var rigidbody = human.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;

            var multiplierRight = Mathf.Pow(-1, Random.Range(0, 2));
            var forceRight = human.transform.right * multiplierRight;

            var multiplierForward = -1 * Random.Range(-1, 2);
            var forceForward = human.transform.forward * multiplierForward;

            var force = forceRight + forceForward + _scatterForce;

            rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
