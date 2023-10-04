using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public void Adios()
    {
        gameObject.GetComponent<Animator>().SetTrigger("bye!");
    }
    public void DestroyTree()
    {
        Destroy(gameObject);
    }
}
