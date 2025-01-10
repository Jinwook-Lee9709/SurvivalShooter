using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private static readonly string targetName = "Player";
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Die = Animator.StringToHash("Die");
    [SerializeField] private float findTargetDistance;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private int killScore = 100;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float damage = 5f;
    
    private MonsterHealth health;
    private Animator animator;
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private GameManager gm;
    
    private LivingEntity target;

    private float nextAttack = 0f;
    private void Start()
    {
        health = GetComponent<MonsterHealth>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent.speed = maxSpeed;
        health.onDeath += OnDeath;
        health.onDamage += OnDamage;
    }

    public void Init(GameManager gm)
    {
        this.gm = gm;
    }

    private void Update()
    {
        if(target == null)
            target = FindTarget();
        if(target !=null)
            agent.SetDestination(target.transform.position);
        
        animator.SetFloat(Speed, agent.velocity.magnitude / agent.speed);
    }
    
    public LivingEntity FindTarget()
    {
        var cols = Physics.OverlapSphere(transform.position, findTargetDistance, targetLayers.value);
        foreach(var col in cols)
        {
            var livingEntity = col.GetComponent<LivingEntity>();
            if (!livingEntity.IsDead)
                return livingEntity;
        }
        return null;
    }

    public void OnDamage()
    {
        audioSource.PlayOneShot(hurtSound);
    }
    public void OnDeath()
    {
        animator.SetTrigger(Die);
        agent.isStopped = true;
        Collider[] cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            col.enabled = false;
        }
        gm.OnGetScore(killScore);
        audioSource.PlayOneShot(deathSound);
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag(targetName) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackInterval;
            var health = other.gameObject.GetComponent<PlayerHealth>();
            health.OnDamage(damage, Vector3.forward, Vector3.forward);
            
        }
    }
}
