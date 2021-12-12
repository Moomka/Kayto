using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneTrigger : MonoBehaviour
{
    [SerializeField] GameStateController controller;

    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("SceneController").GetComponent<GameStateController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            controller.NextScene();
        }
    }
}
