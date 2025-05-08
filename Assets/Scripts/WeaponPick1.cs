using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPick1 : MonoBehaviour
{
    public Transform equipPosition;
    public float distance = 15f;
    GameObject currentWeapon;
    private RaycastHit hit;
    bool canGrab; 

    void Update()
    {
        CheckGrab();
        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickUp();
            }
        }
    }

    private void CheckGrab()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if(hit.transform.tag == "Knife")
            {
                Debug.Log("I can grab it");
                currentWeapon = hit.transform.gameObject;
                canGrab = true;
            }
        }
        else
        {
            canGrab = false;
        }
    }

    private void PickUp()
    {
        currentWeapon.transform.position = equipPosition.position;
        currentWeapon.transform.rotation = equipPosition.rotation;
        currentWeapon.transform.parent = equipPosition;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
    }
}
