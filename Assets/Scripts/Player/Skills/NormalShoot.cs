using UnityEngine;

public class NormalShoot : IPlayerSkill
{
  public int staminaCost { get; } = 0;
  public SkillType SkillType => SkillType.DefaultShoot;
  public float lastUseTime { get; private set; }
  public float cooldown { get; } = 0.9f;
  public void Init()
  {
    lastUseTime = 0f;
    Debug.Log("NormalShoot skill initialized.");
  }
  public void Execute()
  {
    lastUseTime = Time.time;
    Debug.Log("NormalShoot skill executed.");
  }
}
