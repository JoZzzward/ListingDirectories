namespace AppricotOffer
{
    public class ClientCommands
    {
        readonly string quite, path, output, humanread;
        string _dashes = "-", prevDashes;
        string[] allPartsOfPath;
        readonly FilesControls filesControls;
        readonly FoldersControls foldersControls;
        public ClientCommands(string quite, string path, string output, string humanread)
        {
            if (!path.Contains("\\"))
                path = Directory.GetCurrentDirectory();
            allPartsOfPath = path.Split('\\');

            if (quite != "-q") quite = "";

            if (!output.Contains("\\"))
                output = Directory.GetCurrentDirectory() + "\\sizes-YYYY-MM-DD.txt";
            try
            {
                filesControls = new FilesControls(output);
                foldersControls = new FoldersControls();
            }
            catch (Exception ex)
            {
                filesControls = new FilesControls(Directory.GetCurrentDirectory() + "\\sizes-YYYY-MM-DD.txt");
            }
            filesControls.isHumanreadOn = humanread != "-h" ? false : true;
            this.quite = quite;
            this.path = path;
            this.output = output;
            this.humanread = humanread;
            FileSearch(allPartsOfPath[0] + "\\" + allPartsOfPath[1], _dashes);
            filesControls.CloseSw();
        }

        public void FileSearch(string currentPath, string _dashes = "")
        {
            string dashes = _dashes;
            try
            {
                DirectoryInfo di = new DirectoryInfo(currentPath);
                if (di.GetDirectories().ToArray().Length != 0)
                {
                    var allDirectories = di.GetDirectories();
                    foreach (var item in allDirectories)
                    {
                        if (item.Name == "admin")
                            continue;

                        prevDashes = dashes;

                        if (quite == "-q")
                            filesControls.FilesOutputToFile(item.FullName, dashes, prevDashes);
                        else
                        {
                            filesControls.FilesOutputLog(item.FullName, dashes, prevDashes);
                            filesControls.FilesOutputToFile(item.FullName, dashes, prevDashes);
                        }
                        var nextDir = item.GetDirectories();
                        if (nextDir.Length != 0)
                        {
                            for (int i = 0; i < nextDir.Length; i++)
                            {
                                if (nextDir[i] is null)
                                    currentPath = path;
                                FileSearch(nextDir[i].FullName, dashes);
                            }
                        }
                    }
                }
                else
                {
                    dashes += "-";
                    if (quite == "-q")
                        filesControls.FilesOutputToFile(di.FullName, dashes, prevDashes);
                    else
                    {
                        filesControls.FilesOutputLog(di.FullName, dashes, prevDashes);
                        filesControls.FilesOutputToFile(di.FullName, dashes, prevDashes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }
    }
}
