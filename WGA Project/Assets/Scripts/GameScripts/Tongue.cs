using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField] private float _targetingSpeed;
    [SerializeField] private float _attackCooldown;
    private float timeFromPastAttack;
    private float _power = 0;
    private Animation attackAnim;

    private void Awake()
    {
        attackAnim = gameObject.GetComponent<Animation>();
    }
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
            if (timeFromPastAttack > _attackCooldown)
            {
                Shoot();
            }
        }
        timeFromPastAttack += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.gameObject.transform.position, Vector3.Lerp(gameObject.transform.parent.transform.position, GameSettings.mousePosition, _power));
    }

    void Shoot()
    {
        timeFromPastAttack = 0;
        attackAnim.Play("TongueShot");
    }
}
