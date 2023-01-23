using System;
using UnityEditor;
using UnityEngine;

namespace Mixin.MultiLanguage
{
    /// <summary>
    /// This class contains a normal Text property. <br></br>
    /// But the inspector shows a custom drawer which has 100f height and 100% width.
    /// </summary>
    [Serializable]
    public class MultilineString
    {
        public string Text;
    }
}

