using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public bool RandomOrNot = true;
    public Transform[] moveSpots;
    private int _randomSpot;
    private float waitTime;
    public float startWaitTime;
    private int index;
    void Start()
    {
        if (RandomOrNot)
            index = UnityEngine.Random.Range(0, moveSpots.Length - 1);
        else
            index = 0;
        //_randomSpot = UnityEngine.Random.Range(0, moveSpots.Length - 1);
        waitTime = startWaitTime;
    }
    
    void Update()
    {
        MovementLogic();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                transform.SetParent(other.gameObject.transform);
                GameSettings.playerHP -= gameObject.GetComponent<Enemy>().damage;
                gameObject.GetComponent<Enemy>().GetDamage(1000);
                break;
            default:
                break;
        }
        /* if (other.gameObject.CompareTag("Player"))
         {
             rb.isKinematic = true;
             transform.SetParent(other.gameObject.transform);
             if (_damageDone == false)
             {
                 PlayerController.S.GetDamage(damage);
                 _damageDone = true;
             }
         }*/
    }
    public virtual void MovementLogic()
    {
        if (index != moveSpots.Length)
        {
            Vector3 direction = Vector3.Normalize(moveSpots[index].position - gameObject.transform.position);
            gameObject.transform.Translate(direction * Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, moveSpots[index].position) < 0.01f)
            {
                if (waitTime <= 0)
                {
                    if (RandomOrNot) 
                        index = UnityEngine.Random.Range(0, moveSpots.Length - 1);
                    else
                    {
                        index++;
                        if (index == moveSpots.Length) index = 0;
                    }
                    //_randomSpot = UnityEngine.Random.Range(0, moveSpots.Length - 1);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
    
}
