using UnityEngine;

/// <summary>
/// Component that reflects projectiles back at the player
/// Used by the boss during the Core Awakening state shield phase
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ProjectileReflector : MonoBehaviour
{
  [Header("Reflection Settings")]
  [SerializeField] private float reflectionSpeedMultiplier = 1.2f;
  [SerializeField] private LayerMask reflectableLayers = -1;
  [SerializeField] private string[] reflectableTags = { "Bullet" };

  [Header("Visual Effects")]
  [SerializeField] private GameObject reflectionEffectPrefab;
  [SerializeField] private bool showDebugRays = true;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!enabled) return;

    if (CanReflect(collision))
    {
      ReflectProjectile(collision);
    }
  }

  private bool CanReflect(Collider2D collision)
  {
    if ((reflectableLayers.value & (1 << collision.gameObject.layer)) == 0)
      return false;

    foreach (string tag in reflectableTags)
    {
      if (collision.CompareTag(tag))
        return true;
    }

    return false;
  }

  private void ReflectProjectile(Collider2D projectile)
  {
    Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
    if (projectileRb == null) return;

    Vector2 incomingDirection = projectileRb.linearVelocity.normalized;
    Vector2 surfaceNormal = GetSurfaceNormal(projectile.transform.position);
    Vector2 reflectedDirection = Vector2.Reflect(incomingDirection, surfaceNormal);

    float currentSpeed = projectileRb.linearVelocity.magnitude;
    projectileRb.linearVelocity = reflectedDirection * currentSpeed * reflectionSpeedMultiplier;

    float angle = Mathf.Atan2(reflectedDirection.y, reflectedDirection.x) * Mathf.Rad2Deg;
    projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

    ChangeProjectileToReflected(projectile);

    PlayReflectionEffect(projectile.transform.position);

    if (showDebugRays)
    {
      Debug.DrawRay(transform.position, incomingDirection * 2f, Color.red, 1f);
      Debug.DrawRay(transform.position, reflectedDirection * 2f, Color.green, 1f);
      Debug.DrawRay(transform.position, surfaceNormal, Color.blue, 1f);
    }

    Debug.Log("Projectile reflected by boss shield!");
  }

  private Vector2 GetSurfaceNormal(Vector3 collisionPoint)
  {
    Vector2 directionFromCenter = (collisionPoint - transform.position).normalized;
    return directionFromCenter;
  }

  private void ChangeProjectileToReflected(Collider2D projectile)
  {
    projectile.tag = "EnemyBullet";

    SpriteRenderer spriteRenderer = projectile.GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
      spriteRenderer.color = Color.red;
    }

    Projectile projectileScript = projectile.GetComponent<Projectile>();
    if (projectileScript != null)
    {
    }
  }

  private void PlayReflectionEffect(Vector3 position)
  {
    if (reflectionEffectPrefab != null)
    {
      GameObject effect = Instantiate(reflectionEffectPrefab, position, Quaternion.identity);
      Destroy(effect, 2f);
    }
  }

  private void OnDrawGizmos()
  {
    if (!enabled) return;

    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, GetComponent<Collider2D>().bounds.size.x * 0.5f);
  }
}
