
namespace AppricotOffer
{
    public class ClientCommands
    {
        readonly string q, p, o, h;
        string d = "-", prevDashes;
        string[] allPartsOfPath;
        readonly FilesControls filesControls;
        readonly FoldersControls foldersControls;
        public ClientCommands(string q, string p, string o, string h)
        {
            if (!p.Contains("\\"))
                p = Directory.GetCurrentDirectory();
            allPartsOfPath = p.Split('\\');

            if (q != "-q") q = "";

            if (!o.Contains("\\"))
                o = Directory.GetCurrentDirectory() + "\\sizes-YYYY-MM-DD.txt";
            try
            {
                filesControls = new FilesControls(o);
                foldersControls = new FoldersControls();
            }
            catch (Exception ex)
            {
                filesControls = new FilesControls(Directory.GetCurrentDirectory() + "\\sizes-YYYY-MM-DD.txt");
            }
            filesControls.isHumanreadOn = h != "-h" ? false : true;
            this.q = q;
            this.p = p;
            this.o = o;
            this.h = h;
            RecursionMethod(allPartsOfPath[0] + "\\" + allPartsOfPath[1], d);
            filesControls.CloseSw();
        }

        public void RecursionMethod(string currPath, string _d = "")
        {
            string dashes = _d;
            try
            {
                DirectoryInfo di = new DirectoryInfo(currPath);
                if (di.GetDirectories().ToArray().Length != 0)
                {
                    var allDirectories = di.GetDirectories();
                    foreach (var item in allDirectories)
                    {
                        prevDashes = dashes;
                        if (q == "-q")
                        {
                            filesControls.FilesOutputToFile(item.FullName, dashes, prevDashes); 
                        }
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
                                    currPath = p;
                                RecursionMethod(nextDir[i].FullName, dashes);
                            }
                        }
                    }
                }
                else
                {
                    dashes += "-";
                    if (q == "-q")
                    {
                        filesControls.FilesOutputToFile(di.FullName, dashes, prevDashes);
                    }
                    else
                    {
                        filesControls.FilesOutputLog(di.FullName, dashes, prevDashes);
                        filesControls.FilesOutputToFile(di.FullName, dashes, prevDashes);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return;
        }
    }
}
