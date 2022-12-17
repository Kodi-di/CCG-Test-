using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hand : Field
{
    [SerializeField] private GameObject _template;

    public override event UnityAction<List<RectTransform>> OnHandFilled;
    public void Start()
    {
        FillHand();
    }

    private void FillHand()
    {
        int count = Random.Range(4, 6);
        List<RectTransform> list = new();

        for(int i = 0; i < count; i++)
        {
            list.Add(Instantiate(_template, transform).GetComponent<RectTransform>());
        }

        OnHandFilled?.Invoke(list);
    }
}
