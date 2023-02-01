using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public int maxHealth = 100;
    public int currentHealth, damage, armor;

    private void Awake() {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage) {
        damage -= Mathf.Clamp(armor,0,armor);
        currentHealth -= damage;

        if (currentHealth <= 0) Die(); 
    }

    public virtual void Die() {
        print("dead");    
    }
}
