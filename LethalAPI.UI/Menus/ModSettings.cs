using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalAPI.UI.Menus;

public class ModSettings : LethalCompanyMenu
{
    private readonly string[] IgnoreList = new[] { "Headers", "Confirm", "BackButton", "PleaseConfirmChangesPanel" };
    
    public bool ChangesNotApplied;
    
    public static ModSettings Instance { get; private set; }
    public override GameObject Root { get; }

    public ModSettings(Transform settingsPanel)
    {
        Instance = this;
        ChangesNotApplied = false;
        
        if (settingsPanel == null) throw new Exception("Settings panel is null");

        GameObject root = GameObject.Instantiate(settingsPanel.gameObject, settingsPanel.parent);
        if (root == null) throw new Exception("Could not instantiate settings panel");

        int childCount = root.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = root.transform.GetChild(i);
            SettingsOption option = child.GetComponent<SettingsOption>();
            
            if (!IgnoreList.Contains(child.name)) GameObject.Destroy(child.gameObject);
            else if (option != null) GameObject.DestroyImmediate(option); 
        }

        GameObject displayGameObject = root.transform.Find("Headers/Display")?.gameObject;
        if (displayGameObject == null) throw new Exception("Could not find Display game object");

        TextMeshProUGUI displayComponent = displayGameObject.GetComponent<TextMeshProUGUI>();
        if (displayComponent == null) throw new Exception("Could not find TextMeshProUGUI in Display");

        GameObject backGameObject = root.transform.Find("BackButton")?.gameObject;
        if (backGameObject == null) throw new Exception("Could not find BackButton game object");

        Button backComponent = backGameObject.GetComponent<Button>();
        if (backComponent == null) throw new Exception("Could not find Button component for BackButton");

        GameObject confirmGameObject = root.transform.Find("Confirm")?.gameObject;
        if (confirmGameObject == null) throw new Exception("Could not find Confirm game object");

        Button confirmComponent = confirmGameObject.GetComponent<Button>();
        if (confirmComponent == null) throw new Exception("Could not find Button component for Confirm");

        displayComponent.enableWordWrapping = false;
        displayComponent.text = "MOD SETTINGS";
        confirmComponent.onClick = new Button.ButtonClickedEvent();
        confirmComponent.onClick.AddListener(OnConfirm);
        backComponent.onClick = new Button.ButtonClickedEvent();
        backComponent.onClick.AddListener(OnBack);
        root.name = "ModSettingsPanel";
        Root = root;
    }
    public override void Dispose()
    {
        Instance = null;
    }

    private void OnBack()
    {
        ChangesNotApplied = false;
        Close(); // TODO: Add save confirmation before exit
    }

    private void OnConfirm() // TODO: Add saving settings
    {
        ChangesNotApplied = false;
    }
}