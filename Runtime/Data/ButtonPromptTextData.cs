using System;
using System.Collections.Generic;

using Freyja.ButtonPrompts.DataAsset;
using Freyja.InputSystem;

using Sirenix.OdinInspector;

using TMPro;

using UnityEngine;

namespace Freyja.ButtonPrompts.Data
{
    [InfoBox("Button Prompt Text Data Create In Once")]
    [Serializable]
    public class ButtonPromptTextData
    {
        #region Constants

        private const string PromptSpriteAssetsGrp = "Prompt Sprite Assets";
        private const string ButtonPromptDataAssetGrp = "Prompt Data Asset";

        #endregion

        #region Fields

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(PromptSpriteAssetsGrp)]
    #endif
        [SerializeField]
        private List<SpriteAssetData> m_SpriteAssets;

    #if UNITY_EDITOR && ODIN_INSPECTOR
        [TitleGroup(ButtonPromptDataAssetGrp)]
    #endif
        [SerializeField]
        private List<ButtonPromptDataAsset> m_ButtonPrompts;

        #endregion

        #region Properties

        public List<SpriteAssetData> SpriteAssets
        {
            get => m_SpriteAssets;
        }

        public List<ButtonPromptDataAsset> ButtonPrompts
        {
            get => m_ButtonPrompts;
        }

        #endregion

        #region Inner Class

        [Serializable]
        public class SpriteAssetData
        {
            #region Fields

            [SerializeField]
            private InputDeviceType m_InputDeviceType;

            [SerializeField]
            private TMP_SpriteAsset m_SpriteAsset;

            #endregion

            #region Properties

            public InputDeviceType InputDeviceType
            {
                get => m_InputDeviceType;
            }

            public TMP_SpriteAsset TMPSpriteAsset
            {
                get => m_SpriteAsset;
            }

            #endregion
        }

        #endregion
    }
}