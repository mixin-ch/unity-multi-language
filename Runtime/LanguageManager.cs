using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin.Utils;
using TMPro;
using System;

namespace Mixin.Language
{
    /// <summary>
    /// 
    /// </summary>
    public class LanguageManager : Singleton<LanguageManager>
    {
        /// <summary>
        /// The current select language.
        /// </summary>
        [SerializeField] Language _selectedLanguage = Language.EN;

        /// <inheritdoc cref="_selectedLanguage"/>
        public Language SelectedLanguage { get => _selectedLanguage; set => _selectedLanguage = value; }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshTexts()
        {
            LanguageText[] languageText = FindObjectsOfType<LanguageText>();
            $"found: {languageText.Length}".Log();

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

