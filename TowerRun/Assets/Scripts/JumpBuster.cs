using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBuster : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float coefficient = 0.55f;
    //private float bustedJumpForce = 1f;
    private float normalJumpForce;

    //public Transform Target { set; get; }
    public int Count { set; get; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Jumper jumper) && other.TryGetComponent(out PathFollower follower))
        {
            //float speed = follower.Speed;

            //Vector3 vectorToTarget = Target.position - transform.position;
            //float distance = vectorToTarget.x;

            //float sinOf2Alpha = (distance * Mathf.Abs(Physics.gravity.y)) / Mathf.Pow(speed, 2);
            //float alpha = Mathf.Asin(sinOf2Alpha) / 2;

            //Vector3 direction = follower.transform.forward;
            //Vector3 jumpDirection = new Vector3(
            //    direction.x,
            //    direction.y * Mathf.Cos(alpha) + direction.z * Mathf.Sin(alpha),
            //    direction.y * Mathf.Sin(alpha) + direction.z * Mathf.Cos(alpha)
            //);

            //bustedJumpForce = jumpDirection.y;

            //normalJumpForce = jumper.JumpForce;
            //jumper.JumpForce = bustedJumpForce;
            normalJumpForce = jumper.JumpForce;
            jumper.BoostJump(Count - ((Count - 1) * coefficient));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Jumper jumper))
        {
            jumper.JumpForce = normalJumpForce;
        }
    }
}
