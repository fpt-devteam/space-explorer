using UnityEngine;
using TMPro;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI idText;

    public void SetEntryData(int rank, LeaderboardEntry entry)
    {
        rankText.text = rank.ToString();
        nameText.text = entry.playerName;
        scoreText.text = entry.score.ToString();
        idText.text = entry.playerId.ToString().Substring(0, 8);
    }

    public void SetEmpty(int rank)
    {
        rankText.text = rank.ToString();
        nameText.text = "-";
        scoreText.text = "0";
        idText.text = "-";
    }
}
