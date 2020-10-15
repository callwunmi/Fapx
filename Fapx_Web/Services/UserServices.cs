using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;

namespace Fapx_Web.Services
{
    public class UserServices
    {
        public async Task UpdateDomantUserStatus()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["FapxDb"].ToString();
                using (IDbConnection con = new SqlConnection(conn))
                {
                    var days = ConfigurationManager.AppSettings["dormantPeriod"];
                    int dt = 0;
                    int.TryParse(days, out dt);

                    var ct = ConfigurationManager.AppSettings["UserCount"];
                    int totalCt = 0;
                    int.TryParse(ct, out totalCt);
                    if (totalCt == 0)
                        totalCt = 100;

                    string sql = $@"select top {totalCt} id from users where (status is null and status = 0) and LastLoginDate < DATEADD(day, -{dt}, GETDATE())
                        order by LastLoginDate";

                    var ids = await con.QueryAsync<long>(sql);
                    if (!ids.Any())
                        return;

                    foreach (var n in ids)
                        await con.ExecuteAsync($"update users set status = 2 where id = {n}");
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message + " " + ex.StackTrace);
            }
        }

    }
}