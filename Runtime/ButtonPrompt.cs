using System.Linq;

using Freyja.ButtonPrompts.DataAsset;
using Freyja.InputSystem;

using Sirenix.OdinInspector;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Log = Freyja.ButtonPrompts.DebugLog.DebugLog;

namespace Freyja.ButtonPrompts
{
    [AddComponentMenu("Freyja/Button Prompts/Button Prompt")]
    [RequireComponent(typeof(InputDeviceTypeEvent))]
    public class ButtonPrompt : MonoBehaviour
    {
        #region Constants

        private const string DataAssetGrp = "Data Assets";
        private const string ConfigGrp = "Config";
        private const string UIGrp = "UI";

        #endregion

        #region Fields

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(ConfigGrp)]
        [Required]
    #endif
        [SerializeField]
        private InputDeviceTypeEvent m_InputDeviceTypeEvent = null;

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(DataAssetGrp)]
        [InlineEditor]
        [Required]
        [OnValueChanged(nameof(OnValueChangeButtonPromptDataAsset))]
    #endif
        [SerializeField]
        private ButtonPromptDataAsset m_DataAsset;

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(UIGrp)]
        [Required]
    #endif
        [SerializeField]
        private Image m_Icon;

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(UIGrp)]
        [Required]
    #endif
        [SerializeField]
        private TextMeshProUGUI m_Text;

        #endregion

        #region Properties

        public UnityEvent<InputDeviceType> UpdateButtonPrompt { get; private set; } = new UnityEvent<InputDeviceType>();

        #endregion

        #region Methods

        private void OnEnable()
        {
            m_InputDeviceTypeEvent.OnDeviceEvent.AddListener(OnDeviceChanged);
            UpdateButtonPrompt.AddListener(UpdatePrompt);
        }

        private void OnDisable()
        {
            m_InputDeviceTypeEvent.OnDeviceEvent.RemoveListener(OnDeviceChanged);
            UpdateButtonPrompt.RemoveListener(UpdatePrompt);
        }

        private void OnDeviceChanged(InputDeviceType inputDeviceType)
        {
            UpdatePrompt(inputDeviceType);
        }

        private void UpdatePrompt(InputDeviceType inputDeviceType)
        {
            if (m_DataAsset == null || m_Text == null)
            {
                Log.Show.LogWarning(this, "m_DataAsset or m_Text is null. Cannot proceed with device change.");
                return;
            }

            var prompt = m_DataAsset.GetPrompt(inputDeviceType);
            if (prompt == null)
            {
                Log.Show.LogWarning(this, $"No prompt found for input device type: {inputDeviceType}");
                return;
            }

            m_Icon.sprite = prompt.Icon;

            var promptName = m_DataAsset.Value.Name;
            if (m_Text.text == promptName)
            {
                return;
            }

            m_Text.SetText(promptName);
        }

        private void Reset()
        {
        #if UNITY_EDITOR
            m_InputDeviceTypeEvent = GetComponent<InputDeviceTypeEvent>();
            GetIconComponent();
            GetIconTextComponent();
        #endif
        }

    #if UNITY_EDITOR

        private void OnValueChangeButtonPromptDataAsset()
        {
            if (m_DataAsset == null)
            {
                gameObject.name = $"ButtonPrompt_";
                return;
            }

            var prompt = m_DataAsset.GetPrompt(InputDeviceType.Keyboard);

            if (m_Icon != null && prompt != null)
            {
                m_Icon.sprite = prompt.Icon;
            }

            if (m_Text != null && prompt != null)
            {
                m_Text.text = m_DataAsset.Value.Name;
            }

            gameObject.name = $"ButtonPrompt_{m_DataAsset.Value.Name}";
        }

        private void GetIconComponent()
        {
            if (m_Icon != null)
            {
                return;
            }

            var images = GetComponentsInChildren<Image>().ToList();

            var icon = images.Find(image => image.name == "Icon");
            m_Icon = icon != null ? icon : CreateNewComponent<Image>("Icon", transform);
        }

        private void GetIconTextComponent()
        {
            if (m_Text != null)
            {
                return;
            }

            var texts = GetComponentsInChildren<TextMeshProUGUI>().ToList();

            var text = texts.Find(text => text.name == "Text");
            m_Text = text != null ? text : CreateNewComponent<TextMeshProUGUI>("Text", transform);
        }

        private T CreateNewComponent<T>(string gameObjectName, Transform parent) where T : Component
        {
            Log.Show.LogWarning(this, $"No {typeof(T).Name} component found on {gameObject.name}");
            Log.Show.Log(this, $"Creating {typeof(T).Name} component");

            var newGameObject = new GameObject(gameObjectName);
            newGameObject.transform.SetParent(parent);

            var newRectTransform = newGameObject.AddComponent<RectTransform>();
            newRectTransform.localPosition = Vector3.zero;
            newRectTransform.sizeDelta = new Vector2(100, 100);
            newRectTransform.localScale = Vector3.one;

            newGameObject.AddComponent<T>();

            Log.Show.Log(this, $"Success to create new {typeof(T).Name} component with name {gameObjectName}");

            return newGameObject.GetComponent<T>();
        }

    #endif

        #endregion
    }
}