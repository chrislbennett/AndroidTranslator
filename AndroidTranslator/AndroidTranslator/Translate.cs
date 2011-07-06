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
using System.Xml.Linq;
using System.Xml;
using System.IO;


namespace AndroidTranslator
{
    /// <summary>
    /// Used to manage the translation process
    /// </summary>
    class Translate
    {
        private string _SourceLanguage { get; set; }
        private string _SourcePath { get; set; }
        private string _TargetLanguage { get; set; }
        private string _TargetPath { get; set; }
        private string _APIKey { get; set; }
        private string _service { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="APIKey"></param>
        /// <param name="SourceLanguage"></param>
        /// <param name="SourcePath"></param>
        /// <param name="TargetLanguage"></param>
        /// <param name="TargetPath"></param>
        public Translate(string APIKey, string SourceLanguage, string SourcePath, string TargetLanguage, string TargetPath, string Service)
        {
            this._APIKey = APIKey;
            this._SourceLanguage = SourceLanguage;
            this._SourcePath = SourcePath;
            this._TargetLanguage = TargetLanguage;
            this._TargetPath = TargetPath;
            this._service = Service;
        }

        //Used to start the translation process
        public void Go()
        {
            XElement TargetRoot;

            //Get the Translation API
            TranslateAPI.TranslateAPIBase translator = null;
            switch (_service.ToLower())
            {
                case "microsoft":
                    translator = new TranslateAPI.MicrosoftAPI(this._APIKey, this._SourceLanguage, this._TargetLanguage);
                    break;
                default:
                    translator = new TranslateAPI.GoogleAPI(this._APIKey, this._SourceLanguage, this._TargetLanguage);
                    break;
            }
            

            //First thing go out and try to open the source file and start processing
            using (TextReader tr = File.OpenText(_SourcePath))
            {
                XDocument xdoc = XDocument.Load(tr);
                XElement root = xdoc.Element("resources");
                TargetRoot = new XElement("resources");

                foreach (XElement e in root.Elements())
                {
                    if (e.Name.LocalName.Equals("string", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //Normal String Element
                        TargetRoot.Add(ProcessString(translator, e));
                    }
                    else if (e.Name.LocalName.Equals("string-array", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //String Array
                        TargetRoot.Add(ProcessStringArray(translator, e));
                    }
                    else
                    {
                        //Unknown element, just pass it thru
                        TargetRoot.Add(e);
                    }
                }
            }

            //Now that the translation is done we can output the result            
            XmlTextWriter tw = new XmlTextWriter(this._TargetPath, Encoding.UTF8);
            tw.Formatting = Formatting.Indented;
            TargetRoot.WriteTo(tw);
            tw.Close();
            Console.WriteLine("Done.");
        }


        /// <summary>
        /// Used for translating string array
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private XElement ProcessStringArray(TranslateAPI.TranslateAPIBase translator, XElement e)
        {
            XElement OutputEelement = new XElement("string-array", e.Attribute("name"));

            //loop through the invidual strings and process them one at a time
            foreach (XElement item in e.Elements("item"))
            {
                OutputEelement.Add(new XElement("item", translator.TranslateString(item.Value)));
            }

            return OutputEelement;
        }

        /// <summary>
        /// Used for translating strings
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private XElement ProcessString(TranslateAPI.TranslateAPIBase translator, XElement e)
        {
            XElement OutputElement = new XElement("string");
            OutputElement.Add(new XAttribute("name", e.Attribute("name").Value));

            //Translate the individual string
            OutputElement.Value = translator.TranslateString(e.Value);
            
            return OutputElement;
        }

    }
}
