using System;
using System.IO;
using System.Linq;

//SharpCompress - https://github.com/adamhathcock/sharpcompress
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;

namespace extract
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                    throw new ArgumentNullException("File", "File missing.");

                string filename = args[0];
                string directory = Directory.GetCurrentDirectory();
                string filenameNoExt = Path.GetFileNameWithoutExtension($"{Directory.GetCurrentDirectory()}\\{filename}");
                Console.WriteLine($"Filename: {filename} \n" + 
                                  $"Saving to: {directory}\\{filenameNoExt}");

                if (!Directory.Exists($"{directory}\\{filenameNoExt}"))
                    Directory.CreateDirectory($"{directory}\\{filenameNoExt}");

                using (var archive = ArchiveFactory.Open(filename))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory($"{directory}\\{filenameNoExt}\\", new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
            catch (ArgumentNullException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
        }
    }
}
