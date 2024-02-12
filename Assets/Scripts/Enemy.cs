using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Enemy : MonoBehaviour
{
    public Transform targetDestination;
    GameObject targetGameObject;
    [SerializeField] float speed;
    public Transform player;
    PlayerManager targetCharacter;
    public float rotationSpeed = 90f;
    [SerializeField] private Transform initialTarget;

    Rigidbody rb;   

    [SerializeField] int hp = 20;
    [SerializeField] int damage = 5;

    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (initialTarget != null)
        {
            SetTarget(initialTarget.gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
        player = target.transform;

        targetCharacter = target.GetComponent<PlayerManager>();
        if (targetCharacter == null)
        {
            Debug.LogError("PlayerManager component not found on the target GameObject.");
        }

        if (targetDestination == null && initialTarget != null)
        {
            targetDestination.position = initialTarget.position;
        }
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }
    
    private void Attack()
    {
        targetCharacter.TakeDamage(damage);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Swipe"))
        {
            animator.SetTrigger("Attack");
        }   
            
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp < 1)
        {
            Destroy(gameObject);
        }
    }


    private void LookAtPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    }

}
