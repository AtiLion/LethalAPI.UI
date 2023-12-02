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

        public GameObject MainMenuContainer => GameObject.Find("MenuContainer");
        public GameObject MainMenuButtons => GameObject.Find("MainButtons");
        public GameObject SettingsPanel => GameObject.Find("SettingsPanel");
        public bool IsValid => Instance != null && MainMenuContainer != null && MainMenuButtons != null;

        public MainMenu()
        {
            Instance = this;
            OnMainMenuOpen(this);
        }
        public void Dispose()
        {
            Instance = null;
            OnMainMenuClose();
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

        public void AddMenuButton(MainMenuButton button, ushort position)
        {
            if (!IsValid || button == null) return;

            button.SetSiblingIndex(position);
        }
        public void AddMenuButton(string id, string text, ushort position, Action onClick)
        {
            MainMenuButton button = new MainMenuButton(this, id, text);
            button.OnClick += onClick;
            AddMenuButton(button, position);
        }
    }
}
