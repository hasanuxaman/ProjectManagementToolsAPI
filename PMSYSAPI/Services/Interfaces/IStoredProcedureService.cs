namespace PMSYSAPI.Services.Interfaces
{
    public interface IStoredProcedureService
    {
        Task<bool> AddCompanyUsingSp(string compname, string compShortname, int cmpGroupCod);
        Task<bool> AddProjectUsingSp(
            string projName, string projShortname, string projDesc,
            int projCompCod, int projPhaseCod, int projStsCod,
            DateTime projInitDate, DateTime projStrtPlndt, DateTime projEndPlandt,
            decimal projEstAmount);
    }
}