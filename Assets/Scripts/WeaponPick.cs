using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class WeaponPick : MonoBehaviour
{
    public Transform equipPosition;
    public float distance = 15f;
    GameObject currentWeapon;
    public GameObject pressF;
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
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if(hit.transform.tag == "CanGrab")
            {
                pressF.SetActive(true);
                Debug.Log("I can grab it");
                currentWeapon = hit.transform.gameObject;
                canGrab = true;
            }
            else
            {
                pressF.SetActive(false);
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

        Gun gunScript = currentWeapon.GetComponent<Gun>();
        if (gunScript != null)
        {
            gunScript.canShoot = true;
        }
    }
}
