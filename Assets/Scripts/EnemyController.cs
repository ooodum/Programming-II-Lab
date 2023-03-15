using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    [SerializeField] CharacterStats characterStats;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttack;
    bool canCough = true;
    public GameObject projectile, coughSpawnLocation;

    public float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;

    private void Update() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) Chase();
        if (playerInAttackRange && playerInSightRange) Cough();
    }
   
    void Patrol() {
        if (!walkPointSet) SearchWalkPoint(); else {
            agent.SetDestination(walkPoint);
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if (distanceToWalkPoint.magnitude < 1f) {
                walkPointSet = false;
            }
        }   
    }

    void SearchWalkPoint() {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.localPosition.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 1f, groundLayer)) {
            walkPointSet = true;
        }
    }

    private void Chase() {
        agent.SetDestination(player.position);
    }

    private void Cough() {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!canCough) return;
        canCough = false;
        GameObject coughObject = Instantiate(projectile, coughSpawnLocation.transform.position, Quaternion.identity);
        Cough cough = coughObject.GetComponent<Cough>();
        cough.bulletOwner = global::Cough.BulletOwner.Enemy;
        cough.StartCough(transform.forward, 4, 1, 2);

        Invoke(nameof(CoughTimer), timeBetweenAttack);
    }

    private void CoughTimer() {
        canCough = true;
    }

    private void OnTriggerEnter(Collider other) {
        Cough cough;
        other.TryGetComponent<Cough>(out cough);

        if (cough.bulletOwner == global::Cough.BulletOwner.Player) {
            characterStats.TakeDamage(5);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
