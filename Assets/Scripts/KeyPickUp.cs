using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public GameObject keyistrue;
    public GameObject pressE;
    public bool isplayer;
    // Start is called before the first frame update
    void Start()
    {
        isplayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isplayer = true;
            pressE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isplayer = false;
            pressE.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isplayer)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                keyistrue.SetActive(true);
                Destroy(gameObject);
                pressE.SetActive(false);

            }
        }
    }
}
