using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Transform player;
    [SerializeField]private GameObject child;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        var distance = player.position - transform.position;
        if(distance.x < 30)
        {
            child.SetActive(true);
        }
        else
        {
            child.SetActive(false);
        }
    }
}
