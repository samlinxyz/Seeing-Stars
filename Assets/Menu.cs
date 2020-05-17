using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    [SerializeField]
    private Movement movement = null;
    [SerializeField]
    private GameObject menu = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                movement.enabled = true;
                LockMouse(true);
            }
            else
            {
                menu.SetActive(true);
                movement.enabled = false;
                LockMouse(false);
            }
        }
    }

    public void LockMouse(bool locked)
    {
        Cursor.visible = locked ? false : true;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
