using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootFolder = @"C:\rootfolder"; // Change this to your root folder
            string outputFolder = Path.Combine(rootFolder, "MergedPDFs");
            string mergedPdfPath = Path.Combine(outputFolder, "MergedPDF.pdf");

            // Create a new folder to store merged PDFs
            Directory.CreateDirectory(outputFolder);

            // Find all PDF files in the root folder and its subfolders
            string[] pdfFiles = Directory.GetFiles(rootFolder, "*.pdf", SearchOption.AllDirectories);

            // Combine all PDFs into a single PDF
            CombinePDFs(pdfFiles, mergedPdfPath);

            Console.WriteLine("PDFs merged successfully!");
        }

        static void CombinePDFs(string[] pdfFiles, string outputPath)
        {
            using (FileStream stream = new FileStream(outputPath, FileMode.Create))
            {
                Document document = new Document();
                PdfCopy pdf = new PdfCopy(document, stream);
                document.Open();

                foreach (string file in pdfFiles)
                {
                    PdfReader reader = new PdfReader(file);
                    pdf.AddDocument(reader);
                    reader.Close();
                }

                document.Close();
            }
        }
    }
}
