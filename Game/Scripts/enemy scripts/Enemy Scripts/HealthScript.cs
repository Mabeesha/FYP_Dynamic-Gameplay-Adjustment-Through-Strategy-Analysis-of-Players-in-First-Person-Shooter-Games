using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    private EnemyController enemyController;

    public float health = 100f;
    public bool is_player, is_boar;
    private bool is_dead;
    private EnemyAudio enemyAudio;

    // Start is called before the first frame update
    void Awake()
    {
        if (is_boar)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            enemyAudio = GetComponentInChildren<EnemyAudio>();
            // get enemy Audio

        }




    }

    public void ApplyDamage(float damage)
    {
        if (is_dead)
        {
            return;
        }

        health -= damage;

        if (is_boar)
        {
            if (enemyController.Enemy_State== EnemyState.PATROL)
            {
                enemyController.chase_distance = 50f;
            }
        }

        if (health <=0)
        {
            playerDied();
            is_dead = true;
        }

        void playerDied()
        {
            if (is_boar)
            {
                navAgent.velocity = Vector3.zero;
                navAgent.isStopped = true;
                enemyController.enabled = false;

                enemyAnimator.Dead();

                StartCoroutine(DeadSound());
            }
        }

        IEnumerator DeadSound()
        {
            yield return new WaitForSeconds(0.3f);
            enemyAudio.play_deadsound();
        }


    } //ApplyDamage


}
