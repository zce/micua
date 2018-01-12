// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryWapper.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the Settings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Micua.Core.ComponentModel
{
    using System.Collections.Generic;

    /// <summary>
    /// The dictionary wapper.
    /// </summary>
    public class DictionaryWapper
    {
        private IDictionary<string, string> _data;

        public DictionaryWapper(IDictionary<string, string> data)
        {
            this._data = data;
        }

        public string this[string index]
        {
            get
            {
                if (_data.ContainsKey(index)) return _data[index];
                return string.Empty;
            }
        }
    }
}
