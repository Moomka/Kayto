using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField] private float _targetingSpeed;
    private float _power = 0;

    private void Update()
    {
        if (GameSettings.playerState == GameSettings.playerStates.attack && Input.GetMouseButton(0))
        {
            _power += _targetingSpeed * Time.deltaTime;
            _power = Mathf.Clamp(_power, 0, 1);
        }
        if (Input.GetMouseButtonUp(0) && _power > 0)
        {
            _power = 0;
            Shoot();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.gameObject.transform.position, Vector3.Lerp(this.gameObject.transform.position, GameSettings.mousePosition, _power));
    }

    void Shoot()
    {

    }
}
