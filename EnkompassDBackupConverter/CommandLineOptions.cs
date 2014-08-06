// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineOptions.cs" company="Soroush Falahati (soroush@falahati.net)">
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see [http://www.gnu.org/licenses/].
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace EnkompassDBackupConverter
{
    using System;
    using System.Linq;

    using CommandLine;
    using CommandLine.Text;

    internal class CommandLineOptions
    {
        private static CommandLineOptions defaultObject;

        private CommandLineOptions()
        {
        }

        [Option('t', "pertable", HelpText = "Generate a script file per each table", DefaultValue = false)]
        public bool PerTableFile { get; set; }

        [Option(HelpText = "MYSQL_BACKUP_FILES address", DefaultValue = "")]
        public string BackupDirectory { get; set; }

        [Option('o', "output", HelpText = "Output directory", DefaultValue = "")]
        public string OutputDirectory { get; set; }

        [Option('d', "databases", HelpText = "Databases to include, separated by comma", DefaultValue = "")]
        public string Databases { get; set; }

        [Option('c', "create", HelpText = "Generate create script", DefaultValue = true)]
        public bool GenerateCreateScript { get; set; }

        [Option('i', "insert", HelpText = "Generate Insert script", DefaultValue = true)]
        public bool GenerateInsertScript { get; set; }

        public static CommandLineOptions Default
        {
            get
            {
                if (defaultObject == null)
                {
                    defaultObject = new CommandLineOptions();
                    Parser.Default.ParseArguments(Environment.GetCommandLineArgs().Skip(1).ToArray(), defaultObject);
                    Console.WriteLine(string.Join(" ", Environment.GetCommandLineArgs().Skip(1)));
                    if (defaultObject.LastParserState != null && defaultObject.LastParserState.Errors.Count > 0)
                    {
                        throw new Exception(defaultObject.LastParserState.Errors[0].ToString());
                    }
                }
                return defaultObject;
            }
        }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        // ReSharper disable once UnusedMember.Global
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}