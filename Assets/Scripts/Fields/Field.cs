using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class Field : MonoBehaviour, IDropHandler 
{
    public virtual event UnityAction<List<RectTransform>> OnHandFilled;

    public void OnDrop(PointerEventData eventData)
    {
        MouseInteractions card = eventData.pointerDrag.GetComponent<MouseInteractions>();
        card.ChangeParent(this);
    }
}
