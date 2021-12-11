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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameSettings.mousePosition = hit.point;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(GameSettings.mousePosition, 0.5f);
    }
}
