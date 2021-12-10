using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform anchorTransform;
    [SerializeField] float camFollowSpeed;
    private void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, anchorTransform.position, camFollowSpeed * Time.deltaTime);
    }
}
