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
    class Program
    {
        /// <summary>
        /// Main entry point for the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Start processing the arguments
            CommandLineProcess clp = new CommandLineProcess(args);
            if (clp.ReadyForTranslation)
            {
                //Ok we have what we need, start the conversion
                Translate t = new Translate(clp.APIKey, clp.SourceLanguage, clp.SourcePath, clp.TargetLanguage, clp.TargetPath, clp.Service);
                t.Go();
            }

            //Console.ReadLine();
        }
    }
}
