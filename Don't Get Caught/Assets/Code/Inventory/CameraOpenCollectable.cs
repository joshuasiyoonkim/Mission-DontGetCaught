using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOpenCollectable : MonoBehaviour
{
    public float DistanceOpen = 3;
    private CollectibleItem currentItem;

    void Start()
    {
    }

    void Update()
    {
        RaycastHit hit;
        bool isItemDetected = false;

        if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
        {
            CollectibleItem item = hit.transform.GetComponent<CollectibleItem>();
            if (item != null)
            {
                isItemDetected = true;
                item.isLookedAt = true;
                currentItem = item;

                if (Input.GetKeyDown(KeyCode.K))
                {
                    item.ShowPickupUI();
                }
            }
        }

        if (!isItemDetected && currentItem != null)
        {
            currentItem.isLookedAt = false;
            currentItem = null;
        }
    }
}
