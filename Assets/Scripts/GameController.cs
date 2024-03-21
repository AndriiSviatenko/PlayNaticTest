using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Text")]
    [Space(5)]
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI scoreText;
    [Header("SpawnSettings")]
    [Space(5)]
    [SerializeField] private RectTransform[] spawnPoints;
    [SerializeField] private RectTransform content;
    [Header("Objects")]
    [Space(5)]
    [SerializeField] private ColorElement prefab;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private RectTransform pot;
    [SerializeField] private DragInDrop dragInDrop;
    [Header("Stats")]
    [Space(5)]
    [SerializeField] private float timeLimit = 60f;
    [SerializeField] private int score = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private float minDistanceToApply;
    [SerializeField] private List<string> keys = new List<string>();
    private Dictionary<string, Color> _recipes = new Dictionary<string, Color>();
    private Dictionary<string, Color> _allRecipes = new Dictionary<string, Color>();
    private List<ColorElement> _colorElements = new List<ColorElement>();
    private Coroutine _coroutine;
    private float _timer;
    private bool _gameOver = false;

    private void Awake()
    {
        CreateAllColors();
        _recipes.Clear();
    }
    private void Start()
    {
        _timer = timeLimit;
        timer.text = $"Timer: {_timer}";
        GenerateRecipe(3 + PlayerPrefs.GetInt("level"));

        foreach (var recipe in _recipes)
        {
            var element = Instantiate(prefab, content);
            element.Init(recipe.Key, recipe.Value);
            _colorElements.Add(element);
        }
        _coroutine = StartCoroutine(SpawnIngredient());
    }

    private void CreateAllColors()
    {
        _allRecipes.Add("red", Color.red);
        _allRecipes.Add("black", Color.black);
        _allRecipes.Add("blue", Color.blue);
        _allRecipes.Add("cyan", Color.cyan);
        _allRecipes.Add("gray", Color.gray);
        _allRecipes.Add("green", Color.green);
        _allRecipes.Add("grey", Color.grey);
        _allRecipes.Add("magenta", Color.magenta);
        _allRecipes.Add("white", Color.white);
        _allRecipes.Add("yellow", Color.yellow);
    }

    private void GenerateRecipe(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var randomIndex = Random.Range(0, keys.Count);
            foreach (var recipe in _allRecipes)
            {
                if (keys[randomIndex].Contains(recipe.Key) && !_recipes.ContainsKey(recipe.Key))
                {
                    _recipes.Add(recipe.Key, recipe.Value);
                }
            }
        }


    }

    private void Update()
    {
        if (!_gameOver)
        {
            _timer -= Time.deltaTime;
            timer.text = $"Timer: {_timer.ToString("00")}";
            if (_timer <= 0)
            {
                EndGame();
            }
        }
        if (_recipes.Count == 0)
        {
            if (_gameOver) return;
            WinGame();
        }
    }

    private IEnumerator SpawnIngredient()
    {

        while (!_gameOver)
        {
            foreach (var recipe in _allRecipes)
            {
                if (_gameOver) yield break;

                var index = Random.Range(0, spawnPoints.Length);
                var card = Instantiate(dragInDrop, spawnPoints[index].position, Quaternion.identity, transform);
                card.GetComponent<Card>().Init(recipe.Key, recipe.Value);
                card.EndDragEvent += CheckRightCard;
                yield return new WaitForSeconds(Random.Range(1f, 2f) / level);
            }
        }
    }

    public void CheckRightCard(DragInDrop card)
    {
        var distance = Vector2.Distance(card.GetComponent<RectTransform>().position, pot.position);
        if (distance < minDistanceToApply)
        {
            if (_recipes.ContainsKey(card.tag))
            {
                score++;
                scoreText.text = $"Score: {score}";
                Debug.Log("Правильний інгредієнт! Ваш рахунок: " + score);
                _recipes.Remove(card.tag);
                foreach (var element in _colorElements)
                {
                    if (element.Tag == card.tag)
                    {
                        Destroy(element.gameObject);
                    }
                }
            }

            else
            {
                Debug.Log("Неправильний інгредієнт! Гра закінчена!");
                EndGame();
            }

        }
        Destroy(card.gameObject);
    }

    public void EndGame()
    {
        _gameOver = true;
        if (_coroutine == null) return;
        StopCoroutine(_coroutine);
    }

    public void WinGame()
    {
        _gameOver = true;
        level++;
        PlayerPrefs.SetInt("level", level);
        if (_coroutine == null) return;
        StopCoroutine(_coroutine);
    }
}
