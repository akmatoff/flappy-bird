using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public List<CharacterModel> characterList = new List<CharacterModel>();

    public TextMeshProUGUI characterName;
    public Image character;
    public bool unlocked = false;
}

public class CharacterModel
{
    public Sprite characterImage;
    public string characterName;
    public bool unlocked;
}