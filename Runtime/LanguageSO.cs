using UnityEngine;
using Mixin.Utils;

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
            // Get selected and fallback language from the language manager.
            Language selectedLanguage = LanguageManager.Instance.SelectedLanguage;
            Language fallbackLanguage = LanguageManager.Instance.FallbackLanguage;

            // Return the selected language text if it exists.
            if(_languageTextList.ContainsKey(selectedLanguage))
                return _languageTextList[selectedLanguage].Text;

            // Return the fallback language text if selected language does not exist.
            if (_languageTextList.ContainsKey(fallbackLanguage))
                return _languageTextList[fallbackLanguage].Text;

            // Return the name of the file, if selected and fallback text is missing.
            return $"<color=orange><{name}>";
        }

        private void OnValidate()
        {
            LanguageManager.Instance.RefreshTexts();
        }
    }
}

