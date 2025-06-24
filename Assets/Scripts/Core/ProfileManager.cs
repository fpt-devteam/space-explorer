using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
  private static ProfileManager _instance;
  public static ProfileManager Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = FindObjectOfType<ProfileManager>();
        if (_instance == null)
        {
          GameObject obj = new GameObject("ProfileManager");
          _instance = obj.AddComponent<ProfileManager>();
        }

        DontDestroyOnLoad(_instance.gameObject);
        _instance.LoadProfiles();
      }
      return _instance;
    }
    private set { _instance = value; }
  }

  const string FILE_NAME = "profiles.json";
  string DataPath => Application.persistentDataPath + "/";

  public ProfileList profileList;
  public ProfileMeta currentProfile;

  // void Awake()
  // {
  //   if (Instance != null && Instance != this) { Destroy(gameObject); return; }
  //   Instance = this;
  //   DontDestroyOnLoad(gameObject);
  //   LoadProfiles();
  //   print($"ProfileManager Awake: {profileList?.profiles.Count} profiles loaded.");
  // }

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

  public void CreateProfile(string playerName)
  {
    var meta = new ProfileMeta
    {
      playerId = Guid.NewGuid().ToString(),
      name = playerName,
      avatarImagePath = ""
    };
    profileList.profiles.Add(meta);
    SaveProfiles();
  }

  public void SelectProfile(string playerId)
  {
    // playerId = (playerId == "") ? GlobalData.Instance.playerId : playerId;
    // print($"Selected profile: {currentProfile.name} ({currentProfile.playerId}) Singleton: {GlobalData.Instance.playerId} PlayerId: {playerId}");
    // currentProfile = profileList.profiles.FirstOrDefault(p => p.playerId == playerId);
    // if (currentProfile == null)
    // {
    //   Debug.LogError($"Profile {playerId} not found");
    //   return;
    // }
    // PlayerManager.Instance.LoadForProfile(profileId: playerId);
  }
}
