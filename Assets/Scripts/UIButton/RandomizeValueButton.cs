using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RandomizeValueButton : MonoBehaviour
{
    [SerializeField] private Button _randomizeButton;
    [SerializeField] private CardCollection _cards;

    private int _index = 0;
    private int _count = 0;

    private void Start()
    {
        _randomizeButton = GetComponent<Button>();
        _randomizeButton.onClick.AddListener(RandomizeValue);
        _cards.OnListChanged += RecalculateCount;
    }

    private void OnDisable()
    {
        _cards.OnListChanged -= RecalculateCount;
    }

    private void RecalculateCount()
    {
        _count = _cards.Count;
    }

    private void RandomizeValue()
    {

        if (_count == 0)
        {
            return;
        }

        if (_index >= _count)
        {
            _index = 0;
        }

        switch (Random.Range(0, 3))
        {
            case 0:
                {
                    _cards.Cards[_index].ChangedHealth(Random.Range(-2, 9));
                    break;
                }
            case 1:
                {
                    _cards.Cards[_index].ChangedMana(Random.Range(-2, 9));
                    break;
                }
            case 2:
                {
                    _cards.Cards[_index].ChangedAttack(Random.Range(-2, 9));
                    break;
                }
            default:
                {
                    break;
                }
        }

        _index++;
    }
}
