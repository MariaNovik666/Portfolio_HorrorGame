using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryObject;
    public float distance = 2f;
    public CameraController Player;
    

    public GameObject Crosshair;

    public Slot[] slots;

    public Slot[] equipSlots;

    void Start()
    {
        inventoryObject.SetActive(false);

        foreach(Slot i in slots)
        {
            i.CustomStart();
        }
        foreach (Slot i in equipSlots)
            i.CustomStart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryObject.activeSelf == false)
            {
                inventoryObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameStates.inventoryIsOpen = true;
                Crosshair.SetActive(false);
            }
            else
            {
                inventoryObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameStates.inventoryIsOpen = false;
                Crosshair.SetActive(true);
            }
        }


        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.gameObject.GetComponent<Item>())
            {   
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AddItem(hit.collider.gameObject.GetComponent<Item>());
                }
            }
        }

        foreach (Slot i in slots)
        {
            i.CheckForItem();
        }
        foreach (Slot i in equipSlots)
            i.CheckForItem();
    }
    public int GetItemAmount(int id)
    {
        int num = 0;
        foreach(Slot i in slots)
        {
            if (i.slotsItem)
            {
                Item z = i.slotsItem;
                if (z.itemID == id)
                    num += z.amountInStack;
            }
        }
        return num;
    }

    public void RemoveItemAmount(int id, int amountToRemove)
    {
        foreach(Slot i in slots)
        {
            if (amountToRemove <= 0)
                return;

            if (i.slotsItem)
            {
                Item z = i.slotsItem;
                if(z.itemID == id)
                {
                    int amountThatCanRemoved = z.amountInStack;
                    if(amountThatCanRemoved <= amountToRemove)
                    {
                        Destroy(z.gameObject);
                        amountToRemove -= amountThatCanRemoved;
                    }
                    else
                    {
                        z.amountInStack -= amountToRemove;
                        amountToRemove = 0;
                    }
                }
            }
        }
    }

    public void AddItem(Item itemToBeAdded, Item startingItem = null)
    {

        foreach (Slot i in slots)
        {
            if (i.slotsItem is null)
            {

                itemToBeAdded.transform.parent = i.transform;
                itemToBeAdded.gameObject.SetActive(false);
                i.AddItem(itemToBeAdded);
                return;

            }

        }

    }
    public bool HasItem(int? itemId)
    {
        if (itemId is null)
            return false;


        foreach (var slot in slots)
        {
            if (slot.slotsItem is null)
                continue;
            if (slot.slotsItem.itemID == itemId.Value)
                return true;
        }
        
        return false;
    }
}
