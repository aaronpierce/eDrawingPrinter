﻿using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using EDF.Common;

namespace EDF.DL
{
    /// <summary>
    /// Class to reference loading/saving data
    /// </summary>
    public static class Data
    {
    
        static Data()
        {

            // Establishing disk file paths
            string appDataFolder = Environment.GetEnvironmentVariable("LOCALAPPDATA");
            string programAppDataFolder = Path.Combine(appDataFolder, "eDrawingFinder");
            string opDrawingDataFile = Path.Combine(programAppDataFolder, "OPDrawingPaths.json");
            string bmDrawingDataFile = Path.Combine(programAppDataFolder, "BMDrawingPaths.json");

            if (!Directory.Exists(programAppDataFolder))
            {
                Directory.CreateDirectory(programAppDataFolder);
            }

            ProgramFolder = programAppDataFolder;
            OPDrawingDataFile = opDrawingDataFile;
            BMDrawingDataFile = bmDrawingDataFile;

        }

        public static string ProgramFolder { get; }
        public static string OPDrawingDataFile { get; }
        public static string BMDrawingDataFile { get; }

        // If the required json files don't exists, make them.
        //public static void PreCheckDataGridLoad()
        //{
        //    // If either dont exists, tell the user whats happening.
        //    if (!File.Exists(OPDrawingDataFile) || !File.Exists(BMDrawingDataFile))
        //    {
        //        PreLoadMessage.ShowMessageBox("Database being created. This is a one time process.\n\nExit this window once the application starts.", "Please Wait.");
        //    }


        //    if (!File.Exists(OPDrawingDataFile))
        //    {
        //        List<string> exclusions = new List<string> { "BM" };
        //        DirectoryScan.DirectorySearch(@"\\pokydata1\CAD\DWG", exclusions, DrawingGroup.OP);
        //    }

        //    if (!File.Exists(BMDrawingDataFile))
        //    {
        //        List<string> exclusions = new List<string> { "" };
        //        DirectoryScan.DirectorySearch(@"\\pokydata1\CAD\DWG\BM", exclusions, DrawingGroup.BM);
        //    }

        //    // Loads json files to Data Grid for UI 
        //    DataGrid.Load();

        //    //Thread t = new Thread(() => PostDataGridLoad());
        //    //t.Start();
        //}

        public static bool UpdateAvailable { get; set; } = false;

        // Runs every start after the inital loading to update datasets everytime.
        //public static void PostDataGridLoad()
        //{
        //    List<string> exclusions = new List<string> { "BM" };
        //    DirectoryScan.DirectorySearch(@"\\pokydata1\CAD\DWG", exclusions, DrawingGroup.OP);

        //    exclusions = new List<string> { "" };
        //    DirectoryScan.DirectorySearch(@"\\pokydata1\CAD\DWG\BM", exclusions, DrawingGroup.BM);

        //    // Tells program an updated set of files are available
        //    UpdateAvailable = true;

        //    // Reloads DataGrid from disk
        //    DataGrid.Load();
        //}

        // Function for saving directory scan results to disk as json file.
        public static void SaveJson(Dictionary<string, string> drawingDictionary, DrawingGroup group)
        {
            string datafile = (group == DrawingGroup.OP) ? OPDrawingDataFile : BMDrawingDataFile;
            using (StreamWriter file = new StreamWriter(datafile, false))
            {
                file.Write(JsonConvert.SerializeObject(drawingDictionary, Formatting.Indented));
            }
        }

        /// <summary>
        /// Function to load json from disk to dictionary.
        /// </summary>
        /// <param name="group">Determines which group to load from disk.</param>
        /// <returns></returns>
        public static Dictionary<string, string> LoadJson(DrawingGroup group)
        {
            string datafile = (group == DrawingGroup.OP) ? OPDrawingDataFile : BMDrawingDataFile;
            using (StreamReader file = new StreamReader(datafile))
            {
                Dictionary<string, string> pathDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(file.ReadToEnd());
                return pathDictionary;
            }
        }

        public static IEnumerable<string> BatchPrintLoadFile(bool isCSVFile, string filename)
        {
            List<string> drawings = new List<string>();

            using (StreamReader fileReader = new StreamReader(filename))
            using (CsvReader csvReader = new CsvReader(fileReader))
            {
                if (!isCSVFile)
                {
                    string line = string.Empty;
                    string cleaned = string.Empty;

                    while ((line = fileReader.ReadLine()) != null)
                    {
                        cleaned = line.TrimEnd(',').Trim();
                        drawings.Add(cleaned);
                    }
                }
                else
                {
                    while (csvReader.Read())
                    {
                        drawings.Add(csvReader.GetField<string>(0));
                    }
                }

            }

            return drawings;

        }




    }
}