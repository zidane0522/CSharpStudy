﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CsharpLearning
{
    public class CodeDriver
    {
        private string prefix = "using System;public static class Driver{public static void Run(){";
        private string postfix="}}";
        public string CompileAndRun(string input,out bool hasError)
        {
            hasError = false;
            string returnData = null;

            CompilerResults results = null;
            using (var provider=new CSharpCodeProvider())
            {
                var options = new CompilerParameters();
                options.GenerateInMemory = true;

                var sb = new StringBuilder();
                sb.Append(prefix);
                sb.Append(input);
                sb.Append(postfix);
                results = provider.CompileAssemblyFromSource(options,sb.ToString());

            }
            if (results.Errors.HasErrors)
            {
                hasError = true;
                var errorMessage = new StringBuilder();
                foreach (CompilerError item in results.Errors)
                {

                }
                returnData = errorMessage.ToString();
            }
            else
            {
                TextWriter temp = Console.Out;
                var writer = new StringWriter();
                Console.SetOut(writer);
                Type driverType = results.CompiledAssembly.GetType("Driver");
                driverType.InvokeMember("Run", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, null);
                Console.SetOut(temp);
                returnData = writer.ToString();
            }
            return returnData;
        }
    }
}
