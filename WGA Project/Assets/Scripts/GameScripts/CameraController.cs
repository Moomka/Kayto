using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform anchorTransform;
    [SerializeField] float camFollowSpeed;
    [SerializeField] Vector3 cameraOffset;

    private void Update()
    {
        if (!GameSettings.gameOnPause)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, anchorTransform.position + cameraOffset, camFollowSpeed * Time.deltaTime);
            GameSettings.mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(GameSettings.mousePosition, 1f);
    }
}
