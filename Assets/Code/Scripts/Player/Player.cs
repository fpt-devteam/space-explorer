using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
  [Header("Base Stats")]
  [SerializeField] public int baseHealth { get; set; } = 11;
  [SerializeField] public int maxHealth { get; set; } = 11;
  [SerializeField] public int baseShield { get; set; } = 2;
  [SerializeField] public int maxShield { get; set; } = 11;
  [SerializeField] private int baseDamage { get; set; } = 10;
  [SerializeField] private int baseMoveSpeed { get; set; } = 5;
  [SerializeField] private int baseAttackSpeed { get; set; } = 1;

  [Header("Base Skills")]
  [SerializeField] private IPlayerSkill[] baseSkills;

  [Header("Runtime Stats")]
  public int currentHealth { get; set; }
  public int currentShield { get; set; }

  public int Damage => baseDamage;
  public int MoveSpeed => baseMoveSpeed;
  public int AttackSpeed => baseAttackSpeed;

  void Awake()
  {
    InitStats();
  }

  public void InitStats()
  {
    currentHealth = baseHealth;
    currentShield = baseShield;
  }

  public void Shoot()
  {

  }
}
