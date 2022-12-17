using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] private Stat _manaStat;
    [SerializeField] private Stat _attackStat;
    [SerializeField] private Stat _healthStat;
    [SerializeField] private int _startHealth;
    [SerializeField] private int _startMana;
    [SerializeField] private int _startAttack;

    public event UnityAction<Card> OnHealthZero;

    public int Health => _healthStat.Value;
    public int Attack => _attackStat.Value;
    public int Mana => _manaStat.Value;

    private void Start()
    {
        _manaStat.Init(_startMana);
        _attackStat.Init(_startAttack);
        _healthStat.Init(_startHealth);
        _healthStat.OnValueZero += CardDying;
    }

    private void OnDestroy()
    {
        _healthStat.OnValueZero -= CardDying;
    }

    public void ChangedMana(int value)
    {
        _manaStat.NewValue(value);
    }

    public void ChangedAttack(int value)
    {
        _attackStat.NewValue(value);
    }

    public void ChangedHealth(int value)
    {
        _healthStat.NewValue(value);
    }

    private void CardDying()
    {
        OnHealthZero?.Invoke(this);
    }

    public override bool Equals(object obj)
    {
        return obj is Card card &&
               base.Equals(obj) &&
               name == card.name &&
               EqualityComparer<GameObject>.Default.Equals(gameObject, card.gameObject) &&
               Health == card.Health &&
               Attack == card.Attack &&
               Mana == card.Mana;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), name, gameObject, Health, Attack, Mana);
    }

}
