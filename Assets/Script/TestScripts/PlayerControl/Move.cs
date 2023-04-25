using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float playerWalkSpeed = 3.0f;
    [SerializeField]
    private Transform axisCamera = null;
    new Rigidbody rigidbody;
    private Vector3 vectorXZ = new Vector3(1, 0, 1);

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void SetDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            Vector3 cameraDirection = Vector3.Scale(axisCamera.forward, vectorXZ).normalized;
            Vector3 moveDirection = cameraDirection * direction.z + axisCamera.right * direction.x;
            transform.rotation = Quaternion.LookRotation(moveDirection);

            rigidbody.velocity = moveDirection * playerWalkSpeed +
                ((rigidbody.velocity.y > 0) ? 0 : rigidbody.velocity.y) * Vector3.up;
        }
    }
}
