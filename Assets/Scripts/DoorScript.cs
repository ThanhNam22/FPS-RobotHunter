using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    public GameObject pressE;
    public GameObject keyistrue;
    public GameObject keyisfalse;
    public bool isplayer;
    public Animator animator;
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        isplayer = false;
        keyisfalse.SetActive(false); // Ẩn keyisfalse ban đầu
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isplayer = true;
            pressE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isplayer = false;
            pressE.SetActive(false);
            keyisfalse.SetActive(false);
        }
    }

    void DoorOpens()
    {
        isOpen = true;
        animator.SetBool("isOpen", true);
        animator.SetBool("isClose", false);
    }
    void DoorCloses()
    {
        isOpen = false;
        animator.SetBool("isOpen", false);
        animator.SetBool("isClose", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isplayer && Input.GetKeyUp(KeyCode.F))
        {
            if (keyistrue.activeSelf)
            {
                if (!isOpen)
                {
                    DoorOpens();
                }
                else
                {
                    DoorCloses();
                }
            }
            else
            {
                keyisfalse.SetActive(true);
            }
        }
    }
}
