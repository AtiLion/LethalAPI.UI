using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LethalAPI.UI.Views;
using LethalAPI.UI.Exceptions;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LethalAPI.UI.Components
{
    public class MainMenuButton
    {
        private const string SampleButtonId = "SettingsButton";

        private MainMenu _mainMenu;

        public GameObject Root { get; private set; }
        public Button ButtonComponent { get; private set; }
        public TextMeshProUGUI TextComponent { get; private set; }

        public event Action OnClick;

        public MainMenuButton(MainMenu menu, string id, string text)
        {
            _mainMenu = menu;

            GameObject sample = GameObject.Find(SampleButtonId);
            if (sample == null) throw new SampleNotFoundException(SampleButtonId);

            GameObject root = GameObject.Instantiate(sample, sample.transform.parent);
            if (root == null) throw new Exception("Could not instantiate sample " + SampleButtonId);

            Button buttonComponent = root.GetComponent<Button>();
            if (buttonComponent == null)
            {
                GameObject.Destroy(root);
                throw new Exception("Could not find Button for " + SampleButtonId);
            }

            GameObject textGameObject = root.transform.GetChild(1)?.gameObject;
            if (textGameObject == null)
            {
                GameObject.Destroy(root);
                throw new Exception("Could not find text child for " + SampleButtonId);
            }

            TextMeshProUGUI textComponent = textGameObject.GetComponent<TextMeshProUGUI>();
            if (textComponent == null)
            {
                GameObject.Destroy(root);
                throw new Exception("Could not find TextMeshProUGUI for " + SampleButtonId);
            }

            root.name = id;
            textComponent.text = "> " + text ?? "> Sample";
            buttonComponent.onClick = new Button.ButtonClickedEvent();
            buttonComponent.onClick.AddListener(() => OnClick?.Invoke());

            Root = root;
            ButtonComponent = buttonComponent;
            TextComponent = textComponent;
        }

        public void SetSiblingIndex(ushort position) => Root.transform.SetSiblingIndex(position);
        public void SetPosition(Vector3 localPosition) => Root.transform.localPosition = localPosition;
    }
}
