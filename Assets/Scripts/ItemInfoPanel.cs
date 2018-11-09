using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Font font;

    private void Start()
    {

    }

    public void SetInfoPanel(ItemInfo info)
    {
		var childs = GetComponentsInChildren<Text>(true);
		if (childs.Length > 0)
		{
			foreach (var child in childs)
			{
				Destroy(child.gameObject);
			}
		}
        AddText("ItemName", string.Format("<color=cyan><b>{0}</b></color>", info.name));
        AddText("ItemType", string.Format("物品类型：{0}", info.type));
		AddText("ItemPro", string.Format("{0}: +{1}",info.prop,info.value));
    }

    private void AddText(string objectName, string text)
    {
        var obj = new GameObject(objectName);
        obj.transform.SetParent(transform);
        var typeText = obj.AddComponent(typeof(Text)) as Text;
        typeText.font = font;
        typeText.text = text;
    }


}
