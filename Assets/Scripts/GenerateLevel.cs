
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Biomes
{
    Forest,
    WinterForest,
    Desert
}
public class GenerateLevel : MonoBehaviour
{
    public GameObject[] forestPrefabs;
    public GameObject[] winterPrefabs;
    public GameObject[] desertPrefabs;
    [SerializeField] GameObject ForestEndPrefab;
    [SerializeField] GameObject WinterEndPrefab;
    [SerializeField] GameObject DesertEndPrefab;
    [SerializeField] int len=10;
    public Biomes current_biome = Biomes.Forest;
    [SerializeField]private int levelIndex;
    float next;

    private int changeLevelLockInt = 0;
    private void Start()
    {
        changeLevelLockInt = PlayerPrefs.GetInt("LevelLocked");
        Debug.Log("level locked = " + changeLevelLockInt.ToString());

        levelIndex = PlayerPrefs.GetInt("currentLevelIndex");
        if (levelIndex == 0)
        {
            levelIndex = 1;
            current_biome = Biomes.Forest;
        }
        else if(levelIndex == 1)
        {
            levelIndex = 2;
            current_biome = Biomes.WinterForest;
        }
        else if(levelIndex == 2)
        {
            levelIndex = 0;
            current_biome = Biomes.Desert;
        }
        else
        {
            levelIndex = 0;
        }

        if(changeLevelLockInt == 0) PlayerPrefs.SetInt("currentLevelIndex",levelIndex);
        Debug.Log("level index: " + levelIndex.ToString());
        next = 25f;
        
        if(current_biome == Biomes.Forest)
        {
            Instantiate(forestPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
            for (int i = 0; i < len; i++)
            {
                Instantiate(forestPrefabs[UnityEngine.Random.Range(0, forestPrefabs.Length)], new Vector3(next, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
                next += 25f;
            }
            Instantiate(ForestEndPrefab, new Vector3(next, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
        }
        else if(current_biome== Biomes.WinterForest)
        {
            Instantiate(winterPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
            for (int i = 0; i < len; i++)
            {
                Instantiate(winterPrefabs[UnityEngine.Random.Range(0, winterPrefabs.Length)], new Vector3(next, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
                next += 25f;
            }
            Instantiate(WinterEndPrefab, new Vector3(next, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
        }
        else if (current_biome == Biomes.Desert)
        {
            Instantiate(desertPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
            for (int i = 0; i < len; i++)
            {
                Instantiate(desertPrefabs[UnityEngine.Random.Range(0, winterPrefabs.Length)], new Vector3(next, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
                next += 25f;
            }
            Instantiate(DesertEndPrefab, new Vector3(next, 0f, 0f), Quaternion.identity, transform.parent = gameObject.transform);
        }
    }
}
