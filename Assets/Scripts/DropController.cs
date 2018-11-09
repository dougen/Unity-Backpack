using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Color hightLightColor;
    private Color hideColor;
    private Image image;

    private void Start()
    {
        hightLightColor = new Color(1f, 1f, 1f, 0.2f);
        hideColor = new Color(1f, 1f, 1f, 0f);
        image = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject item = eventData.pointerDrag;
        item.GetComponent<DragController>().PutItem(transform as RectTransform);
        HideColor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            image.color = hightLightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            HideColor();
        }
    }

    public void HideColor()
    {
        image.color = hideColor;
    }
}
