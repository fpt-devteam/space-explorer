using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Base Stats")]
  [SerializeField] public float baseHealth { get; set; } = 100f;
  [SerializeField] public float baseStamina { get; set; } = 50f;
  [SerializeField] private float baseDamage { get; set; } = 10f;
  [SerializeField] private float baseMoveSpeed { get; set; } = 5f;
  [SerializeField] private float baseAttackSpeed { get; set; } = 1f;

  [Header("Base Skills")]
  [SerializeField] private IPlayerSkill[] baseSkills;

  [Header("Runtime Stats")]
  public float currentHealth { get; set; }
  public float currentStamina { get; set; }
  public bool isShieldActive { get; set; }

  public float Damage => baseDamage;
  public float MoveSpeed => baseMoveSpeed;
  public float AttackSpeed => baseAttackSpeed;

  void Awake()
  {
    currentHealth = 0;
    currentStamina = 0;
  }
}
