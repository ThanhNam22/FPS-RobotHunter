using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelect(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
