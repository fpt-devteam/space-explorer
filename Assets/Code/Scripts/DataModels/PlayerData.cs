using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerData {
  public string playerId;         
  public string playerName;
  public int highScore = 0;
  public string avatarImagePath;
  public int numStars = 0;
  public int level = 1;
  public List<string> ownedItemIds = new List<string>();
}
