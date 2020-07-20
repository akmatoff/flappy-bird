using System;
using UnityEngine;

public class AddToLeaderboard : MonoBehaviour
{
    public GameObject addToLeaderboardMenu;
    public void ShowAddToLeaderboardMenu() {
        addToLeaderboardMenu.SetActive(true);
    }
}