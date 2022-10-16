using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin.Utils;
using TMPro;
using System;

namespace Mixin.Language
{
    /// <summary>
    /// It pairs the language with the belonging text.
    /// </summary>
    [System.Serializable]
    public class LanguageBlock
    {
        /// <summary>
        /// The text for the specific language.
        /// </summary>
        [Multiline]
        [SerializeField] string _text;

        /// <inheritdoc cref="_text"/>
        public string Text { get => _text; }
    }
}

