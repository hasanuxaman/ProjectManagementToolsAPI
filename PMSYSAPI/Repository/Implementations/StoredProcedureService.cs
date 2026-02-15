using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Services.Implementations
{
    public class StoredProcedureService:IStoredProcedureService
    {
        private readonly string _connectionString;

        public StoredProcedureService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> AddCompanyUsingSp(string compname, string compShortname, int cmpGroupCod)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_comp_add", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Compname", compname);
                    command.Parameters.AddWithValue("@CompShortname", compShortname);
                    command.Parameters.AddWithValue("@cmp_groupcod", cmpGroupCod);

                    await connection.OpenAsync();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }

        public async Task<bool> AddProjectUsingSp(
            string projName, string projShortname, string projDesc,
            int projCompCod, int projPhaseCod, int projStsCod,
            DateTime projInitDate, DateTime projStrtPlndt, DateTime projEndPlandt,
            decimal projEstAmount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_proj_add", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Proj_Name", projName);
                    command.Parameters.AddWithValue("@Proj_Shortname", projShortname);
                    command.Parameters.AddWithValue("@Proj_Desc", projDesc);
                    command.Parameters.AddWithValue("@Proj_compcod", projCompCod);
                    command.Parameters.AddWithValue("@Proj_Phasecod", projPhaseCod);
                    command.Parameters.AddWithValue("@Proj_StsCod", projStsCod);
                    command.Parameters.AddWithValue("@Proj_initDate", projInitDate);
                    command.Parameters.AddWithValue("@Proj_StrtPlndt", projStrtPlndt);
                    command.Parameters.AddWithValue("@Proj_EndPlandt", projEndPlandt);
                    command.Parameters.AddWithValue("@Proj_EstAmount", projEstAmount);

                    await connection.OpenAsync();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }
    }
}