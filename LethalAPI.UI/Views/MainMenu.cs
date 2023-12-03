using LethalAPI.UI.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LethalAPI.UI.Views
{
    public class MainMenu : IDisposable
    {
        public static MainMenu Instance { get; private set; }

        public GameObject MainMenuContainer { get; }

        public Transform MainMenuButtons { get; }
        public Transform SettingsButton { get; }
        
        public Transform SettingsPanel { get; }
        public Transform SetToDefaultButton { get; }
        
        public bool IsValid => Instance != null && MainMenuContainer != null && MainMenuButtons != null;

        public MainMenu()
        {
            Instance = this;
            
            MainMenuContainer = GameObject.Find("MenuContainer");
            MainMenuButtons = MainMenuContainer.transform.Find("MainButtons");
            SettingsButton = MainMenuButtons.transform.Find("SettingsButton");
            SettingsPanel = MainMenuContainer.transform.Find("SettingsPanel");
            SetToDefaultButton = SettingsPanel.transform.Find("SetToDefault");
            
            OnMainMenuOpen?.Invoke(this);
        }
        public void Dispose()
        {
            Instance = null;
            OnMainMenuClose?.Invoke();
        }

        #region Handle opening and closing main menu events
        public delegate void OnMainMenuOpenHandle(MainMenu menu);
        public static event OnMainMenuOpenHandle OnMainMenuOpen;

        public delegate void OnMainMenuCloseHandle();
        public static event OnMainMenuCloseHandle OnMainMenuClose;

        public static IEnumerator WaitForMainMenu()
        {
            while (Instance == null) yield return null;
        }
        #endregion
        
        public MenuButton AddMainMenuButton(string id, string text, ushort index, Vector3 position, Action onClick)
        {
            if (!IsValid || SettingsButton == null) return null;
            
            MenuButton button = new MenuButton(SettingsButton.gameObject, id, text);
            button.OnClick += onClick;
            button.SetSiblingIndex(index);
            button.SetPosition(position);
            return button;
        }
        public MenuButton AddSettingsButton(string id, string text, ushort index, Vector3 position, Action onClick)
        {
            if (!IsValid || SetToDefaultButton == null) return null;

            MenuButton button = new MenuButton(SetToDefaultButton.gameObject, id, text);
            button.OnClick += onClick;
            button.SetSiblingIndex(index);
            button.SetPosition(position);
            return button;
        }
    }
}
