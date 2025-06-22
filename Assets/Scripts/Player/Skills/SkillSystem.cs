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
    skillMap[type].Execute();
  }
}
