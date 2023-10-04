using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Player player;
    [SerializeField] private bool[] index;
    [SerializeField] private GameObject[] hearts;
    private void Start()
    {
        player = FindObjectOfType<Player>();    
    }
    private void Update()
    {
        if(player != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (player.hearts > i)
                {
                    hearts[i].SetActive(true);
                }
                else
                {
                    hearts[i].SetActive(false);
                }
            }
        }
    }
}
