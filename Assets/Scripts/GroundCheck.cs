using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool tuchGround;
    private void OnTriggerEnter2D(Collider2D ground)
    {
        if(ground.CompareTag("Ground")) tuchGround = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            tuchGround = true;
        }
        else
        {
            tuchGround=false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) tuchGround = false;
    }
}
