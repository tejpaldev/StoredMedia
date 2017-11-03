using StoreyedMedia.DAL.Extensions;
using StoreyedMedia.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StoreyedMedia.DAL.Repositories
{
    public class UserRepository : Repository<User>
    {
        private readonly DbContext _context;
        public UserRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }


        public IList<User> GetUsers()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "exec [dbo].[uspGetUsers]";

                return  ToList(command).ToList();
            }
        }

        public User CreateUser(User user)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "uspSignUp";

                command.Parameters.Add(command.CreateParameter("@pFirstName", user.FirstName)); 
                command.Parameters.Add(command.CreateParameter("@pLastName", user.LastName));
                command.Parameters.Add(command.CreateParameter("@pUserName", user.UserName));
                command.Parameters.Add(command.CreateParameter("@pPassword", user.Password));
                command.Parameters.Add(command.CreateParameter("@pEmail", user.Email));

                return  ToList(command).FirstOrDefault();

              
            }
           
        }


        public User LoginUser(string id, string password)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "uspSignIn";

                command.Parameters.Add(command.CreateParameter("@pId", id));
                command.Parameters.Add(command.CreateParameter("@pPassword", password));

                return  ToList(command).FirstOrDefault();
            }
        }


        public User GetUserByUsernameOrEmail(string username,string email)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "uspGetUserByUsernameOrEmail";

                command.Parameters.Add(command.CreateParameter("@pUsername", username));
                command.Parameters.Add(command.CreateParameter("@pEmail", email));

                return  ToList(command).FirstOrDefault();
            }
        }

        
    }
}
