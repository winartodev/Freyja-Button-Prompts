using System;
using System.Collections.Generic;

using Freyja.InputSystem;

using Sirenix.OdinInspector;

using UnityEngine;

namespace Freyja.ButtonPrompts.Data
{
    [Serializable]
    public class ButtonPromptData
    {
        #region Constants

        private const string PromptNameGrp = "Prompt Name";
        private const string PromptsGrp = "Prompts";

        #endregion

        #region Fields

    #if ODIN_INSPECTOR && UNITY_EDITOR
        [TitleGroup(PromptNameGrp)]
        [HideLabel]
    #endif
        [SerializeField]
        private string m_Name;

    #if ODIN_INSPECTOR && UNITY_EDITOR
        [TitleGroup(PromptsGrp)]
        [TableList]
    #endif
        [SerializeField]
        private List<Prompt> m_Prompts;

        #endregion

        #region Properties

        public string Name
        {
            get => m_Name;
        }

        public List<Prompt> Prompts
        {
            get => m_Prompts;
        }

        #endregion

        #region Inner Class

        [Serializable]
        public class Prompt
        {
            #region Fields

        #if ODIN_INSPECTOR && UNITY_EDITOR
            [TableColumnWidth(120, Resizable = false)]
        #endif
            [SerializeField]
            private InputDeviceType m_InputDeviceType = InputDeviceType.Keyboard;

        #if ODIN_INSPECTOR && UNITY_EDITOR
            [PreviewField(50, ObjectFieldAlignment.Left)]
        #endif
            [SerializeField]
            private Sprite m_Icon;

            #endregion

            #region Properties

            public InputDeviceType InputDeviceType
            {
                get => m_InputDeviceType;
            }

            public Sprite Icon
            {
                get => m_Icon;
            }

            #endregion
        }

        #endregion
    }
}