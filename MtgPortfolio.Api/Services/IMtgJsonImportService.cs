namespace MtgPortfolio.API.Services
{
    public interface IMtgJsonImportService
    {
        bool ImportMtgJsonToDatabase(string filename);
    }
}