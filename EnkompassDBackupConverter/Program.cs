// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Soroush Falahati (soroush@falahati.net)">
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal static class Program
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            try
            {
                string rootDirectory = CommandLineOptions.Default.BackupDirectory;
                if (string.IsNullOrWhiteSpace(rootDirectory))
                {
                    rootDirectory = Environment.CurrentDirectory;
                }
                if (!Directory.Exists(rootDirectory))
                {
                    throw new Exception(string.Format("Can not access {0}", rootDirectory));
                }
                string outputDirectory = CommandLineOptions.Default.OutputDirectory;
                if (string.IsNullOrWhiteSpace(outputDirectory))
                {
                    outputDirectory = Environment.CurrentDirectory;
                }
                if (!Directory.Exists(outputDirectory))
                {
                    throw new Exception(string.Format("Can not access {0}", outputDirectory));
                }
                if (!Directory.EnumerateDirectories(rootDirectory).Any())
                {
                    throw new Exception(string.Format("Nothing to do, directory is empty. ({0})", outputDirectory));
                }
                string[] includedDatabases = string.IsNullOrWhiteSpace(CommandLineOptions.Default.Databases)
                                                 ? null
                                                 : CommandLineOptions.Default.Databases.Split(',')
                                                       .Select(s => s.Trim().ToLower())
                                                       .ToArray();
                StreamWriter writer = null;
                foreach (string databaseDirectory in Directory.EnumerateDirectories(rootDirectory))
                {
                    string database = Path.GetFileName(databaseDirectory);
                    if (includedDatabases == null || includedDatabases.Contains(database.Trim().ToLower()))
                    {
                        if (!CommandLineOptions.Default.PerTableFile)
                        {
                            if (writer != null)
                            {
                                writer.Flush();
                                writer.Close();
                            }
                            writer = new StreamWriter(
                                Path.Combine(outputDirectory, string.Format("{0}.sql", database)),
                                false,
                                Encoding.UTF8);
                        }
                        foreach (string tableDirectory in Directory.EnumerateDirectories(databaseDirectory))
                        {
                            string table = Path.GetFileName(tableDirectory);
                            if (CommandLineOptions.Default.PerTableFile)
                            {
                                if (writer != null)
                                {
                                    writer.Flush();
                                    writer.Close();
                                }
                                writer =
                                    new StreamWriter(
                                        Path.Combine(outputDirectory, string.Format("{0}.{1}.sql", database, table)),
                                        false,
                                        Encoding.UTF8);
                            }
                            if (writer != null)
                            {
                                if (CommandLineOptions.Default.GenerateCreateScript
                                    && File.Exists(Path.Combine(tableDirectory, string.Format("{0}.Create", table))))
                                {
                                    Console.WriteLine("Generating Create: {0}.{1}", database, table);
                                    writer.WriteLine(
                                        File.ReadAllText(
                                            Path.Combine(tableDirectory, string.Format("{0}.Create", table))) + ";");
                                }
                                if (CommandLineOptions.Default.GenerateInsertScript
                                    && File.Exists(Path.Combine(tableDirectory, string.Format("{0}.Backup", table))))
                                {
                                    Console.WriteLine("Generating Backup: {0}.{1}", database, table);
                                    StreamReader reader =
                                        new StreamReader(
                                            Path.Combine(tableDirectory, string.Format("{0}.Backup", table)),
                                            Encoding.UTF8,
                                            true);
                                    while (!reader.EndOfStream)
                                    {
                                        string row = ReadRow(reader);
                                        if (row != null)
                                        {
                                            writer.WriteLine(
                                                "INSERT INTO {0} VALUES({1});",
                                                table,
                                                string.Join(
                                                    ",",
                                                    SplitColumn(row)
                                                        .Select(s => string.Format("'{0}'", EscapeToInsert(s)))
                                                        .ToArray()));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Skiped: {0}", database);
                    }
                }
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
                Console.WriteLine("Done. Press any key to exit.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private static string ReadRow(StreamReader reader)
        {
            int charCode = Convert.ToInt32(RowSeperator);
            string line = "";
            char lastChar = ' ';
            int currentCharCode;
            while (!reader.EndOfStream && ((currentCharCode = reader.Read()) != charCode || lastChar == '\\'))
            {
                lastChar = Convert.ToChar(currentCharCode);
                line += Convert.ToChar(currentCharCode);
            }
            return line;
        }

        private static IEnumerable<string> SplitColumn(string str)
        {
            int charCode = Convert.ToInt32(ColumnSeperator);
            List<string> parts = new List<string>();
            char lastChar = ' ';
            string item = string.Empty;
            foreach (char c in str)
            {
                if (c == charCode && lastChar != '\\')
                {
                    parts.Add(UnEscapeToRead(item));
                    item = string.Empty;
                }
                else
                {
                    lastChar = Convert.ToChar(c);
                    item += lastChar;
                }
            }
            parts.Add(UnEscapeToRead(item));
            return parts.ToArray();
        }

        private const char RowSeperator = (char)10;
        private const char ColumnSeperator = (char)9;
        private static string UnEscapeToRead(string value)
        {
            value = value.Replace("\\" + Convert.ToChar(ColumnSeperator), Convert.ToChar(ColumnSeperator).ToString(CultureInfo.InvariantCulture));
            value = value.Replace("\\" + Convert.ToChar(RowSeperator), Convert.ToChar(RowSeperator).ToString(CultureInfo.InvariantCulture));
            return value;
        }
        private static string EscapeToInsert(string value)
        {
            value = value.Replace("'", "\\'");
            value = value.Replace("\"", "\\\"");
            value = value.Replace("`", "\\`");
            value = value.Replace("ґ", "\\ґ");
            value = value.Replace("’", "\\’");
            value = value.Replace("‘", "\\‘");
            return value;
        }
    }
}