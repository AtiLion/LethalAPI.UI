using BepInEx;
using LethalAPI.UI.Views;
using System;
using UnityEngine.SceneManagement;

namespace LethalAPI.UI
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        private const string SceneNameMainMenu = "MainMenu";

        private MainMenu _mainMenu;

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
            if (scene == null) return;

            if (scene.name == SceneNameMainMenu) _mainMenu = new MainMenu();
        }
        private void OnSceneUnloaded(Scene scene)
        {
            if (scene == null) return;

            if (scene.name == SceneNameMainMenu)
            {
                _mainMenu.Dispose();
                _mainMenu = null;
            }
        }
        
        private void CreateModSettings(MainMenu menu)
        {
            menu.AddMenuButton("test", "Test", 2, () => { Console.WriteLine("This is a test"); });
        }
        private void OnMainMenuOpen(MainMenu menu)
        {
            if (menu == null) return;

            CreateModSettings(menu);
        }
    }
}