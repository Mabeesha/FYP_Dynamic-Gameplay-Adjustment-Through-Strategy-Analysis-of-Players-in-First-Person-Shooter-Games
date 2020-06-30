using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}


public class EnemyController : vp_DamageHandler
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    //public GameObject screenFlash;
    private EnemyState enemyState;
    public float DamagePoints = 8.0f;
    public float walk_speed = 0.5f;

    public float run_speed = 4f;

    public float chase_distance = 7f;

    private float current_chase_distance;

    public float attack_distance = 1.8f;
    public float chase_After_attack = 2f;

    public float patrol_radius_min = 20f, patrol_radius_max = 30f;
    public float patrol_for_this_time = 15f;
    private float patrol_timer;

    private float wait_before_attack = 2f;

    private float attack_timer;
    private Transform target;
    private Animator anim;
    private GameObject Player;
    public GameObject attck_point;
    private EnemyAudio enemyAudio;
    private bool isDead = false;

    // Start is called before the first frame update
    protected override void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        enemyAudio = GetComponentInChildren<EnemyAudio>();

    }

    private void Start()
    {
        enemyState = EnemyState.PATROL;
        patrol_timer = patrol_for_this_time;

        // wait a little time before attack
        attack_timer = wait_before_attack;

        current_chase_distance = chase_distance; // memorze the value of chase distance

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (enemyState == EnemyState.PATROL)
            {
                patrol();
            }

            if (enemyState == EnemyState.CHASE)
            {
                chase();
            }

            if (enemyState == EnemyState.ATTACK)
            {
                attack();
            }
        }

    }
    void patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walk_speed;

        patrol_timer += Time.deltaTime;

        if (patrol_timer > patrol_for_this_time)
        {
            setNewRandomDestination();

            patrol_timer = 0f;
        }

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            //print("Walking...........................");
            enemyAnimator.Walk(true);
        }
        else
        {
            //print("Not walking");
            enemyAnimator.Walk(false); ;
        }

        if (Vector3.Distance(transform.position, target.position) <= chase_distance)
        {

            enemyAnimator.Walk(false);
            enemyState = EnemyState.CHASE;
            enemyAudio.play_screamsound();
        }


    }

    public void ShootEvent()
    {
        //  Debug.Log("Shooot event calling");
        if (m_Audio != null)
        {
            //Debug.Log("Playing gun sound");
           // m_Audio.PlayOneShot(GunSound);
        }

        float random = Random.Range(0.0f, 1.0f);

        // The higher the accuracy is, the more likely the player will be hit
    //    bool isHit = random > 1.0f - HitAccuracy;
            //   StartCoroutine(playFlash());
            // GlobalHealth.playerHealth -= 10;
            //Debug.Log("Wadunooooooooo");
            Player.SendMessage("Damage", DamagePoints, SendMessageOptions.DontRequireReceiver);

        
    }

    public override void Damage(vp_DamageInfo damageInfo)
    {
        Log.logByAnotherObject("Boar_Hit  : ");
        base.Damage(damageInfo);
        //animator.Play("Hit", 0, 0.25f);

        //  animator.enabled = false;
    }

    public override void Die()
    {
        isDead = true;
        Log.logByAnotherObject("Boar_dead  :");
        //soldierHealth = -10f;
        //GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
        if (!enabled || !vp_Utility.IsActive(gameObject))
            return;
        //m_Audio.PlayOneShot(DeathSound);
        if (m_Audio != null)
        {   
            m_Audio.pitch = Time.timeScale;
            m_Audio.PlayOneShot(DeathSound);
        }
         
        enemyAudio.play_deadsound();
        navAgent.enabled = false;

        //anim.SetBool("Run", false);
        //anim.SetBool("Shoot", false);
        //GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
        //GetComponent<Rigidbody>().AddTorque(-transform.forward * 100f);
       // EnemyManager.instance.EnemyDied();
        
        anim.SetTrigger("Dead");
        //Destroy(anim);
        Destroy(GetComponent<vp_SurfaceIdentifier>());

        //StartCoroutine("DeleteObject");

    }
    void chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = run_speed;

        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            //print("Runing...........................");

            enemyAnimator.Run(true);
        }
        else
        {
            print("Not Running");
            enemyAnimator.Run(false); ;
        }

        if (Vector3.Distance(transform.position, target.position) <= attack_distance)
        {
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            enemyState = EnemyState.ATTACK;
            
            if (chase_distance != current_chase_distance)
            {
                chase_distance = current_chase_distance;
            }

        } else if (Vector3.Distance(transform.position, target.position) > chase_distance)
        {
            enemyAnimator.Run(false);
            enemyState = EnemyState.PATROL;

            patrol_timer = patrol_for_this_time;

            if (chase_distance != current_chase_distance)
            {
                chase_distance = current_chase_distance;
            }
        }

    } // chase

    void attack()
    {
        //anim.SetTrigger("Attack");
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_timer += Time.deltaTime;

        if (attack_timer > wait_before_attack)
        {
            
            //GlobalHealth.playerHealth -= 10;  // for now it's reduced by 10 , needed to be changed
            
            attack_timer = 0f;
            StartCoroutine(playFlash());
        //    screenFlash.SetActive(false);
            //play sound
            enemyAudio.play_attacksound();
        }


        if (Vector3.Distance(transform.position, target.position) > (attack_distance + chase_After_attack))
        {
            enemyState = EnemyState.CHASE;
        }




    }

    IEnumerator playFlash()
    {   
        //screenFlash.SetActive(true);
        enemyAnimator.Attack();
        yield return new WaitForSeconds(0.05f);
        //screenFlash.SetActive(false);
    }


        void setNewRandomDestination()
    {
        float rand_radius = Random.Range(patrol_radius_min, patrol_radius_max);

        Vector3 randDir = Random.insideUnitSphere * rand_radius;
        randDir += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, rand_radius, -1); // 

        navAgent.SetDestination(navHit.position); // doesn't give outside of the map

    }

    void Turn_on_attackPoint()
    {
        attck_point.SetActive(true);
    }

    void Turn_off_attackPoint()
    {
        if (attck_point.activeInHierarchy)
        {
            attck_point.SetActive(false);
        }
    }


    public EnemyState Enemy_State{
        get; set;
    }


} // class
