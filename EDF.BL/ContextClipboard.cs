﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace eDrawingFinder
{
    class ContextClipboard
    {
        public static void CopyPartNumber()
        {
            IEnumerator<string> drawings = DrawingStorage.GetSelectedDrawings(MainUI.DataGridReference);
            string items = string.Empty;
            while (drawings.MoveNext())
            {

                items += $"{Path.GetFileNameWithoutExtension(drawings.Current)}\n";
            }

            Clipboard.SetText(items.Trim());

        }
        public static void CopyDrawingFileName()
        {
            IEnumerator<string> drawings = DrawingStorage.GetSelectedDrawings(MainUI.DataGridReference);
            string items = string.Empty;
            while (drawings.MoveNext())
            {

                items += $"{Path.GetFileName(drawings.Current)}\n";
            }

            Clipboard.SetText(items.Trim());

        }
        public static void CopyFilePath()
        {
            IEnumerator<string> drawings = DrawingStorage.GetSelectedDrawings(MainUI.DataGridReference);
            string items = string.Empty;
            while (drawings.MoveNext())
            {

                items += $"{drawings.Current}\n";
            }

            Clipboard.SetText(items.Trim());

        }

        public static void OpenWithFileExplorer()
        {
            IEnumerator<string> drawings = DrawingStorage.GetSelectedDrawings(MainUI.DataGridReference);

            HashSet<string> paths = new HashSet<string>();
            while (drawings.MoveNext())
            {
                paths.Add(drawings.Current);
            }
            
            foreach (string path in paths)
            {
                //Process.Start(path);

                Process.Start("explorer.exe", $"/select, \"{path}\"");
            }
        }
    }
}