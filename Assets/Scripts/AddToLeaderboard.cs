using UnityEngine;

public class AddToLeaderboard : MonoBehaviour
{
    public GameObject addToLeaderboardMenu;
    public void ShowAddToLeaderboardMenu() {
        addToLeaderboardMenu.SetActive(true);
    }

    public void CloseAddToLeaderboardMenu() {
        addToLeaderboardMenu.SetActive(false);
    }
}