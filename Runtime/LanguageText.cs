using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin.Utils;
using TMPro;
using System;

namespace Mixin.Language
{
    /// <summary>
    /// The component to put on the object.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LanguageText : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] LanguageFile _languageFile;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] bool _useCustomTextField = false;

        /// <summary>
        /// 
        /// </summary>
        [ConditionalField("_useCustomTextField", true)]
        [SerializeField] TMP_Text _customTextField;

        /********* *********/

        /// <summary>
        /// 
        /// </summary>
        TMP_Text _textField;

        /// <summary>
        /// 
        /// </summary>
        Language _selectedLanguage;

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
            if (_textField == null) return;

            _textField.text = GetTextFromLanguage();
        }

        private string GetTextFromLanguage()
        {
            return _languageFile.GetText();
        }

        private void OnValidate()
        {
            Setup();
        }
    }
}

