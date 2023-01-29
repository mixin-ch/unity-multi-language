using System;

namespace Mixin.MultiLanguage
{
    /// <summary>
    /// This class contains a normal Text property. <br></br>
    /// But the inspector shows a custom drawer which has 100f height and 100% width.
    /// </summary>
    [Serializable]
    public struct MultilineString
    {
        public string Text;
    }
}

