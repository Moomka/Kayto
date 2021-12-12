using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneTrigger : MonoBehaviour
{
    [SerializeField] GameStateController controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("SceneController").GetComponent<GameStateController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            controller.NextScene();
        }
    }
}
