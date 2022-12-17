using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RandomizeValueButton : MonoBehaviour
{
    [SerializeField] private Button _randomizeButton;
    [SerializeField] private CardCollection _cards;

    private int _index = 1;
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
        _index--;
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
                    _cards.Cards[_index].ChangedHealth(Random.Range(-2, 10));
                    break;
                }
            case 1:
                {
                    _cards.Cards[_index].ChangedMana(Random.Range(-2, 10));
                    break;
                }
            case 2:
                {
                    _cards.Cards[_index].ChangedAttack(Random.Range(-2, 10));
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
