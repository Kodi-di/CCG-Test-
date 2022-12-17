using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MouseInteractions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private int _siblingPos = 0;
    private Camera _camera;
    private Vector3 _position;
    private Vector3 _offset;
    private Transform _defaultParent;
    private Image _image;

    private void Start()
    {
        _camera = Camera.allCameras[0];
        _image = GetComponent<Image>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - _camera.ScreenToWorldPoint(eventData.position);

        _defaultParent = transform.parent;
        transform.SetParent(_defaultParent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetAsLastSibling();
        _siblingPos = transform.GetSiblingIndex();

        _defaultParent.GetComponent<CardCollection>().RemoveCard(GetComponent<Card>());

        _image.color = Color.green;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _position = _camera.ScreenToWorldPoint(eventData.position);
        transform.position = _position + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_defaultParent);

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        _defaultParent.GetComponent<CardCollection>().AddCard(GetComponent<Card>());

        _image.color = Color.clear;

    }

    private void OnMouseEnter()
    {
        _siblingPos = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    private void OnMouseExit()
    {
        transform.SetSiblingIndex(_siblingPos);
    }

    public void ChangeParent(Field field)
    {
        _defaultParent = field.transform;
    }

}
