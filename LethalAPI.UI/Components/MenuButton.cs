using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LethalAPI.UI.Components
{
    public class MenuButton
    {
        public GameObject Root { get; private set; }
        public Button ButtonComponent { get; private set; }
        public TextMeshProUGUI TextComponent { get; private set; }

        public event Action OnClick;

        public MenuButton(GameObject sampleButton, string id, string text)
        {
            if (sampleButton == null) throw new Exception("Sample button is null");
            
            GameObject root = GameObject.Instantiate(sampleButton, sampleButton.transform.parent);
            if (root == null) throw new Exception("Could not instantiate sample " + sampleButton.name);

            Button buttonComponent = root.GetComponent<Button>();
            if (buttonComponent == null)
            {
                GameObject.Destroy(root);
                throw new Exception("Could not find Button for " + sampleButton.name);
            }

            GameObject textGameObject = root.transform.GetChild(1)?.gameObject;
            if (textGameObject == null)
            {
                GameObject.Destroy(root);
                throw new Exception("Could not find text child for " + sampleButton.name);
            }

            TextMeshProUGUI textComponent = textGameObject.GetComponent<TextMeshProUGUI>();
            if (textComponent == null)
            {
                GameObject.Destroy(root);
                throw new Exception("Could not find TextMeshProUGUI for " + sampleButton.name);
            }

            root.name = id;
            textComponent.text = text ?? "> Sample";
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
