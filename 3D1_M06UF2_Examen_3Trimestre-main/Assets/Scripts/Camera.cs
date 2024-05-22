using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{   

    public Transform playerTransform;
    public Vector3 offset;
    public Vector2 minCameraPosition;
    public Vector2 maxCameraPosition;


    void Update()
    {
        if(playerTransform !=null){
            Vector3 desiredPosition = playerTransform.position + offset;

            float clampX = Mathf.Clamp(desiredPosition.x, minCameraPosition.x, maxCameraPosition.x);
            float clampY = Mathf.Clamp(desiredPosition.y, minCameraPosition.y, maxCameraPosition.y);

            Vector3 clampedPosition = new Vector3(clampX, clampY, desiredPosition.z);

            transform.position = clampedPosition;
        }        
    }
}
