using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] bool music = false;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!music)
            {
                audioSources[i].volume = gameManager.soundEffectsVolume * gameManager.globalVolume;
            }
            else
            {
                audioSources[i].volume = gameManager.musicVolume * gameManager.globalVolume;
            }
        }
    }
}
