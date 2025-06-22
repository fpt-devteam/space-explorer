using UnityEngine;

public class NormalShoot : MonoBehaviour, IPlayerSkill
{
  public int staminaCost { get; } = 0;
  public SkillType SkillType => SkillType.DefaultShoot;
  public float lastUseTime { get; private set; }
  public float cooldown { get; } = 0.9f;

  private float cooldownTimer = 0f;

  private void Update()
  {
    cooldownTimer -= Time.deltaTime;
  }

  public void Execute()
  {
    if (cooldownTimer > 0f) return;
    lastUseTime = Time.time;
    //
    Debug.Log("NormalShoot skill executed.");
  }
}
