using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardUiManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform tableContent;
    [SerializeField] private GameObject rowPrefab;

    private List<GameObject> rows = new List<GameObject>();

    void OnEnable()
    {
        LoadLeaderboardData();
    }

    public void LoadLeaderboardData()
    {
        // Xóa dữ liệu cũ
        ClearTable();

        // Tải dữ liệu mới từ LeaderboardManager
        if (LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.LoadLeaderboardData();
            
            if (LeaderboardManager.Instance.leaderboardData != null && 
                LeaderboardManager.Instance.leaderboardData.topEntries != null)
            {
                // Hiển thị dữ liệu mới
                foreach (var entry in LeaderboardManager.Instance.leaderboardData.topEntries)
                {
                    AddLeaderboardRow(entry);
                }
            }
            else
            {
                Debug.Log("Không có dữ liệu leaderboard");
            }
        }
        else
        {
            Debug.LogError("LeaderboardManager.Instance không tồn tại");
        }
    }

    private void ClearTable()
    {
        // Xóa tất cả các dòng hiện tại
        foreach (var row in rows)
        {
            Destroy(obj: row);
        }
        rows.Clear();
    }

    private void AddLeaderboardRow(LeaderboardEntry entry)
    {
        // Tạo một dòng mới
        GameObject rowObj = Instantiate(original: rowPrefab, parent: tableContent);
        rows.Add(rowObj);
        
        // Lấy tất cả các text elements trong dòng
        TextMeshProUGUI[] texts = rowObj.GetComponentsInChildren<TextMeshProUGUI>();
        
        // Đặt dữ liệu vào dòng
        if (texts.Length >= 3)
        {
            texts[0].text = (rows.Count).ToString();
            texts[1].text = entry.playerName;
            texts[2].text = entry.score.ToString();
        }
    }

    // Hàm này có thể được gọi từ một nút để làm mới bảng xếp hạng
    public void RefreshLeaderboard()
    {
        LoadLeaderboardData();
    }
}
