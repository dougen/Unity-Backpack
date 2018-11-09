using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 物品抓取控制器，实现用鼠标拖动物品功能。
/// </summary>
public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public RectTransform canvas;
    // 储存物品最放的最近一个物品栏
    public RectTransform lastSlot;

    // 这个参数用来调整鼠标点击时，鼠标坐标与物品坐标的偏移量
    private Vector3 dragOffset;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        lastSlot = transform.parent as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas);
        canvasGroup.blocksRaycasts = false;
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, eventData.position, null, out worldPos))
        {
            dragOffset = new Vector3(transform.position.x - worldPos.x, transform.position.y - worldPos.y, 0f);
            transform.position = worldPos + dragOffset;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, eventData.position, null, out worldPos))
        {
            transform.position = worldPos + dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 没有对齐格子时，返回原来的背包格子
        if (eventData.pointerEnter == null || eventData.pointerEnter.tag != "Slot")
        {
            PutItem(lastSlot);
        }
    }

    // 将物品放入一个物品栏内
    public void PutItem(RectTransform slot)
    {
        lastSlot = slot;
        transform.SetParent(slot);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }

    // 当有其他物品想要放在自己这格时，双方交换一下位置
    public void OnDrop(PointerEventData eventData)
    {
        // 先让物品栏高亮效果消失
        lastSlot.GetComponent<DropController>().HideColor();
        var dc = eventData.pointerDrag.GetComponent<DragController>();
        var tempSlot = dc.lastSlot;
        dc.PutItem(lastSlot);
        PutItem(tempSlot);
    }
}
