using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
  private Dictionary<SkillType, IPlayerSkill> skillMap;
  private IPlayerSkill[] skills;

  private void Awake()
  {
    skills = GetComponents<IPlayerSkill>();
    skillMap = new Dictionary<SkillType, IPlayerSkill>();

    foreach (var skill in skills)
    {
      if (skill is IPlayerSkill typed)
      {
        skillMap[typed.SkillType] = skill;
      }
    }
  }

  public void ExecuteSkill(SkillType type, Player player)
  {
    bool isCooldown = false;

    if (skillMap.TryGetValue(type, out var skill))
    {
      isCooldown = Time.time - skill.lastUseTime < skill.cooldown;
    }

    Debug.Log($" Executing skill: {type}, Cooldown: {isCooldown}, Stamina Cost: {skill?.staminaCost ?? 0}");

    if (!isCooldown && skill.CanUse(player.currentStamina))
    {
      skill.Execute();
    }
  }
}
