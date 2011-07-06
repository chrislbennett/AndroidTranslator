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

namespace AndroidTranslator
{
    /// <summary>
    /// Used to figure out what to do with the command line arguments
    /// </summary>
    class CommandLineProcess
    {
        public bool ReadyForTranslation { get; set; }

        public string SourceLanguage { get; set; }
        public string SourcePath { get; set; }
        public string TargetLanguage { get; set; }
        public string TargetPath { get; set; }
        public string APIKey { get; set; }
        public string Service { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args"></param>
        public CommandLineProcess(string[] args)
        {
            ReadyForTranslation = false;
            SourceLanguage = "";
            SourcePath = "";
            TargetLanguage = "";
            TargetPath = "";
            APIKey = "";

            //See if we have any args at all
            if (args.Count() == 0 || args.Contains("--help",StringComparer.CurrentCultureIgnoreCase))
            {
                //Display Help
                DisplayHelp();
            }
            else if (args.Contains("--lang",StringComparer.CurrentCultureIgnoreCase))
            {
                DisplayLanguages();
            }
            else
            {
                //Make sure we have everything we need to actually perform a conversion
                foreach (string s in args)
                {
                    string[] data = s.Split('=');

                    if (data.Count() == 2)
                    {
                        switch (data[0])
                        {
                            case "-k":
                                APIKey = data[1];
                                break;
                            case "-sL":
                                SourceLanguage = data[1];
                                break;
                            case "-s":
                                SourcePath = data[1];
                                break;
                            case "-tL":
                                TargetLanguage = data[1];
                                break;
                            case "-t":
                                TargetPath = data[1];
                                break;
                            case "-x":
                                if (data[1].ToLower() == "google" || data[1].ToLower() == "microsoft")
                                {
                                    Service = data[1].ToLower();
                                }
                                break;
                        }
                    }
                }

                //Make sure we have all of the ones we need
                if (SourceLanguage != "" & SourcePath != "" & TargetLanguage != "" && TargetPath != "" && APIKey != "" && Service != "")
                {
                    ReadyForTranslation = true;
                }
                else
                {
                    Console.WriteLine("Error: Missing Required Parameters");
                }
            }
        }

        /// <summary>
        /// Used to display the 
        /// </summary>
        private void DisplayHelp()
        {
            DisplayHeader();
            Console.WriteLine("Android Translator is used to take advantage of the Google Translate API for");
            Console.WriteLine("translating string resource files into other languages.");
            Console.WriteLine();
            Console.WriteLine("AndroidTranslator -k=[APIkey] -sL=[lang] -s=[source] -tL=[lang] -t=[target] ");
            Console.WriteLine();
            Console.WriteLine("    -k=[APIkey]           - API Key");
            Console.WriteLine("    -x=[Google|Microsoft] - Service Google or Microsoft/Bing");
            Console.WriteLine("    -sL=[lang]            - Source Language Code");
            Console.WriteLine("    -s=[source]           - Path to the source file");
            Console.WriteLine("    -tL=[lang]            - Target Language Code");
            Console.WriteLine("    -t=[target]           - Path to the target file");
            Console.WriteLine("    --help                - Display this help screen");
            Console.WriteLine("    --lang                - Display the list of language codes");
            Console.WriteLine();
            Console.WriteLine("Example");
            Console.WriteLine(" AndroidTranslator -k=YourKeyHere -sL=en -s=strings.xml -tL=es -t=strings_es.xml");
            Console.WriteLine("");
        }

        /// <summary>
        /// Used to display header info
        /// </summary>
        private void DisplayHeader()
        {
            Console.WriteLine("AndroidTranslator - Version: {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() );
            Console.WriteLine("Created by Chris L. Bennett");
            Console.WriteLine();
        }

        /// <summary>
        /// Used to display the accepted languages
        /// </summary>
        private void DisplayLanguages()
        {
            DisplayHeader();
            Console.WriteLine("Accepted Language Codes");

            Console.WriteLine("Code	    Language");
            Console.WriteLine("-----       ---------------------------------");
            Console.WriteLine("af	    AFRIKAANS");
            Console.WriteLine("sq	    ALBANIAN");
            Console.WriteLine("ar	    ARABIC");
            Console.WriteLine("be	    BELARUSIAN");
            Console.WriteLine("bg	    BULGARIAN");
            Console.WriteLine("ca	    CATALAN");
            Console.WriteLine("zh	    CHINESE");
            Console.WriteLine("zh-CN       CHINESE_SIMPLIFIED");
            Console.WriteLine("zh-TW       CHINESE_TRADITIONAL");
            Console.WriteLine("hr	    CROATIAN");
            Console.WriteLine("cs	    CZECH");
            Console.WriteLine("da	    DANISH");
            Console.WriteLine("nl	    DUTCH");
            Console.WriteLine("en	    ENGLISH");
            Console.WriteLine("et	    ESTONIAN");
            Console.WriteLine("tl	    FILIPINO");
            Console.WriteLine("fi	    FINNISH");
            Console.WriteLine("fr	    FRENCH");
            Console.WriteLine("gl	    GALICIAN");
            Console.WriteLine("de	    GERMAN");
            Console.WriteLine("el	    GREEK");
            Console.WriteLine("ht	    HAITIAN_CREOLE");
            Console.WriteLine("iw	    HEBREW");
            Console.WriteLine("hi	    HINDI");
            Console.WriteLine("hu	    HUNGARIAN");
            Console.WriteLine("is	    ICELANDIC");
            Console.WriteLine("id	    INDONESIAN");
            Console.WriteLine("ga	    IRISH");
            Console.WriteLine("it	    ITALIAN");
            Console.WriteLine("ja	    JAPANESE");
            Console.WriteLine("ko	    KOREAN");
            Console.WriteLine("lv	    LATVIAN");
            Console.WriteLine("lt	    LITHUANIAN");
            Console.WriteLine("mk	    MACEDONIAN");
            Console.WriteLine("ms	    MALAY");
            Console.WriteLine("mt	    MALTESE");
            Console.WriteLine("no	    NORWEGIAN");
            Console.WriteLine("fa	    PERSIAN");
            Console.WriteLine("pl	    POLISH");
            Console.WriteLine("pt	    PORTUGUESE");
            Console.WriteLine("pt-PT       PORTUGUESE_PORTUGAL");
            Console.WriteLine("ro	    ROMANIAN");
            Console.WriteLine("ru	    RUSSIAN");
            Console.WriteLine("sr	    SERBIAN");
            Console.WriteLine("sk	    SLOVAK");
            Console.WriteLine("sl	    SLOVENIAN");
            Console.WriteLine("es	    SPANISH");
            Console.WriteLine("sw	    SWAHILI");
            Console.WriteLine("sv	    SWEDISH");
            Console.WriteLine("tl	    TAGALOG");
            Console.WriteLine("th	    THAI");
            Console.WriteLine("tr	    TURKISH");
            Console.WriteLine("uk	    UKRAINIAN");
            Console.WriteLine("vi	    VIETNAMESE");
            Console.WriteLine("cy	    WELSH");
            Console.WriteLine("yi	    YIDDISH");
        }
    }
}
