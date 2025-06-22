using UnityEngine;
public interface IPlayerSkill
{
  int staminaCost { get; }
  float lastUseTime { get; }
  float cooldown { get; }
  SkillType SkillType { get; }
  void Init();
  void Execute();
  bool CanUse(float currentStamina) => currentStamina >= staminaCost;
}
