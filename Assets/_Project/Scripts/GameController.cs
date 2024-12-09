using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public event Action Win;
    public event Action Lose;

    [SerializeField] private RectTransform context;
    [SerializeField] private CardsSO datas;
    [SerializeField] private DragInDrop dragInDrop;
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

    [Header("Stats")]
    [Space(5)]
    [SerializeField] private float timeLimit = 60f;
    [SerializeField] private int score = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private float minDistanceToApply;
    [SerializeField] private List<CardType> keys = new();

    private CardPool _prefabPool;
    private Dictionary<CardType, Color> _recipes = new();
    private Dictionary<CardType, Color> _allRecipes = new();
    private List<ColorElement> _colorElements = new();

    private Coroutine _coroutine;
    private float _timer;
    public void StartGame()
    {
        CreateAllColors();
        _recipes.Clear();
        _prefabPool = new CardPool(cardPrefab, context, datas, 100);
        _prefabPool.Prewarm(cardPrefab, 50);

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
        dragInDrop.EndDragEvent += CheckRightCard;
    }

    private void CreateAllColors()
    {
        _allRecipes.Add(CardType.Red, Color.red);
        _allRecipes.Add(CardType.Green, Color.green);
        _allRecipes.Add(CardType.Blue, Color.blue);
        _allRecipes.Add(CardType.Yellow, Color.yellow);
        _allRecipes.Add(CardType.Magenta, Color.magenta);
        _allRecipes.Add(CardType.White, Color.white);
        _allRecipes.Add(CardType.Cyan, Color.cyan);
        _allRecipes.Add(CardType.Gray, Color.gray);
        _allRecipes.Add(CardType.Grey, Color.grey);
        _allRecipes.Add(CardType.Black, Color.black);
    }

    private void GenerateRecipe(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var randomIndex = Random.Range(0, keys.Count);
            foreach (var recipe in _allRecipes)
            {
                if (keys[randomIndex].ToString().Contains(recipe.Key.ToString()) && !_recipes.ContainsKey(recipe.Key))
                {
                    _recipes.Add(recipe.Key, recipe.Value);
                }
            }
        }


    }

    public void Tick()
    {
        _timer -= Time.deltaTime;
        timer.text = $"Timer: {_timer.ToString("00")}";
        if (_timer <= 0)
        {
            EndGame();
        }

        if (_recipes.Count == 0)
        {
            WinGame();
        }
    }

    private IEnumerator SpawnIngredient()
    {
        var index = Random.Range(0, spawnPoints.Length);
        _prefabPool.TryGet(cardPrefab, spawnPoints[index].anchoredPosition, out var instance);
        yield return new WaitForSeconds(Random.Range(1f, 2f) / level);
    }

    public void CheckRightCard(Card card)
    {
        var distance = Vector2.Distance(card.GetComponent<RectTransform>().position, pot.position);

        if (distance < minDistanceToApply)
        {
            if (_recipes.ContainsKey(card.GetTypeCard()))
            {
                score++;
                scoreText.text = $"Score: {score}";
                Debug.Log("Правильний інгредієнт! Ваш рахунок: " + score);
                _recipes.Remove(card.GetTypeCard());
                foreach (var element in _colorElements)
                {
                    if (element.GetTypeCard() == card.GetTypeCard())
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
        if (_coroutine == null) return;
        StopCoroutine(_coroutine);
        Lose?.Invoke();
    }

    public void WinGame()
    {
        Debug.Log("WIN");
        level++;
        PlayerPrefs.SetInt("level", level);
        if (_coroutine == null) return;
        StopCoroutine(_coroutine);
        Win?.Invoke();
    }
}
