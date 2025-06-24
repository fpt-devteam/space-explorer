using System.Linq;
using TMPro;
using UnityEngine;

public class NewProfileHandler : MonoBehaviour
{
    [Header("Username Input")]
    [SerializeField] public TMP_InputField inputField;
    [Header("Profile Carousel")]
    [SerializeField] public ProfileCarousel profileCarousel;
    void Start()
    {

    }

    private void Awake()
    {
        inputField.onEndEdit.AddListener(OnEndEditSubmit);
    }

    private void OnDestroy()
    {
        inputField.onEndEdit.RemoveListener(OnEndEditSubmit);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
        if (inputField.isFocused && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            SubmitProfile();
        }
    }

    private void OnEndEditSubmit(string text)
    {
        if (string.IsNullOrEmpty(text) || ProfileManager.Instance.profileList.profiles.Any(p => p.name == text) || text == "")
        {
            Debug.LogWarning("Please enter a name or name already exists");
            return;
        }
        SubmitProfile();
    }

    public void SubmitProfile()
    {
        string userName = inputField.text.Trim();
        if (string.IsNullOrEmpty(userName) || ProfileManager.Instance.profileList.profiles.Any(p => p.name == userName) || userName == "")
        {
            Debug.LogWarning("Please enter a name or name already exists");
            return;
        }
        Debug.Log($"Creating profile: {userName}");
        ProfileManager.Instance.CreateProfile(userName);
        Debug.Log($"Profile created: {ProfileManager.Instance.profileList.profiles.Count}");

        inputField.text = "";
        gameObject.SetActive(false);

        profileCarousel.RefreshCarousel();

    }
}
