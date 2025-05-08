using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public ParticleSystem destroyEff;
    public GameObject key;
    public TextMeshProUGUI collectibleText;
    private static float point = 0f;
    public Animator animator;

    public void TakeDamage(float amount)
    {
        if(tag != "Player")
        {
            health -= amount;
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        ParticleSystem effect = Instantiate(destroyEff, transform.position, Quaternion.identity);

        effect.Play();

        if ((tag == "Box" || tag == "Robot") && key != null)
        {
            key.transform.position = transform.position;
            key.transform.rotation = transform.rotation;
            key.SetActive(true);
        }
        if(tag == "Aim" && collectibleText != null)
        {
            point += 10;
            collectibleText.text = $"Point: {point}";
        }
        Destroy(gameObject);
        Destroy(effect.gameObject, effect.main.duration);

    }

    private void Update()
    {
        if (point >= 100 && animator != null)
        {
            animator.SetBool("open", true);
        }
    }
}
