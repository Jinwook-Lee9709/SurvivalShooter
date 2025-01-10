using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Die = Animator.StringToHash("Die");
    [SerializeField] float moveSpeed = 100f;
    [SerializeField] private Transform character;
    [SerializeField] private Gun gun;
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    
    
    private PlayerInput input;
    private PlayerHealth health;
    private Rigidbody rb;
    private Animator animator;
    private AudioSource audioSource;
    private GameManager gm;
    private Vector3 newVelocity;
    

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        health.onDeath += OnDie;
        health.onDamage += OnDamage;
    }

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (health.IsDead || gm.isPaused)
            return;
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0f;
            character.rotation = Quaternion.LookRotation(dir);
        }
        newVelocity = new Vector3(input.SideMove, 0f, input.ForthMove);
        if (input.Fire)
        {
            if (gun.Fire())
            {
                audioSource.PlayOneShot(gunShotSound);
            }
        }
    }

    private void FixedUpdate()
    {
        if (health.IsDead || gm.isPaused)
            return;
        rb.velocity = newVelocity * moveSpeed;
        
        animator.SetFloat(Speed, rb.velocity.magnitude / moveSpeed);
    }

    private void OnDamage()
    {
        audioSource.PlayOneShot(hurtSound);
    }
    private void OnDie()
    {
        audioSource.PlayOneShot(deathSound);
        animator.SetTrigger(Die);
    }
}
