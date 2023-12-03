using BepInEx;
using LethalAPI.UI.Views;
using System;
using LethalAPI.UI.Menus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LethalAPI.UI
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        private const string SceneNameMainMenu = "MainMenu";

        private MainMenu _mainMenu;
        private ModSettings _modSettings;

        private MenuManager _gameMenuManager;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            MainMenu.OnMainMenuOpen += OnMainMenuOpen;

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;

            MainMenu.OnMainMenuOpen -= OnMainMenuOpen;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == SceneNameMainMenu)
            {
                _mainMenu = new MainMenu();
                if (_mainMenu.SettingsPanel != null)
                {
                    _modSettings = new ModSettings(_mainMenu.SettingsPanel);

                    LethalCompanyMenu.OnMenuClose += (LethalCompanyMenu menu) =>
                        _gameMenuManager.EnableUIPanel(_mainMenu.SettingsPanel.gameObject);
                }

                _gameMenuManager = GameObject.FindObjectOfType<MenuManager>();
            }
        }
        private void OnSceneUnloaded(Scene scene)
        {
            if (scene.name == SceneNameMainMenu)
            {
                _mainMenu?.Dispose();
                _modSettings?.Dispose();

                _mainMenu = null;
                _modSettings = null;
            }
        }
        
        private void CreateModSettings(MainMenu menu)
        {
            if (menu.SetToDefaultButton == null) return;

            Vector3 setToDefaultPosition = menu.SetToDefaultButton.transform.localPosition;
            menu.AddSettingsButton("ModSettings", "> Mod Settings", 5, new Vector3(setToDefaultPosition.x, -127, setToDefaultPosition.z), () => {
                if (_modSettings == null || _gameMenuManager == null) return;
                
                _gameMenuManager.DisableUIPanel(_mainMenu.SettingsPanel.gameObject);
                _modSettings.Open();
            });
        }
        private void OnMainMenuOpen(MainMenu menu)
        {
            if (menu == null) return;

            CreateModSettings(menu);
        }
    }
}