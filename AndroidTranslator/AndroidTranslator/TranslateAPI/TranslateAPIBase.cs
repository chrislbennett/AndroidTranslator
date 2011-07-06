//Copyright 2011 Chris L. Bennett
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidTranslator.TranslateAPI
{
    abstract class TranslateAPIBase
    {
        protected string _API;
        protected string _SourceLanguage;
        protected string _TargetLanguage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="API"></param>
        /// <param name="SourceLanguage"></param>
        /// <param name="TargetLanguage"></param>
        public TranslateAPIBase(string API, string SourceLanguage, string TargetLanguage)
        {
            this._API = API;
            this._SourceLanguage = SourceLanguage;
            this._TargetLanguage = TargetLanguage;
        }


        /// <summary>
        /// Used to Translate a specific string
        /// </summary>
        /// <param name="InputString"></param>
        /// <returns></returns>
        public abstract string TranslateString(string InputString);
    }
}
