using UnityEngine;

public class SpecialShoot : IPlayerSkill
{
  public int staminaCost { get; } = 0;
  public SkillType SkillType => SkillType.SpecialShoot;
  public float lastUseTime { get; private set; }
  public float cooldown { get; } = 1f;
  public void Init()
  {
    lastUseTime = 0f;
    Debug.Log("SpecialShoot skill initialized.");
  }

  public void Execute()
  {
    lastUseTime = Time.time;
    Debug.Log("SpecialShoot skill executed.");
  }
}
