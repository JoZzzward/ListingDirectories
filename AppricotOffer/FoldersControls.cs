using AppricotOffer.Interface;

namespace AppricotOffer
{
    internal class FoldersControls : IFoldersControls
    {
        public long GetFolderSize(string currPath)
        {
            var di = new DirectoryInfo(currPath);
            var allFiles = di.GetFiles();
            long folderSize = 0;
            foreach (var obj in allFiles)
                folderSize += obj.Length;

            return folderSize;
        }
    }
}
