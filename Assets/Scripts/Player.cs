using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterName
{
    Adam = 1,
    Amelia,
}
public class Player : MonoBehaviour
{
    [SerializeField] private CharacterName characterName;

    private static CharacterData[] characters;


    // Start is called before the first frame update
    void Start()
    {
        if (characters == null)
        {
            characters = Resources.LoadAll<CharacterData>("CharacterData");
            Debug.Log("Characters loaded: " + characters.Length);
        }
        UpdateCharacters();
    }


    void UpdateCharacters()
    {
        foreach (var character in characters)
        {
            if (character.characterName == characterName)
            {
                Debug.Log("Character found: " + character.characterName);
                GetComponent<Animator>().runtimeAnimatorController = character.animator;
            }
        }
    }
}
