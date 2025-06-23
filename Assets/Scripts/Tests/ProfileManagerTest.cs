using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManagerTest : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI debugText;
    [SerializeField] public TextMeshProUGUI profileCountText;
    [SerializeField] public TextMeshProUGUI profile1NameText;
    [SerializeField] public TextMeshProUGUI profile2NameText;
    [SerializeField] public TextMeshProUGUI profile3NameText;
    [SerializeField] public TextMeshProUGUI profile1AvatarText;
    [SerializeField] public TextMeshProUGUI profile2AvatarText;
    [SerializeField] public TextMeshProUGUI profile3AvatarText;
    [SerializeField] public TextMeshProUGUI currentProfileText;

    private ProfileManager profileManager;

    private void Start()
    {
        profileManager = ProfileManager.Instance;
        if (profileManager == null)
        {
            Debug.LogError(message: "ProfileManager instance not found!");
            return;
        }

        RunTests();
    }

    private void RunTests()
    {
        LogMessage(message: "Starting profile manager tests");

        DeleteExistingProfiles();

        SeedProfiles();

        UpdateUI();

        LoadAndVerifyProfiles();
        
        TestSelectProfile();
    }
    
    private void DeleteExistingProfiles()
    {
        LogMessage(message: "Deleting existing profiles");
        profileManager.profileList.profiles.Clear();
        profileManager.SaveProfiles();
        profileManager.LoadProfiles();
        
        if (profileManager.profileList.profiles.Count == 0)
        {
            LogMessage(message: "Successfully cleared all profiles");
        }
        else
        {
            LogMessage(message: "ERROR: Failed to clear profiles");
        }
    }

    private void SeedProfiles()
    {
        LogMessage(message: "Creating 3 test profiles");
        PlayerData player1 = new PlayerData() { playerId = Guid.NewGuid().ToString(), playerName = "Astronaut Alpha", avatarImagePath = "Assets/Art/Avatars/Avatar1.png" };
        PlayerData player2 = new PlayerData() { playerId = Guid.NewGuid().ToString(), playerName = "Captain Beta", avatarImagePath = "Assets/Art/Avatars/Avatar2.png" };
        PlayerData player3 = new PlayerData() { playerId = Guid.NewGuid().ToString(), playerName = "Pilot Gamma", avatarImagePath = "Assets/Art/Avatars/Avatar3.png" };
        
        profileManager.CreateProfile(player1.playerId, player1.playerName, player1.avatarImagePath);
        
        profileManager.CreateProfile(player2.playerId, player2.playerName, player2.avatarImagePath);
        
        profileManager.CreateProfile(player3.playerId, player3.playerName, player3.avatarImagePath);
                
        LogMessage(message: $"Created {profileManager.profileList.profiles.Count} profiles");
    }

    private void LoadAndVerifyProfiles()
    {
        profileManager.LoadProfiles();
        
        int profileCount = profileManager.profileList.profiles.Count;
        LogMessage(message: $"Loaded {profileCount} profiles");
        
        if (profileCount != 3)
        {
            LogMessage(message: "TEST FAILED: Incorrect number of profiles loaded!");
            return;
        }
        
        List<string> profileNames = new List<string>();
        foreach (var profile in profileManager.profileList.profiles)
        {
            profileNames.Add(profile.name);
            LogMessage(message: $"Profile: {profile.playerId} - {profile.name}");
        }
        
        bool hasAstronaut = profileNames.Contains("Astronaut Alpha");
        bool hasCaptain = profileNames.Contains("Captain Beta");
        bool hasPilot = profileNames.Contains("Pilot Gamma");
        
        bool testPassed = hasAstronaut && hasCaptain && hasPilot;
        
        if (testPassed)
        {
            LogMessage(message: "TEST PASSED: All profiles loaded successfully!");
            UpdateUI();
        }
        else
        {
            LogMessage(message: "TEST FAILED: Missing profiles!");
            if (!hasAstronaut) LogMessage(message: "Missing: Astronaut Alpha");
            if (!hasCaptain) LogMessage(message: "Missing: Captain Beta");
            if (!hasPilot) LogMessage(message: "Missing: Pilot Gamma");
        }
    }
    
    private void TestSelectProfile()
    {
        if (profileManager.profileList.profiles.Count > 0)
        {
            var firstProfile = profileManager.profileList.profiles[0];
            LogMessage(message: $"Testing profile selection: {firstProfile.name}");

            profileManager.SelectProfile(playerId: Guid.NewGuid().ToString());
            profileManager.SelectProfile(playerId: firstProfile.playerId);
            
            if (profileManager.currentProfile != null && 
                profileManager.currentProfile.playerId == firstProfile.playerId)
            {
                LogMessage(message: $"TEST PASSED: Profile selection works correctly");

                if (currentProfileText != null)
                {
                    currentProfileText.text = $"Current: {profileManager.currentProfile.name}";
                }
            }
            else
            {
                LogMessage(message: "TEST FAILED: Profile selection failed!");
            }
        }
    }
    
    private void UpdateUI()
    {
        if (profileCountText != null)
        {
            profileCountText.text = $"Total Profiles: {profileManager.profileList.profiles.Count}";
        }
        
        if (profileManager.profileList.profiles.Count >= 1 && profile1NameText != null)
        {
            profile1NameText.text = profileManager.profileList.profiles[0].name;
            profile1AvatarText.text = profileManager.profileList.profiles[0].avatarImagePath;
        }
        
        if (profileManager.profileList.profiles.Count >= 2 && profile2NameText != null)
        {
            profile2NameText.text = profileManager.profileList.profiles[1].name;
            profile2AvatarText.text = profileManager.profileList.profiles[1].avatarImagePath;
        }
        
        if (profileManager.profileList.profiles.Count >= 3 && profile3NameText != null)
        {
            profile3NameText.text = profileManager.profileList.profiles[2].name;
            profile3AvatarText.text = profileManager.profileList.profiles[2].avatarImagePath;
        }
    }

    private void LogMessage(string message)
    {
        Debug.Log(message: message);
        if (debugText != null)
        {
            debugText.text += message + "\n";
        }
    }
}