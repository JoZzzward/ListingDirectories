using AppricotOffer.Interface;

namespace AppricotOffer
{
    internal class FoldersControls : IFoldersControls
    {
        public long GetFolderSize(string currPath)
        {
            var currentPath = new DirectoryInfo(currPath);
            var allFiles = currentPath.GetFiles();
            long folderSize = 0;
            foreach (var item in allFiles)
                folderSize += item.Length;

            return folderSize;
        }
    }
}
