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
    [CreateAssetMenu(fileName = "Language", menuName = "Mixin/Language file")]
    public class LanguageFile : ScriptableObject
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] List<LanguageBlock> _languageBlockList;

        /// <inheritdoc cref="_languageBlockList"/>
        public List<LanguageBlock> LanguageBlockList { get => _languageBlockList; }

        /// <inheritdoc cref="LanguageManager.SelectedLanguage"/>
        Language _selectedLanguage;

        private void OnEnable()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetTextFromLanguage()
        {
            try
            {
                _selectedLanguage = LanguageManager.Instance.SelectedLanguage;

                // Cycle through all language blocks and search for the current selected language
                foreach (LanguageBlock language in _languageBlockList)
                {
                    if (language.Language == _selectedLanguage)
                        return language.Text;
                }

                // If there is no matching language, it will return null
                return null;
            }
            catch(Exception e)
            {
                $"Could not get text from language file: {e}".LogWarning();
                return null;
            }
        }

        private void OnValidate()
        {
            LanguageManager.Instance.RefreshTexts();
        }
    }
}

