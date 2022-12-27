using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin.Utils;
using TMPro;
using System;

namespace Mixin.Language
{
    /// <summary>
    /// This object manages the languages.
    /// </summary>
    [ExecuteAlways]
    public class LanguageManager : Singleton<LanguageManager>
    {
        /// <summary>
        /// When enabled it refreshes all the Textes in Editor Mode.
        /// </summary>
        [SerializeField]
        private bool _liveRefresh;

        /// <summary>
        /// The current select language.
        /// </summary>
        [SerializeField]
        private Language _selectedLanguage = Language.EN;

        /// <summary>
        /// Use this language if the selected language value is empty.
        /// </summary>
        [SerializeField]
        private Language _fallbackLanguage = Language.EN;

        /// <inheritdoc cref="_selectedLanguage"/>
        public Language SelectedLanguage { get => _selectedLanguage; set => _selectedLanguage = value; }

        /// <inheritdoc cref="_fallbackLanguage"/>
        public Language FallbackLanguage { get => _fallbackLanguage; set => _fallbackLanguage = value; }
        public bool LiveRefresh { get => _liveRefresh; }

        /// <summary>
        /// Searches all language components and setups them.
        /// </summary>
        public void RefreshTexts()
        {
            // Find all Language components.
            LanguageTextAssigner[] languageText = FindObjectsOfType<LanguageTextAssigner>();

            if (languageText == null)
                return;

            foreach (LanguageTextAssigner text in languageText)
            {
                text.Setup();
            }
        }

        public static SystemLanguage GetSystemLanguage()
        {
            return Application.systemLanguage;
        }

        private void OnValidate()
        {
            if (_liveRefresh)
                RefreshTexts();
        }
    }

}

