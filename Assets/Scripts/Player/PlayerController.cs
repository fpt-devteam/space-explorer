using System.Collections;
using UnityEngine;

public enum SkillType
{
    DefaultShoot,
    SpecialShoot
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private SkillSystem skillSystem;
    private Spaceship spaceship;
    private Rigidbody2D rb;
    private bool isShooting;
    [SerializeField] private float shootInterval = 0.9f;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        spaceship = GetComponent<Spaceship>();
        skillSystem = GetComponent<SkillSystem>();
    }

    private void Start()
    {
        isShooting = true;
        StartCoroutine(ShootCoroutine());
    }

    private void Update()
    {
        HandleMovement();
        HandleSkills();
    }
    private void HandleSkills()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     skillSystem.ExecuteSkill(SkillType.SpecialShoot, player);
        // }
        // else
        // {
        //     skillSystem.ExecuteSkill(SkillType.DefaultShoot, player);
        // }

        // skillSystem.ExecuteSkill(SkillType.DefaultShoot, player);
    }
    private IEnumerator ShootCoroutine()
    {
        while (isShooting && spaceship != null)
        {
            spaceship.Shoot();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player collided with: " + collision.gameObject.name);

        if (collision.CompareTag("Asteroid"))
        {
            if (player.isShieldActive)
            {
                player.isShieldActive = false;
            }
            else
            {
                player.currentHealth -= 10f;
            }
        }
        if (collision.CompareTag("HealthPickup"))
        {
            player.currentHealth += 10f;
        }
        if (collision.CompareTag("StaminaPickup"))
        {
            player.currentStamina += 10f;
        }
        if (collision.CompareTag("ShieldPickup"))
        {
            player.isShieldActive = true;
        }
        else if (collision.CompareTag("StarPickup"))
        {
            // player.StarCount += 1;
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = moveDir * player.MoveSpeed;

        Vector2 clampedPos = rb.position;
        float minX = -8f;
        float maxX = 8f;
        float minY = -4f;
        float maxY = 4f;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);

        rb.position = clampedPos;

        float baseRotationSpeed = 180f;
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0f, 0f, baseRotationSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0f, 0f, -baseRotationSpeed * Time.deltaTime);
        }
    }
}
