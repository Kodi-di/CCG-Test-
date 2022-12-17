using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Field))]
public class CardCollection : MonoBehaviour
{
    [SerializeField] private List<RectTransform> _rects = new();
    [SerializeField] private List<Card> _cards = new();

    private Field _field;

    public event UnityAction OnListChanged;

    public int Count => _rects.Count;
    public List<RectTransform> Rects => _rects;
    public List<Card> Cards => _cards;

    private void Awake()
    {
        _field = GetComponent<Field>();
        _field.OnHandFilled += FilledCardList;
    }

    private void OnDisable()
    {
        _field.OnHandFilled -= FilledCardList;
    }

    private void FilledCardList(List<RectTransform> rects)
    {
        if (rects.Count == 0)
        {
            return;
        }
        _rects = rects;

        foreach(var rect in _rects)
        {
            _cards.Add(rect.GetComponent<Card>());
        }

        OnListChanged?.Invoke();
        Subscription();
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
        _rects.Add(card.GetComponent<RectTransform>());
        card.transform.SetAsLastSibling();
        OnListChanged?.Invoke();
    }

    public void AddCard(RectTransform rect)
    {
        if(rect.TryGetComponent(out Card card))
        {
            _rects.Add(rect);
            _cards.Add(card);
            rect.transform.SetAsLastSibling();
            OnListChanged?.Invoke();
        }
        else
        {
            throw new Exception("The object must contain 'Card'");
        }
    }

    private void Subscription()
    {
        foreach(var card in _cards)
        {
            card.OnHealthZero += DeleteCard;
        }
    }

    private void DeleteCard(Card card)
    {
        _rects.RemoveAt(_cards.FindIndex(x => x.Equals(card)));
        _cards.Remove(card);
        Destroy(card.gameObject);
        OnListChanged?.Invoke();
    }

    public void RemoveCard(Card card)
    {
        _rects.RemoveAt(_cards.FindIndex(x => x.Equals(card)));
        _cards.Remove(card);
        OnListChanged?.Invoke();
    }

    public void RemoveCard(RectTransform rect)
    {
        if (rect.TryGetComponent(out Card card))
        {
            _rects.Remove(rect);
            _cards.Add(card);
            OnListChanged?.Invoke();
        }
        else
        {
            throw new Exception("The object must contain 'Card'");
        }
    }

}
