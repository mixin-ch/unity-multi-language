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
        /// The current select language.
        /// </summary>
        [SerializeField] Language _selectedLanguage = Language.EN;

        /// <summary>
        /// Use this language if the selected language value is empty.
        /// </summary>
        [SerializeField] Language _fallbackLanguage = Language.EN;

        /// <inheritdoc cref="_selectedLanguage"/>
        public Language SelectedLanguage { get => _selectedLanguage; set => _selectedLanguage = value; }

        /// <inheritdoc cref="_fallbackLanguage"/>
        public Language FallbackLanguage { get => _fallbackLanguage; set => _fallbackLanguage = value; }

        /// <summary>
        /// Searches all language components and setups them.
        /// </summary>
        public void RefreshTexts()
        {
            // Find all Language components.
            LanguageText[] languageText = FindObjectsOfType<LanguageText>();

            foreach (LanguageText text in languageText)
            {
                text.Setup();
            }
        }

        private void OnValidate()
        {
            RefreshTexts();
        }
    }

}

