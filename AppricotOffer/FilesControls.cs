using AppricotOffer.Interface;

namespace AppricotOffer
{
    internal class FilesControls : IFilesControls
    {
        readonly StreamWriter sw;
        readonly FoldersControls foldersControls;
        public bool isHumanreadOn;
        public FilesControls(string o)
        {
            sw = new StreamWriter(o);
            foldersControls = new FoldersControls();
        }
        public void FilesOutputToFile(string item,
                                      string dashes,
                                      string prevDashes)
        {
            var di = new DirectoryInfo(item);
            var allFiles = di.GetFiles();

            prevDashes = dashes;
            sw.WriteLine($"{dashes} {di.Name} {SizeFiltering(foldersControls.GetFolderSize(di.FullName))}");
            foreach (var obj in allFiles)
            {
                dashes += "-";
                sw.WriteLine($"{dashes} {obj.Name} {SizeFiltering(obj.Length)}");
                dashes = prevDashes;
            }
        }
        public void FilesOutputLog(string item,
                                   string dashes,
                                   string prevDashes)
        {
            DirectoryInfo di = new DirectoryInfo(item);
            var allFiles = di.GetFiles();

            prevDashes = dashes;
            Console.WriteLine($"{dashes} {di.Name} {SizeFiltering(foldersControls.GetFolderSize(di.FullName))}");
            foreach (var obj in allFiles)
            {
                dashes += "-";
                Console.WriteLine($"{dashes} {obj.Name} {SizeFiltering(obj.Length)}");
                dashes = prevDashes;
            }
        }

        public string SizeFiltering(double num)
        {
            if (isHumanreadOn)
            {
                long tb = 1099511627776;
                long gb = 1073741824;
                long mb = 1048576;
                long kb = 1024;
                double bytes;
                if (tb > num & num > gb)
                {
                    bytes = num / gb;
                    return $"({bytes:0.00} гигабайт)";
                }
                else if (gb > num & num > mb)
                {
                    bytes = num / mb;
                    return $"({bytes:0.00} мегабайт)";
                }
                else if (mb > num & num > kb)
                {
                    bytes = num / kb;
                    return $"({bytes:0.00} килобайт)";
                }
                else
                {
                    return $"({num:0.00} байт)";
                }
            }
            else return string.Empty;
        }

        public void CloseSw() =>
            sw.Close();
    }
}
