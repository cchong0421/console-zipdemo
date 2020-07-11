using System;
using System.IO;
using System.IO.Compression;

namespace zipdemo
{
    class Program
    {
        private static readonly string zippath = @"./";
        private static readonly string zipfile = zippath + DateTime.Now.ToString("yyyyMMdd") + ".zip";

        static void Main(string[] args)
        {
            // 如果壓縮檔已存在就刪除
            if (File.Exists(zipfile)) File.Delete(zipfile);

            // 產生要壓縮的測試檔案
            CreateSampleFiles();

            // 將檔案加入壓縮檔
            using (FileStream zipToOpen = new FileStream(zipfile, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    var files = Directory.GetFiles(zippath, "*txt");
                    ZipArchiveEntry entry;
                    foreach (string file in files)
                    {
                        entry = archive.CreateEntryFromFile(zipfile, Path.GetFileName(file));
                    }
                }
            }
        }

        public static void CreateSampleFiles()
        {
            string fileA = zippath + "aaa.txt";
            string fileB = zippath + "bbb.txt";
            string fileC = zippath + "ccc.txt";

            if (!File.Exists(fileA)) using (File.Create(fileA)) { }
            if (!File.Exists(fileB)) using (File.Create(fileB)) { }
            if (!File.Exists(fileC)) using (File.Create(fileC)) { }
        }
    }
}
