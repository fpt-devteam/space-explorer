using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
  public static ProfileManager Instance { get; private set; }

  const string FILE_NAME = "profiles.json";
  string DataPath => Application.persistentDataPath + "/";

  public ProfileList profileList;
  public ProfileMeta currentProfile;

  void Awake()
  {
    if (Instance != null && Instance != this) { Destroy(gameObject); return; }
    Instance = this;
    DontDestroyOnLoad(gameObject);
    LoadProfiles();
  }

  public void LoadProfiles()
  {
    string path = Path.Combine(DataPath, FILE_NAME);
    if (File.Exists(path))
    {
      string json = File.ReadAllText(path);
      profileList = JsonUtility.FromJson<ProfileList>(json);
    }
    else
    {
      profileList = new ProfileList();
      SaveProfiles();
    }
  }

  public void SaveProfiles()
  {
    string json = JsonUtility.ToJson(profileList, true);
    File.WriteAllText(Path.Combine(DataPath, FILE_NAME), json);
  }

  public void CreateProfile(string playerId, string playerName, string avatarImagePath)
  {
    var meta = new ProfileMeta
    {
      playerId = playerId,
      name = playerName,
      avatarImagePath = avatarImagePath
    };
    profileList.profiles.Add(meta);
    SaveProfiles();
  }

  public void SelectProfile(string playerId)
  {
    currentProfile = profileList.profiles.FirstOrDefault(p => p.playerId == playerId);
    if (currentProfile == null)
    {
      Debug.LogError($"Profile {playerId} not found");
      return;
    }
    PlayerManager.Instance.LoadForProfile(currentProfile.playerId);
  }
}
