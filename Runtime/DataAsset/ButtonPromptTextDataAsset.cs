using Freyja.ButtonPrompts.Data;
using Freyja.DataAsset;
using Freyja.InputSystem;

using UnityEngine;

namespace Freyja.ButtonPrompts.DataAsset
{
    [CreateAssetMenu(menuName = "Freyja/UI/Button Prompt Text", fileName = "ButtonPromptTextData_")]
    public class ButtonPromptTextDataAsset : DataAsset<ButtonPromptTextData>
    {
        #region Methods

        public ButtonPromptTextData.SpriteAssetData GetSpriteAsset(InputDeviceType inputDeviceType)
        {
            if (inputDeviceType == InputDeviceType.None)
            {
                DebugLog.DebugLog.Show.LogWarning(this, "Unsupported input device type: None");
                return null;
            }

            if (Value.SpriteAssets == null || Value.SpriteAssets.Count == 0)
            {
                DebugLog.DebugLog.Show.LogError(this, "SpriteAssets list is null or empty.");
                return null;
            }

            var spriteAssetData = Value.SpriteAssets.Find(prompt => prompt.InputDeviceType == inputDeviceType);
            if (spriteAssetData == null)
            {
                DebugLog.DebugLog.Show.LogWarning(this, $"SpriteAssets not found for device type: {inputDeviceType}");
                return null;
            }

            return spriteAssetData;
        }

        public ButtonPromptData.Prompt GetPrompt(string promptName, InputDeviceType inputDeviceType)
        {
            var promptData = Value.ButtonPrompts.Find(data => data.Value.Name == promptName);
            return promptData.GetPrompt(inputDeviceType);
        }

        #endregion
    }
}