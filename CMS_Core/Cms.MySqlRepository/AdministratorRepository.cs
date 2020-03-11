using Cms.Domain.Models;
using Cms.IRepository;
using Dapper;
using System.Linq;

namespace Cms.MySqlRepository
{
    public class AdministratorRepository : BaseRepository, IAdministratorRepository
    {
        public Administrator Login(string _username, string _password)
        {
            using (var dbcon = CreateCmsConnection())
            {
                var model = dbcon.Query<Administrator>("SELECT * FROM cms.administrator where username=@username and password=@password limit 0,1", new { username = _username, password = _password }).SingleOrDefault();
                return model;
            }
        }
    }
}
