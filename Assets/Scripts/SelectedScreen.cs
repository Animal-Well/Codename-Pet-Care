using NUnit.Framework;
using System;
using UnityEngine;

public class SelectedScreen : MonoBehaviour
{
    public static UI_Manager manager = UI_Manager.Instance_UI_Manager;
    [Serializable]
    public class SelectableScreens
    {
        public string name;
        public GameObject screen;
        public Transform button;
    }
    public SelectableScreens[] screens;
    private void Awake()
    {
        manager.selectedScreen = this;
    }
    public void OnSelected(string newSelection)
    {
        for(int i = 0; i < screens.Length; i++)
        {
            if (screens[i].name != newSelection)
            {
                screens[i].screen.SetActive(false);
            }
            else
            {
                transform.position = screens[i].button.position;
                screens[i].screen.SetActive(true);
            }
        }
    }
}
