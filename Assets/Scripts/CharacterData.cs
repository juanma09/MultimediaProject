using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Charachter Info", menuName = "Charachter Info")]
public class CharacterData : ScriptableObject
{
    public CharacterName characterName;
    public AnimatorOverrideController animator;   
}
