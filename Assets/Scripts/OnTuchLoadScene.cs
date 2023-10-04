using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTuchLoadScene : MonoBehaviour
{
    public string scene;
    public string compareTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(compareTag))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
