using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Keyboard keyboardInput = Keyboard.current;
        Mouse mouseInput = Mouse.current;
        if (keyboardInput != null && mouseInput != null)
        {
            if (keyboardInput.eKey.wasPressedThisFrame)
            {
                Vector2 mousePosition = mouseInput.position.ReadValue();

                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 2f))
                {
                    print("Interacted with " + hit.transform.name + " from " + hit.distance + "m.");


                }
            }
        }
    }
}
