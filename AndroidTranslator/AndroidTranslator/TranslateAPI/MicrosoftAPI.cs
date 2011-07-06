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
using System.Net;
using System.IO;

namespace AndroidTranslator.TranslateAPI
{
    class MicrosoftAPI : TranslateAPIBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="API"></param>
        /// <param name="SourceLanguage"></param>
        /// <param name="TargetLanguage"></param>
        public MicrosoftAPI(string API, string SourceLanguage, string TargetLanguage)
            : base(API, SourceLanguage, TargetLanguage)
        {
        }


        /// <summary>
        /// Used to perform the actual translation
        /// </summary>
        /// <param name="InputString"></param>
        /// <returns></returns>
        public override string TranslateString(string InputString)
        {
            Console.WriteLine("Processing: " + InputString);
            string result = "";


            using (WebClient client = new WebClient())
            {
                using (Stream data = client.OpenRead(this.BuildRequestString(InputString)))
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    result = (string)dcs.ReadObject(data);

                    data.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Used to build up the url used for requesting translation
        /// </summary>
        /// <param name="TranslateText"></param>
        /// <returns></returns>
        private string BuildRequestString(string InputString)
        {
            return string.Format("http://api.microsofttranslator.com/V2/Http.svc/Translate?AppId={0}&text={1}&from={2}&to={3}", base._API, InputString, this._SourceLanguage, this._TargetLanguage); 
        }
    }
}
