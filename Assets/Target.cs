using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        slider.value = health;
    }

    public void TakeDamage(float amount)
    {
        healthBarUI.SetActive(true);
        health -= amount;
        slider.value = health;
        Debug.Log(health);
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
