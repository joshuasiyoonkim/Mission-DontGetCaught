using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOpenCollectable : MonoBehaviour
{
    public float DistanceOpen = 3;
    private CollectibleItem currentItem;

    void Update()
    {
        RaycastHit hit;
        CollectibleItem newItem = null;

        if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
        {
            newItem = hit.transform.GetComponent<CollectibleItem>();

            if (newItem != null)
            {
                if (newItem != currentItem)
                {
                    ResetPreviousItem();
                    currentItem = newItem;
                    newItem.isLookedAt = true;
                }

                if (Input.GetKeyDown(KeyCode.K))
                {
                    newItem.ShowPickupUI();
                }
            }
            else
            {
                ResetPreviousItem();
            }
        }
        else
        {
            ResetPreviousItem();
        }
    }

    private void ResetPreviousItem()
    {
        if (currentItem != null)
        {
            currentItem.isLookedAt = false;
            currentItem.HidePickupUI();
            currentItem = null;
        }
    }
}
