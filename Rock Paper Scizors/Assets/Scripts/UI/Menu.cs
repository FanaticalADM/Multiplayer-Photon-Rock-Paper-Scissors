using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [field: SerializeField] public string MenuName { get; private set; }
    public bool IsOpen;

    public void Open()
    {
        IsOpen = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        IsOpen = false;
        gameObject.SetActive(false);
    }
}
