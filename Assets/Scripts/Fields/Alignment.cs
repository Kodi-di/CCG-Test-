using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CardCollection))]
public class Alignment : MonoBehaviour
{
    [SerializeField] private AnimationCurve _arcOfCards = new(new Keyframe(0, 0), new Keyframe(0.5f, 2f), new Keyframe(1, 0));
    [SerializeField] private float _angle = 10;
    [SerializeField] private float _cardIndent = 10;
    [SerializeField] private float _bottomSpread = 9;
    [SerializeField] private float _arcSpread = 30;
    [SerializeField] private float _durationRotation = 1;
    [SerializeField] private float _moveRotation = 1;

    private CardCollection _listOfCards;
    

    private void Awake()
    {
        _listOfCards = GetComponent<CardCollection>();
        _listOfCards.OnListChanged += Aligning;
    }

    private void OnDisable()
    {
        _listOfCards.OnListChanged -= Aligning;
    }

    private void Aligning()
    {
        if(_listOfCards.Count == 0)
        {
            return;
        }

        int iterator = 0;

        foreach(var position in CalculatingPosition(_listOfCards.Rects))
        {
            if(_listOfCards.Rects[iterator] == null)
            {
                continue;
            }
            _listOfCards.Rects[iterator].DOLocalRotate(position.Item1, _durationRotation, RotateMode.Fast);
            _listOfCards.Rects[iterator].DOLocalMove(position.Item2, _moveRotation, true);
            iterator++;
        }
    }

    private List<(Vector3,Vector3)> CalculatingPosition(List<RectTransform> _cards)
    {
        List<(Vector3, Vector3)> position = new();

        if(_cards.Count == 0)
        {
            return null;
        }

        if(_cards.Count == 1)
        {
            position.Add((new(0, 0, 0), new(0, -9, 0)));

            return position;
        }

        float indent = 1 / (float)(_cards.Count - 1);
        float currentIndent = 0;
        float currentAngle = _angle / (_cards.Count - 1);

        float startXPos = ((_cards[0].sizeDelta.x * (_cards.Count - 1)) + (_cardIndent * (_cards.Count - 1))) / 2;
        float startZRot = ((_cards.Count - 1) * currentAngle) / 2;

        Vector3 cardPosition = new( -startXPos, _arcOfCards.Evaluate(currentIndent) * _arcSpread - _bottomSpread , 0);
        Vector3 cardRotation = new(0,0,startZRot);

        foreach(var card in _cards)
        {
            position.Add((cardRotation, cardPosition));
            currentIndent += indent;
            cardPosition.x += _cardIndent + _cards[0].sizeDelta.x;

            if ((currentIndent >= 1)&&( currentIndent > (1 - indent)))
            {
                currentIndent = 1;
            }

            cardPosition.y = (_arcOfCards.Evaluate(currentIndent) * _arcSpread - _bottomSpread);
            cardRotation.z -= currentAngle;
        }

        return position;
    }

}
