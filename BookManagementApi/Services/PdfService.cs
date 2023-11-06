using BookManagementApi.Services.Interfaces;
using System.Text;
using UglyToad.PdfPig;

public class PdfService : IPdfService
{
    public string ExtractTextFromPdfPage(byte[] fileBytes)
    {
        StringBuilder text = new StringBuilder();

        using (PdfDocument document = PdfDocument.Open(fileBytes))
        {
            foreach (var page in document.GetPages())
            {
              
                text.AppendLine(page.Text);
            }
        }

        return text.ToString();
    }
}
