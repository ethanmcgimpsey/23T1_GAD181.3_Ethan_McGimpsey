using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 20.0f;
    public float maxHealth = 100.0f;
    public float attackRange = 2.0f;
    public float attackDamage = 20.0f;
    public float attackCooldown = 0.5f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDirection = Vector3.zero;
    private float health;
    private bool isAttacking = false;
    private float attackTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If not attacking, handle movement
        if (!isAttacking)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
            moveDirection *= moveSpeed;

            if (controller.isGrounded)
            {
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

            // Flip character sprite to face movement direction
            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }

        // Handle attacking
        if (Input.GetButtonDown("Fire1") && !isAttacking && attackTimer <= 0.0f)
        {
            animator.SetTrigger("attack");
            isAttacking = true;
            attackTimer = attackCooldown;

            // Check for hit on enemy
            Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);

            foreach (Collider hit in hits)
            {
                if (hit.gameObject.CompareTag("Enemy"))
                {
                    hit.gameObject.SendMessage("TakeDamage", attackDamage);
                }
            }
        }

        // Update attack timer
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0.0f)
            {
                isAttacking = false;
            }
        }
    }

    // Handle taking damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("hurt");

        if (health <= 0.0f)
        {
            Die();
        }
    }

    // Handle dying
    private void Die()
    {
        animator.SetTrigger("die");
        controller.enabled = false;
        this.enabled = false;
    }
}
