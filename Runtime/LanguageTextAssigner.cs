using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin.Utils;
using TMPro;
using System;
using UnityEngine.Rendering;

namespace Mixin.Language
{
    /// <summary>
    /// The component to put on the object.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LanguageTextAssigner : MonoBehaviour
    {
        /// <summary>
        /// The language file (scriptable object).
        /// </summary>
        [SerializeField] LanguageTextSO _languageFile;

        /// <summary>
        /// Enable to manually define a text field that should be updated.
        /// </summary>
        [SerializeField] bool _useCustomTextField = false;

        /// <summary>
        /// The custom text field that will be updated when "_useCustomTextField" is enabled.
        /// </summary>
        [ConditionalField("_useCustomTextField", true)]
        [SerializeField] TMP_Text _customTextField;

        /********* *********/

        /// <summary>
        /// The text field that will be updated.
        /// </summary>
        TMP_Text _textField;

        private void Start()
        {
            Setup();
        }

        public void Setup()
        {
            // Define the text field
            if (_useCustomTextField)
                _textField = _customTextField;
            else
                _textField = GetComponent<TMP_Text>();

            UpdateText();
        }

        private void UpdateText()
        {
            if (_textField == null ||
                _languageFile == null)
                return;

            _textField.text = GetTextFromLanguage();
        }

        private string GetTextFromLanguage()
        {
            return _languageFile.GetText();
        }

        private void OnValidate()
        {
            if (LanguageManager.Instance == null)
                return;

            if (LanguageManager.Instance.LiveRefresh)
                Setup();
        }
    }
}

