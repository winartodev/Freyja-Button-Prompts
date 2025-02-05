using Freyja.ButtonPrompts.Data;
using Freyja.DataAsset;
using Freyja.InputSystem;

using UnityEngine;

namespace Freyja.ButtonPrompts.DataAsset
{
    [CreateAssetMenu(menuName = "Freyja/UI/Button Prompt", fileName = "ButtonPromptData_")]
    public class ButtonPromptDataAsset : DataAsset<ButtonPromptData>
    {
        #region Methods

        public ButtonPromptData.Prompt GetPrompt(InputDeviceType inputDeviceType)
        {
            if (inputDeviceType == InputDeviceType.None)
            {
                DebugLog.DebugLog.Show.LogWarning(this, "Unsupported input device type: None");
                return null;
            }

            if (Value.Prompts == null || Value.Prompts.Count == 0)
            {
                DebugLog.DebugLog.Show.LogError(this, "Prompt list is null or empty.");
                return null;
            }

            var prompt = Value.Prompts.Find(prompt => prompt.InputDeviceType == inputDeviceType);
            if (prompt == null)
            {
                DebugLog.DebugLog.Show.LogWarning(this, $"Prompt not found for device type: {inputDeviceType}");
                return null;
            }

            return prompt;
        }

        #endregion
    }
}