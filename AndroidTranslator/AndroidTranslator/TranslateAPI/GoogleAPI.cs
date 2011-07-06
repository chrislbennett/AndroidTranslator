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
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace AndroidTranslator.TranslateAPI
{
    class GoogleAPI : TranslateAPIBase 
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="API"></param>
        /// <param name="SourceLanguage"></param>
        /// <param name="TargetLanguage"></param>
        public GoogleAPI(string API, string SourceLanguage, string TargetLanguage)
            : base(API, SourceLanguage, TargetLanguage)
        {
        }

        /// <summary>
        /// Used to perform the actual conversion of the input string
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

                    using (StreamReader reader = new StreamReader(data))
                    {
                        string s = reader.ReadToEnd();

                        result = ExtractTranslatedString(s);

                        reader.Close();
                    }

                    data.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Used to extract the translated string from the input json data
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        private string ExtractTranslatedString(string jsonData)
        {
            JObject jo = JObject.Parse(jsonData);

            return (string)jo.SelectToken("data.translations[0].translatedText");

        }

        /// <summary>
        /// Used to build the actual URL request for the google translate api
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string BuildRequestString(string text)
        {
            return string.Format("https://www.googleapis.com/language/translate/v2?key={0}&source={1}&target={2}&q={3}", base._API,this._SourceLanguage,this._TargetLanguage,text);
        }
    }
}
