using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {
    public int maxHealth = 100;
    public int currentHealth, damage, armor, currentSpeed;
    public event System.Action<int, int> OnHealthChange;
    public event System.Action<int, int> OnSpeedChange;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Image healthbar;

    private void Awake() {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage) {
        damage -= Mathf.Clamp(armor,0,armor);
        currentHealth -= damage;

        if (healthText != null) {
            if (OnHealthChange != null) {
                OnHealthChange(maxHealth, currentHealth);
            }

            print(currentHealth);
            healthText.text = $"{currentHealth}";

            LeanTween.cancel(healthbar.gameObject);
            LeanTween.value(healthbar.gameObject, healthbar.fillAmount, (float)currentHealth / (float)maxHealth, 1).setEaseOutBounce().setOnUpdate((v) => {
                healthbar.fillAmount = v;
            });
        }
        
        if (currentHealth <= 0) Die(); 
    }

    public virtual void Heal(int healAmount) {
        print("MAN");
        currentHealth += healAmount;

        if (healthText != null) {
            if (OnHealthChange != null) {
                OnHealthChange(maxHealth, currentHealth);
            }

            print(currentHealth);
            healthText.text = $"{currentHealth}";

            LeanTween.cancel(healthbar.gameObject);
            LeanTween.value(healthbar.gameObject, healthbar.fillAmount, (float)currentHealth / (float)maxHealth, 1).setEaseOutBounce().setOnUpdate((v) => {
                healthbar.fillAmount = v;
            });
        }

        if (currentHealth <= 0) Die();
    }

    public virtual void Speed(int speedAmount) {
        int newSpeed =  currentSpeed + speedAmount;
        if (OnSpeedChange != null) {
            OnSpeedChange(newSpeed, currentSpeed);
        }
    }

    public virtual void Die() {
        print("dead");    
    }
}
