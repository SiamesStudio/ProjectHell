using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristManager : MonoBehaviour
{
    public enum CharacterName {Tom, Eva, Daniel, Marina, Juliet, Leo};

    public Dictionary<CharacterName, Character> characters;
    [SerializeField] List<Character> charactersList;
    private List<CharacterName> names = new List<CharacterName>();

    public static TouristManager instance;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        instance = this;

        GenerateDictionary();
    }

    private void GenerateDictionary()
    {
        foreach(CharacterName _characterName in System.Enum.GetValues(typeof(CharacterName)))
            names.Add(_characterName);

        characters = new Dictionary<CharacterName, Character>();
        for (int i = 0; i < charactersList.Count; i++)
        {
            characters.Add(names[i], charactersList[i]);
        }
    }
    
    public Character GenerateCharacter()
    {
        if (names.Count < 1) return null;
        int _nameIndex = Random.Range(0, names.Count);

        CharacterName _name = names[_nameIndex];
        names.RemoveAt(_nameIndex);

        return characters[_name];
    }
}
