using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float curr_health = 50f;

    public GameObject healthBarUI;
    public Image healthBar;

    private float initial_health;

    void Start()
    {
        initial_health = curr_health;
        healthBar.fillAmount = curr_health / initial_health;
    }

    public void TakeDamage(float amount)
    {
        healthBarUI.SetActive(true);
        curr_health -= amount;
        healthBar.fillAmount = curr_health / initial_health;
        if (curr_health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
