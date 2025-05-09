using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PuzzleGenerator : MonoBehaviour
{
    public static PuzzleGenerator Instance;

    private Dictionary<string, Color> _colours = new Dictionary<string, Color>()
    {
        { "Crimson", new Color(0.86f, 0.08f, 0.24f) },
        { "Azure", new Color(0.0f, 0.5f, 1.0f) },
        { "Olive", new Color(0.5f, 0.5f, 0.0f) },
        { "Teal", new Color(0.0f, 0.5f, 0.5f) },
        { "Magenta", Color.magenta },
        { "Cyan", Color.cyan },
        { "Scarlet", new Color(1.0f, 0.14f, 0.0f) },
        { "Amber", new Color(1.0f, 0.75f, 0.0f) },
        { "Ivory", new Color(1.0f, 1.0f, 0.94f) },
        { "Indigo", new Color(0.29f, 0.0f, 0.51f) },
        { "Violet", new Color(0.56f, 0.0f, 1.0f) },
        { "Turquoise", new Color(0.25f, 0.88f, 0.82f) },
        { "Mint", new Color(0.6f, 1.0f, 0.6f) },
        { "Salmon", new Color(0.98f, 0.5f, 0.45f) },
        { "Peach", new Color(1.0f, 0.9f, 0.71f) },
        { "Beige", new Color(0.96f, 0.96f, 0.86f) },
        { "Plum", new Color(0.56f, 0.27f, 0.52f) },
        { "Coral", new Color(1.0f, 0.5f, 0.31f) },
        { "Charcoal", new Color(0.21f, 0.27f, 0.31f) },
        { "Navy", new Color(0.0f, 0.0f, 0.5f) },
        { "Lavender", new Color(0.9f, 0.9f, 0.98f) },
        { "Mauve", new Color(0.88f, 0.69f, 1.0f) },
        { "Chestnut", new Color(0.58f, 0.27f, 0.21f) },
        { "Emerald", new Color(0.31f, 0.78f, 0.47f) },
        { "Ruby", new Color(0.88f, 0.07f, 0.37f) },
        { "Sapphire", new Color(0.06f, 0.32f, 0.73f) },
        { "Topaz", new Color(1.0f, 0.78f, 0.36f) },
        { "Onyx", new Color(0.06f, 0.06f, 0.06f) },
        { "Obsidian", new Color(0.18f, 0.18f, 0.18f) },
        { "Maroon", new Color(0.5f, 0.0f, 0.0f) },
        { "Rose", new Color(1.0f, 0.0f, 0.5f) },
        { "Blush", new Color(0.87f, 0.36f, 0.51f) },
        { "Fuchsia", new Color(1.0f, 0.0f, 1.0f) },
        { "Periwinkle", new Color(0.8f, 0.8f, 1.0f) },
        { "Canary", new Color(1.0f, 1.0f, 0.6f) },
        { "Lemon", new Color(1.0f, 1.0f, 0.0f) },
        { "Pistachio", new Color(0.76f, 0.87f, 0.58f) },
        { "Celadon", new Color(0.67f, 0.88f, 0.69f) },
        { "Burgundy", new Color(0.5f, 0.0f, 0.13f) },
        { "Mustard", new Color(1.0f, 0.86f, 0.35f) },
        { "Bronze", new Color(0.8f, 0.5f, 0.2f) },
        { "Silver", new Color(0.75f, 0.75f, 0.75f) },
        { "Gold", new Color(1.0f, 0.84f, 0.0f) },
        { "Steel", new Color(0.6f, 0.6f, 0.6f) },
        { "Smoke", new Color(0.45f, 0.45f, 0.45f) },
        { "Sand", new Color(0.76f, 0.7f, 0.5f) },
        { "Clay", new Color(0.89f, 0.55f, 0.4f) },
        { "Mahogany", new Color(0.75f, 0.25f, 0.0f) },
        { "Tan", new Color(0.82f, 0.71f, 0.55f) },
        { "Taupe", new Color(0.72f, 0.6f, 0.5f) },
        { "Lilac", new Color(0.78f, 0.64f, 0.78f) },
        { "Denim", new Color(0.08f, 0.38f, 0.74f) },
        { "Slate", new Color(0.44f, 0.5f, 0.56f) },
        { "Ink", new Color(0.09f, 0.09f, 0.18f) },
        { "Bone", new Color(0.95f, 0.92f, 0.84f) },
        { "Cream", new Color(1.0f, 0.99f, 0.82f) },
        { "Snow", new Color(1.0f, 0.98f, 0.98f) },
        { "Ash", new Color(0.7f, 0.75f, 0.71f) },
        { "Flint", new Color(0.41f, 0.41f, 0.41f) },
        { "Graphite", new Color(0.2f, 0.22f, 0.25f) },
        { "Rust", new Color(0.72f, 0.25f, 0.05f) },
        { "Brick", new Color(0.6f, 0.13f, 0.13f) },
        { "Pearl", new Color(0.94f, 0.92f, 0.84f) },
        { "Champagne", new Color(0.97f, 0.91f, 0.81f) },
    };

    private List<string> _titles = new List<string>()
    {
        "Mr.", "Ms.", "Mrs.", "Dr.", "Lord", "Professor", "Sir", "Dame", "Lady", "Honourable", "Chancellor",
        "President", "Prime Minister"
    };

    private List<Character> _characters = new List<Character>();

    [SerializeField] private CharacterButton _characterPrefab;
    [SerializeField] private GridLayoutGroup _grid;

    private int _currentLevel = 4;

    [SerializeField] private Canvas wrongCanvas;
    [SerializeField] private Canvas correctCanvas;
    [SerializeField] private Canvas winCanvas;

    public void ReloadLevel()
    {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        wrongCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.7f);
        wrongCanvas.gameObject.SetActive(false);
        GeneratePuzzle(_currentLevel);
    }

    public void NextLevel()
    {
        if(_currentLevel < 16)
            StartCoroutine(NextCoroutine());
        else
            winCanvas.gameObject.SetActive(true);
    }

    private IEnumerator NextCoroutine()
    {
        correctCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.7f);
        correctCanvas.gameObject.SetActive(false);
        _currentLevel += 2;
        GeneratePuzzle(_currentLevel);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GeneratePuzzle(_currentLevel);
        // foreach (var character in _characters)
        // {
        //     print($"{character.Name}: {character.Statement}");
        //     print($"{character.Name}: {character.Statement} ({character.IsTellingTheTruth})");
        // }
    }

    private void GeneratePuzzle(int count = 2, int truths = 1)
    {
        _characters.Clear();
        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            Destroy(_grid.transform.GetChild(i).gameObject);
        }

        var availableColours = new Dictionary<string, Color>(_colours);

        // Generate Random Characters
        for (int i = 0; i < count; i++)
        {
            var item = availableColours.ElementAt(Random.Range(0, availableColours.Count));

            var newCharacter = new Character
            {
                Name = $"{_titles.ElementAt(Random.Range(0, _titles.Count))} {item.Key}",
                Color = item.Value
            };

            _characters.Add(newCharacter);
            availableColours.Remove(item.Key);
        }

        // Assign first X characters to tell truths
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].IsTellingTheTruth = i < truths;
        }

        // Shuffle characters
        _characters = _characters.OrderBy(c => UnityEngine.Random.value).ToList();

        GenerateStatements(_characters);

        foreach (var character in _characters)
        {
            var instance = Instantiate(_characterPrefab, _grid.transform);
            instance.Init(character);
        }
    }

    void GenerateStatements(List<Character> chars)
    {
        foreach (var c in chars)
        {
            Character target;
            do
            {
                target = chars[UnityEngine.Random.Range(0, chars.Count)];
            } while (target == c);

            bool statementTruth = target.IsTellingTheTruth;

            string statement;
            if (c.IsTellingTheTruth)
            {
                statement = statementTruth
                    ? $"{target.Name} is telling the truth."
                    : $"{target.Name} is lying.";
            }
            else
            {
                // Liar says the opposite of the truth
                statement = statementTruth
                    ? $"{target.Name} is lying."
                    : $"{target.Name} is telling the truth.";
            }

            c.Statement = statement;
        }
    }
}