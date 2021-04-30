using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public List<CharacterModel> characters = new List<CharacterModel>();

    private int selected;

    public TextMeshProUGUI characterName;
    public Image characterImage;
    public bool unlocked;

    private void Start()
    {
        selected = PlayerPrefs.GetInt("currentCharacter", 0);
        UpdateUI();
    }

    // Next character
    public void Next()
    {
        selected++;
        if (selected == characters.Count) selected = 0;
        UpdateUI();
    }

    // Previous character
    public void Prev()
    {
        selected--;
        if (selected < 0) selected = characters.Count - 1;

        UpdateUI();
    }

    public void Confirm()
    {
        // Save index of the selected character
        PlayerPrefs.SetInt("currentCharacter", selected);
        SceneManager.LoadSceneAsync(0);
    }

    private void UpdateUI()
    {
        characterName.text = characters[selected].characterName;
        characterImage.sprite = characters[selected].characterImage;
        unlocked = characters[selected].unlocked;
    }
}