using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DamageHandler_boar : vp_DamageHandler
{
    private Animator animator;
    private NavMeshAgent _navMeshAgent;
    public float AttackDistance = 10.0f;
    [SerializeField] private GameObject Player;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void Damage(vp_DamageInfo damageInfo)
    {
        Log.logByAnotherObject("Boar_Hit  : ");
        base.Damage(damageInfo);
        animator.Play("Hit", 0, 0.25f);

          //  animator.enabled = false;
        

    }

    public override void Die()
    {
        Log.logByAnotherObject("Boar_dead  : ");
        if (!enabled || !vp_Utility.IsActive(gameObject))
            return;

        if (m_Audio != null)
        {
            m_Audio.pitch = Time.timeScale;
            m_Audio.PlayOneShot(DeathSound);
        }

        animator.Play("Die");
        Destroy(GetComponent<vp_SurfaceIdentifier>());

        //base.Die();
    }

    public void EndAttack()
    {
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);

        // TODO: Get rid of this magic number here: (perhaps add property)
        if (dist < 3.0f)
        {
            Player.SendMessage("Damage", 4.0f, SendMessageOptions.DontRequireReceiver);
        }
    }


}
