﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDF.Common;
using EDF.DL;

namespace EDF.UI
{
    class BatchFileLoad
    {
        private static OpenFileDialog OpenFileDialog { get; set; } = OpenFileDialog = new OpenFileDialog()
        {
            FileName = "Select a file",
            Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv",
            Title = "Open A List of Drawings"
        };

        public static bool Get()
        {
            DialogResult result = OpenFileDialog.ShowDialog();
            if (result.ToString() == "OK")
            {
                Log.Write.Debug(result);

                bool isCSVFile = Path.GetExtension(OpenFileDialog.FileName).Equals(".csv") ? true : false;

                List<string> drawings = new List<string>();

                BatchReference.BatchFileTextBoxReference.Text = OpenFileDialog.FileName;

                drawings = Data.BatchPrintLoadFile(isCSVFile, OpenFileDialog.FileName).ToList();

                BatchDataGrid.LoadedDrawingList = drawings;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
