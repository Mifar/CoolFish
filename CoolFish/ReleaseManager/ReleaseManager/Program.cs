﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    return;
                }

                var mainFileName = args[0];

                var version = FileVersionInfo.GetVersionInfo(mainFileName).FileVersion;
                var stream = File.Create("Release_" + version + ".zip");
                var archive = new ZipArchive(stream);
                archive.CreateEntryFromFile(mainFileName, mainFileName);
                for (int i = 1; i < args.Length; i++)
                {
                    archive.CreateEntryFromFile(args[i], args[i]);
                }
                archive.Dispose();
                stream.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }
        }
    }
}
