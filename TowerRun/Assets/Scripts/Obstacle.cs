using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private List<Block> _blocks = new List<Block>();

    public int Height => _blocks.Count;

    public int GetCollisionedCount(Transform distanceCheker)
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            float difference = distanceCheker.position.y - _blocks[i].Top.position.y;

            if (difference < 0)
            {
                return _blocks.Count - i;
            }
        }

        return 0;
    }

    public void PushBlock(Block block)
    {
        SetBlockPosition(block);

        if (_blocks.Count > 0)
           block.transform.position = _blocks[_blocks.Count - 1].Top.position;

        _blocks.Add(block);
    }

    private void SetBlockPosition(Block block)
    {
        block.transform.parent = transform;
        block.transform.localPosition = Vector3.zero;
        block.transform.localRotation = Quaternion.identity;
    }
}
