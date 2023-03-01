using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    CharacterStats characterStats;
    public bool canBeAttacked = true;
    [SerializeField] float iFrames;
    private void Awake() {
        characterStats = GetComponent<EnemyStats>();
    }
    public void TakeDamage(int damage) {
        if (!canBeAttacked) return;
        characterStats.TakeDamage(damage);
        Invoke(nameof(iFrameReset), iFrames);
    }
    private void iFrameReset() {
        canBeAttacked = true;
    }
}
