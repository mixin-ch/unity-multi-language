using UnityEngine;
using Mixin.Utils;
using System.Collections.Generic;

namespace Mixin.MultiLanguage
{
    /// <summary>
    /// This is the scriptable object. 
    /// Here you can type all individual language texts.
    /// You can drag/drop this on the component.
    /// </summary>
    [CreateAssetMenu(fileName = "LanguageText", menuName = "Mixin/Language Text File")]
    public class LanguageTextSO : ScriptableObject
    {
        /// <summary>
        /// The dictionary that contains language text pairs
        /// </summary>
        [SerializeField]
        private MixinDictionary<Language, MultilineString> _languageTextList;

        /// <summary>
        /// Refrence to other Lanuage Files to replace the placeholders.
        /// </summary>
        [SerializeField]
        private LanguageTextSO[] _placeholders;

        /// <inheritdoc cref="_languageTextList"/>
        public MixinDictionary<Language, MultilineString> LanguageTextList { get => _languageTextList.Copy(); }

        /// <summary>
        /// Get the text from the language defined in the Language Manager.
        /// It also handles the fallback language.
        /// </summary>
        public string GetText(bool replacePlaceholders = true)
        {
            return GetText(replacePlaceholders, null);
        }

        private string GetText(bool replacePlaceholders, HashSet<LanguageTextSO> forbiddenPlaceholderSet)
        {
            if (LanguageManager.Instance == null)
                return $"<color=orange><LanguageManager is not set></color>";

            // Get selected and fallback language from the language manager.
            Language selectedLanguage = LanguageManager.Instance.SelectedLanguage;
            Language fallbackLanguage = LanguageManager.Instance.FallbackLanguage;

            string text = null;

            // Set the selected language text if it exists.
            if (_languageTextList.ContainsKey(selectedLanguage))
                text = _languageTextList[selectedLanguage].Text;

            // Set the fallback language text if selected language does not exist.
            if (_languageTextList.ContainsKey(fallbackLanguage))
                text = _languageTextList[fallbackLanguage].Text;

            if (replacePlaceholders)
                text = TryReplacePlaceholders(text, forbiddenPlaceholderSet);

            // Return the text
            if (text != null)
                return text;

            // Return the name of the file, if selected and fallback text is missing.
            return $"<color=orange><{name}></color>";
        }

        /// <summary>
        /// Replaces all placeholders with the refrenced texts.
        /// </summary>
        /// <param name="textWithPlaceholders"></param>
        /// <param name="forbiddenPlaceholderSet">List of all LanguageTextSO not to be replaced.</param>
        /// <returns>Returns the same string if there are no placeholders</returns>
        private string TryReplacePlaceholders(string textWithPlaceholders, HashSet<LanguageTextSO> forbiddenPlaceholderSet)
        {
            if (forbiddenPlaceholderSet == null)
                forbiddenPlaceholderSet = new HashSet<LanguageTextSO>();

            // Add self to forbidden to prevent endless recursion.
            forbiddenPlaceholderSet.Add(this);

            if (_placeholders == null ||
                _placeholders.Length == 0)
                return textWithPlaceholders;

            string replacedText = textWithPlaceholders;
            for (int i = 0; i < _placeholders.Length; i++)
            {
                LanguageTextSO placeholder = _placeholders[i];

                if (placeholder == null)
                    continue;
                if (forbiddenPlaceholderSet.Contains(placeholder))
                    continue;

                replacedText = replacedText.Replace("{" + i + "}", placeholder.GetText(true, new HashSet<LanguageTextSO>(forbiddenPlaceholderSet)));
            }

            return replacedText;
        }

        private void OnValidate()
        {
            if (LanguageManager.Instance == null)
                return;

            if (LanguageManager.Instance.LiveRefresh)
                LanguageManager.Instance?.RefreshTexts();
        }

        public static LanguageTextSO Create(
            MixinDictionary<Language, MultilineString> languageTextList,
            LanguageTextSO[] placeholders)
        {
            LanguageTextSO languageTextSO = CreateInstance<LanguageTextSO>();
            languageTextSO._languageTextList = languageTextList;
            languageTextSO._placeholders = placeholders;

            return languageTextSO;
        }

        public static LanguageTextSO Create(
            MixinDictionary<Language, MultilineString> languageTextList,
            List<LanguageTextSO> placeholders)
        {
            LanguageTextSO languageTextSO = CreateInstance<LanguageTextSO>();
            languageTextSO._languageTextList = languageTextList;
            languageTextSO._placeholders = placeholders.ToArray();

            return languageTextSO;
        }
    }
}

