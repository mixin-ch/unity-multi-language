using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin.Utils;
using TMPro;
using System;
using UnityEditor;

namespace Mixin.Language
{
    /// <summary>
    /// This is the scriptable object. 
    /// Here you can type all individual language texts.
    /// You can drag/drop this on the component.
    /// </summary>
    [CreateAssetMenu(fileName = "Language", menuName = "Mixin/Language file")]
    public class LanguageSO : ScriptableObject
    {
        /// <summary>
        /// The dictionary that contains language text pairs
        /// </summary>
        [SerializeField] MixinDictionary<Language, MultilineString> _languageTextList;

        /// <inheritdoc cref="_languageTextList"/>
        public MixinDictionary<Language, MultilineString> LanguageTextList { get => _languageTextList; }

        /// <summary>
        /// Get the text from the language defined in the Language Manager.
        /// It also handles the fallback language.
        /// </summary>
        public string GetText()
        {
            Language selectedLanguage = LanguageManager.Instance.SelectedLanguage;
            Language fallbackLanguage = LanguageManager.Instance.FallbackLanguage;

            if (_languageTextList?[selectedLanguage] != null)
                return _languageTextList?[selectedLanguage].Text;

            if (_languageTextList?[fallbackLanguage] != null)
                return _languageTextList?[fallbackLanguage].Text;

            return "<color=red>No text set";
        }

        private void OnValidate()
        {
            LanguageManager.Instance.RefreshTexts();
        }
    }
}

