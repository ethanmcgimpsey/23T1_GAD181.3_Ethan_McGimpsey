using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minFOV = 30.0f;
    public float maxFOV = 60.0f;
    public float maxDistance = 10.0f;
    public float FOVSpeed = 5.0f;
    public float cameraFollowSpeed = 5.0f;

    private Camera cam;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cameraOffset = transform.position - (player1.position + player2.position) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2;

        // Calculate distance between players
        float distance = Vector3.Distance(player1.position, player2.position);

        // Calculate target field of view
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, distance / maxDistance);

        // Set camera field of view
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * FOVSpeed);

        // Follow midpoint between players
        Vector3 targetPosition = midpoint + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);
    }
}

