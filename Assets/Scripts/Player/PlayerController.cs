using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles player input and movement.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Spaceship spaceship;
    private Animation anim;
    private bool isShooting;
    [SerializeField] private float shootInterval = 0.9f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spaceship = GetComponent<Spaceship>();
        anim = GetComponent<Animation>();
    }

    private void Start()
    {
        isShooting = true;
        StartCoroutine(ShootCoroutine());
    }

    private void Update()
    {
        HandleMovement();
    }

    private IEnumerator ShootCoroutine()
    {

        while (isShooting && spaceship != null)
        {
            spaceship.Shoot();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = moveDir * moveSpeed;
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceship.Shoot();
        }
    }
}
