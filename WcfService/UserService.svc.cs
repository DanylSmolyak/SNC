using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using WcfService.Database;
using WcfService.Models;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.



    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _logFilePath = "C:\\Users\\smola\\SNC\\WcfService\\error_log.txt";

        public UserService()
        {
            _context = new ApplicationDbContext();
        }

        private void LogError(Exception ex)
        {
            File.AppendAllText(_logFilePath, $"{DateTime.Now}: {ex.Message} {ex.InnerException?.Message} {ex.StackTrace}\n");
        }

        public bool CreateUser(User user)
        {
            try
            {
                if (user == null) return false;

                user.ID = Guid.NewGuid();
                user.CreatedDate = DateTime.UtcNow;
                user.LastModifiedDate = DateTime.UtcNow;

                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public bool DeleteUser(Guid id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null) return false;

                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new List<User>();
            }
        }

        public User GetUserById(Guid id)
        {
            try
            {
                return _context.Users.Find(id);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var existingUser = _context.Users.Find(user.ID);
                if (existingUser == null) return false;

                existingUser.FullName = user.FullName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.TaxNumber = user.TaxNumber;
                existingUser.LastModifiedDate = DateTime.UtcNow;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }
    }

}
