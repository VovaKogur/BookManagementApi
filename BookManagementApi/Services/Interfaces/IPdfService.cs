namespace BookManagementApi.Services.Interfaces
{
    public interface  IPdfService
    {
        public string ExtractTextFromPdfPage(byte[] fileBytes);
    }
}
