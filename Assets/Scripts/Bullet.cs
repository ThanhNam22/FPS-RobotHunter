using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with "Target" or "Robot"
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Robot"))
        {
            HealthBar enemyHealthBar = collision.gameObject.GetComponentInChildren<HealthBar>(); ;

            if (enemyHealthBar != null)
            {
                Debug.Log("hit Robot");
                enemyHealthBar.TakeDamage(10);
            }
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        // Handle collision with "Player"
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Get the HealthBar component attached to the player
            HealthBar playerHealthBar = collision.gameObject.GetComponentInChildren<HealthBar>(); ;

            if (playerHealthBar != null)
            {
                playerHealthBar.TakeDamage(5);
            }

            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(collision.gameObject.transform);
    }
}
