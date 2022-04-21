namespace AppricotOffer.Interface
{
    internal interface IFilesControls
    {
        void CloseSw();
        void FilesOutputLog(string item, string dashes, string prevDashes);
        void FilesOutputToFile(string item, string dashes, string prevDashes);
        string SizeFiltering(double num);
    }
}