using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    public Inventory inventory;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    private Player player;

    private void Awake(){
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer(Player player){
        this.player = player;
    }

    public void SetInventory(Inventory inventory){
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e){
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems(){
        foreach (Transform child in itemSlotContainer){
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 110f;
        foreach (Item item in inventory.GetItemList()) {
            if (item.amount > 0){
                RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);

                itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                //Use Item
                inventory.UseItem(item);
                };

                itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize + 200);
                Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
                image.sprite = item.GetSprite();

                TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
                if (item.amount > 1){
                    uiText.SetText(item.amount.ToString());
                } else {
                    uiText.SetText("");
                }

                if (item.amount <= 1){
                    Destroy(itemSlotRectTransform.Find("backgroundText").GetComponent<Image>());
                }

                x++;
                if (x > 4){
                    x = 0;
                    y++;
                }
            }
        }
    }
}
