using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour {
    public int maxHealth = 100;
    public int currentHealth, damage, armor;
    public event System.Action<int, int> OnHealthChange;
    [SerializeField] TextMeshProUGUI healthText;

    private void Awake() {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage) {
        damage -= Mathf.Clamp(armor,0,armor);
        currentHealth -= damage;

        if(OnHealthChange != null) {
            OnHealthChange(maxHealth, currentHealth);
        }

        print(currentHealth);
        healthText.text = $"{currentHealth}";

        if (currentHealth <= 0) Die(); 
    }

    public virtual void Die() {
        print("dead");    
    }
}
