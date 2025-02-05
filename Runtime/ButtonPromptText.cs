using System.Text.RegularExpressions;

using Freyja.ButtonPrompts.DataAsset;
using Freyja.InputSystem;

using Sirenix.OdinInspector;

using TMPro;

using UnityEngine;

using Log = Freyja.ButtonPrompts.DebugLog.DebugLog;

namespace Freyja.ButtonPrompts
{
    [AddComponentMenu("Freyja/Button Prompts/Button Prompt Text")]
    [RequireComponent(typeof(TextMeshProUGUI), typeof(InputDeviceTypeEvent))]
    public class ButtonPromptText : MonoBehaviour
    {
        #region Constants

        private const string DataAssetGrp = "Data Assets";
        private const string ConfigGrp = "Config";
        private const string UIGrp = "UI";
        private const string TextGrp = "Text";

        private readonly string _promptPattern = $@"<prompt=""([^""]+)"">";

        #endregion

        #region Fields

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(ConfigGrp)]
    #endif
        [SerializeField]
        private InputDeviceTypeEvent m_InputDeviceTypeEvent;

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(DataAssetGrp)]
    #endif
        [SerializeField]
        private ButtonPromptTextDataAsset m_DataAsset;

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(UIGrp)]
    #endif
        [SerializeField]
        private TextMeshProUGUI m_TextMeshProUGUI;

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(TextGrp)]
        [HideLabel]
        [TextArea(4, 10)]
    #endif
        [SerializeField]
        private string m_Text;

        #endregion

        #region Properties

        public string Text
        {
            get => m_Text;
        }

        #endregion

        #region Methods

        private void OnEnable()
        {
            m_InputDeviceTypeEvent.OnDeviceEvent.AddListener(OnDeviceChanged);
        }

        private void OnDisable()
        {
            m_InputDeviceTypeEvent.OnDeviceEvent.RemoveListener(OnDeviceChanged);
        }

        private void OnDeviceChanged(InputDeviceType inputDeviceType)
        {
            var result = m_DataAsset.GetSpriteAsset(inputDeviceType);
            if (result == null)
            {
                Log.Show.LogWarning($"Sprite asset not found for input device type: {inputDeviceType}.");
                return;
            }

            ProcessText(m_Text, result.TMPSpriteAsset.name, inputDeviceType);
        }

        private void ProcessText(string text, string spriteAssetName, InputDeviceType inputDeviceType)
        {
            var result = Regex.Replace(text, _promptPattern, match =>
            {
                var promptName = match.Groups[1].Value;

                var prompt = m_DataAsset.GetPrompt(promptName, inputDeviceType);
                if (prompt == null)
                {
                    Log.Show.LogWarning($"Prompt not found for input device type: {inputDeviceType}, prompt name: {promptName}. ");
                    return string.Empty;
                }

                return $"<sprite=\"{spriteAssetName}\" name=\"{prompt.Icon.name}\">";
            });

            m_TextMeshProUGUI.SetText(result);
        }

        public void SetRawText(string text)
        {
            m_Text = Text;
        }

        private void Reset()
        {
        #if UNITY_EDITOR

            if (m_TextMeshProUGUI == null)
            {
                m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
            }

            if (m_InputDeviceTypeEvent == null)
            {
                m_InputDeviceTypeEvent = GetComponent<InputDeviceTypeEvent>();
            }

        #endif
        }

        #endregion
    }
}