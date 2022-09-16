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

        /// <summary>
        /// 
        /// </summary>
        public string GetText()
        {
            try
            {
                string text = null;
                string fallbackText = null;
                Language selectedLanguage = LanguageManager.Instance.SelectedLanguage;
                Language fallbackLanguage = LanguageManager.Instance.FallbackLanguage;

                // Cycle through all language blocks and search for the current selected language
                foreach (LanguageBlock language in _languageBlockList)
                {
                    // Get selected language text
                    if (language.Language == selectedLanguage)
                        text = language.Text;

                    // Get fallback text
                    if (language.Language == fallbackLanguage)
                        fallbackText = language.Text;
                }

                // If there is no matching language, it will return null
                return text ?? fallbackText;
            }
            catch (Exception e)
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

