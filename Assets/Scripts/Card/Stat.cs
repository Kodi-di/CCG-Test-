using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Text))]
public class Stat : MonoBehaviour
{
    [SerializeField] private Text _uiValue;
    [SerializeField] private float _changedSpeed = 1;
    [SerializeField] private bool _isHealth = false;

    private int _value = 0;
    private Queue<int> _newValues = new();

    public event UnityAction OnValueZero;

    public int Value => _value;

    private void Awake()
    {
        _uiValue = GetComponent<Text>();
    }

    private void ChangingValue(float value)
    {
        if ((_value < 1)&&(_isHealth))
        {
            OnValueZero?.Invoke();
            return;
        }

        if (value < _value)
        {
            _value = Mathf.CeilToInt(Mathf.Lerp(_value, value, _changedSpeed));
            _uiValue.text = _value.ToString();

        }else if(value > _value)
        {
            _value = Mathf.FloorToInt(Mathf.Lerp(_value, value, _changedSpeed));
            _uiValue.text = _value.ToString();
        }
    }

    public void NewValue(int value)
    {
        if (value != _value)
        {
            _newValues.Enqueue(value);
        }

        DOTween.To(ChangingValue, _value, value, _changedSpeed);

        while(_newValues.Count > 0)
        {
            DOTween.To(ChangingValue, _value, _newValues.Dequeue(), _changedSpeed);
        }

    }

    public void Init(int value)
    {
        if(_value == 0)
        {
            if(value > 0)
            {
                _value = value;
                _uiValue.text = _value.ToString();

            }else
            {
                throw new Exception("Value must be greater than 0");
            }
        }
    }

}
