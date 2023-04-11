using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBrick : MonoBehaviour
{
    EnemyStats[] enemies;
    PlayerStats[] players;

    private Collectible damageBrick;

    private void Start() {
        damageBrick = new Collectible("DamageBrick", 10, Color.red);
        enemies = FindObjectsOfType<EnemyStats>();
        players = FindObjectsOfType<PlayerStats>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player") {
            foreach (EnemyStats enemy in enemies) {
                Damage<EnemyStats>(enemy, 20);
            }
            foreach (PlayerStats player in players) {
                Damage<PlayerStats>(player, 20);
            }
            Destroy(gameObject);
        }
    }

    private void Damage<T>(T character, int damage) where T : CharacterStats {
        character.TakeDamage(damage);
    }
}
