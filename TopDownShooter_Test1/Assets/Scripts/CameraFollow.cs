using UnityEngine;

public class CameraFollow : MonoBehaviour{

    public Transform target;

    public float smoothSpeed = 20f;
    public Vector3 offset;


    void FixedUpdate(){

        Vector3 desiredPosition = target.position + offset;
        // Linear interpolation. Process og smoothly going from point A to point B
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
