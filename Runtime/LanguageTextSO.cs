using UnityEngine;
using Mixin.Utils;

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
        MixinDictionary<Language, MultilineString> _languageTextList;

        /// <summary>
        /// Refrence to other Lanuage Files to replace the placeholders.
        /// </summary>
        [SerializeField]
        private LanguageTextSO[] _placeholders;

        /// <inheritdoc cref="_languageTextList"/>
        public MixinDictionary<Language, MultilineString> LanguageTextList { get => _languageTextList; }

        /// <summary>
        /// Get the text from the language defined in the Language Manager.
        /// It also handles the fallback language.
        /// </summary>
        public string GetText(bool replacePlaceholders = true)
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
                text = TryReplacePlaceholders(text);

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
        /// <returns>Returns the same string if there are no placeholders</returns>
        private string TryReplacePlaceholders(string textWithPlaceholders)
        {
            if (_placeholders == null ||
                _placeholders.Length == 0)
                return textWithPlaceholders;

            string replacedText = textWithPlaceholders;
            for (int i = 0; i < _placeholders.Length; i++)
            {
                // Check if the placeholder is null
                if (_placeholders[i] == null)
                    continue;

                replacedText = replacedText.Replace("{" + i + "}", _placeholders[i].GetText(true));
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
    }
}

