using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 20.0f;
    public float maxHealth = 100.0f;
    public float attackDamage = 20.0f;
    public float attackCooldown = 0.5f;
    public float attackTimer = 0.0f;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider punchCollider;
    [SerializeField] private BoxCollider kickCollider;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float health;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode block;
    [SerializeField] private KeyCode punch;
    [SerializeField] private KeyCode kick;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If not attacking, handle movement
        if (!isAttacking)
        {
            moveDirection = new Vector3(0, moveDirection.y, 0);

            anim.SetBool("Idle", true);
            anim.SetBool("IsWalkingForward", false);
            anim.SetBool("IsWalkingBackward", false);
            if (Input.GetKey(moveRight))
            {
                // Move Right
                moveDirection = new Vector3(1, moveDirection.y, 0) * moveSpeed * Time.deltaTime;
                anim.SetBool("Idle", false);
                anim.SetBool("IsWalkingBackward", true);
            }
            if (Input.GetKey(moveLeft))
            {
                // Move Left
                moveDirection = new Vector3(-1, moveDirection.y, 0) * moveSpeed * Time.deltaTime;
                anim.SetBool("Idle", false);
                anim.SetBool("IsWalkingForward", true);
            }

            if (controller.isGrounded)
            {
                if (Input.GetKey(jumpKey))
                {
                    moveDirection.y = jumpSpeed;
                }
                else
                {
                    moveDirection.y = 0;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }

        // Handle attacking
        if (Input.GetKeyDown(punch) && !isAttacking && attackTimer <= 0.0f)
        {
            anim.SetBool("Punch", true);
            isAttacking = true;
            attackTimer = attackCooldown;
            ActivateHitBoxPunch(true);
        }
        else if (Input.GetKeyDown(kick) && !isAttacking && attackTimer <= 0.0f)
        {
            anim.SetBool("Kick", true);
            isAttacking = true;
            attackTimer = attackCooldown;
            ActivateHitBoxKick(true);
        }

        // Update attack timer
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0.0f)
            {
                isAttacking = false;
                anim.SetBool("Punch", false);
                anim.SetBool("Kick", false);
                ActivateHitBoxPunch(false);
                ActivateHitBoxKick(false);
            }
        }
    }

    // Handle taking damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        anim.SetTrigger("hurt");

        if (health <= 0.0f)
        {
            Die();
        }
    }

    public void ActivateHitBoxKick(bool isActive)
    {
        kickCollider.enabled = isActive;
    }

    public void ActivateHitBoxPunch(bool isActive)
    {
        punchCollider.enabled = isActive;
    }

    // Handle dying
    private void Die()
    {
        anim.SetFloat("Die", 0);
        controller.enabled = false;
        this.enabled = false;
    }
}
