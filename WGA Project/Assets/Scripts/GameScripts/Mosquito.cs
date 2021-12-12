using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Transform[] moveSpots;
    private float waitTime;
    public float startWaitTime;
    private int index;
    void Awake()
    {
            index = 0;
        //_randomSpot = UnityEngine.Random.Range(0, moveSpots.Length - 1);
        waitTime = startWaitTime;
    }
    
    void Update()
    {
        if (!gameObject.GetComponent<Enemy>().WaitOrAttack())
        {
            MovementLogic();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                transform.SetParent(other.gameObject.transform);
                GameSettings.playerHP -= gameObject.GetComponent<Enemy>().damage;
                Destroy(this.gameObject, 5f);
                break;
            default:
                break;
        }
    }
    public virtual void MovementLogic()
    {
        if (index != moveSpots.Length)
        {
            Vector3 direction = Vector3.Normalize(moveSpots[index].position - gameObject.transform.position);
            gameObject.transform.Translate(direction * Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, moveSpots[index].position) < 0.01f)
            {
                 index++;
                 if (index == moveSpots.Length) index = 0;
            }
        }
    }
}
