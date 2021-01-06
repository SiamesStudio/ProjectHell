using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristManager : MonoBehaviour
{
    public enum CharacterName {Tom, Eva, Daniel, Marina, Juliet, Leo};
    public enum SkinPart { pelo, pelo1, pelo2, peloCresta, peloRizado, pSphere3, pSphere4, pSphere4_2, rizos, tupe};

    //CHARCACTER NAMES
    public Dictionary<CharacterName, Character> characters;
    [SerializeField] List<Character> charactersList;
    private List<CharacterName> names = new List<CharacterName>();

    //SKINS
    private List<CharacterName> skinNames = new List<CharacterName>();
    public Dictionary<SkinPart, Mesh> skinParts;
    [SerializeField] private Mesh[] skinMeshes;

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

        int _skinId = 0;
        skinParts = new Dictionary<SkinPart, Mesh>();
        foreach (SkinPart _skinPart in System.Enum.GetValues(typeof(SkinPart)))
        {
            skinParts.Add(_skinPart, skinMeshes[_skinId++]);
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
