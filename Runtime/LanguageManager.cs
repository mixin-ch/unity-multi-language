using UnityEngine;
using Mixin.Utils;

namespace Mixin.MultiLanguage
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
        private Language _selectedLanguage = Language.English;

        /// <summary>
        /// Use this language if the selected language value is empty.
        /// </summary>
        [SerializeField]
        private Language _fallbackLanguage = Language.English;

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

        public static Language GetLanguageFromSystemLanguage()
        {
            return GetLanguageFromSystemLanguageType(
                GetSystemLanguage());
        }

        public static Language GetLanguageFromSystemLanguageType(SystemLanguage systemLanguage)
        {
            switch (systemLanguage)
            {
                case SystemLanguage.Dutch:
                    return Language.Dutch;
                case SystemLanguage.English: 
                    return Language.English;
                case SystemLanguage.Finnish:
                    return Language.Finnish;
                case SystemLanguage.French:
                    return Language.French;
                case SystemLanguage.German:
                    return Language.German;
                case SystemLanguage.Icelandic:
                    return Language.Icelandic;
                case SystemLanguage.Italian:
                    return Language.Italian;
                case SystemLanguage.Japanese:
                    return Language.Japanese;
                case SystemLanguage.Korean:
                    return Language.Korean;
                case SystemLanguage.Norwegian:
                    return Language.Norwegian;
                case SystemLanguage.Polish:
                    return Language.Polish;
                case SystemLanguage.Portuguese:
                    return Language.Portuguese;
                case SystemLanguage.Russian:
                    return Language.Russian;
                case SystemLanguage.Spanish:
                    return Language.Spanish;
                case SystemLanguage.Swedish:
                    return Language.Swedish;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseTraditional:
                case SystemLanguage.ChineseSimplified:
                    return Language.Chinease;
                default:
                    return Language.English;
            }
        }

        private void OnValidate()
        {
            if (_liveRefresh)
                RefreshTexts();
        }
    }

}

