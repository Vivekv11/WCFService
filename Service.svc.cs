using System.Collections.Generic;
using System.Linq;
using WCFService.DAL.Entities;
using WCFService.DTOs;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        private readonly DBContext DBContext = new DBContext();
        
        public List<UserDTO> Get()
        {
            return DBContext.User.Select(
                s => new UserDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Username = s.Username,
                    Password = s.Password,
                    EnrollmentDate = s.EnrollmentDate
                }
            ).ToList();
        }

        public UserDTO GetUserById(int Id)
        {
            return DBContext.User.Select(
                    s => new UserDTO
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Username = s.Username,
                        Password = s.Password,
                        EnrollmentDate = s.EnrollmentDate
                    })
                .FirstOrDefault(s => s.Id == Id);
        }
        
        public bool InsertUser(UserDTO User)
        {
            var entity = new User()
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Username = User.Username,
                Password = User.Password,
                EnrollmentDate = User.EnrollmentDate
            };

            DBContext.User.Add(entity);
            DBContext.SaveChangesAsync();

            return true;
        }

        public void UpdateUser(UserDTO User)
        {
            var entity = DBContext.User.FirstOrDefault(s => s.Id == User.Id);

            entity.FirstName = User.FirstName;
            entity.LastName = User.LastName;
            entity.Username = User.Username;
            entity.Password = User.Password;
            entity.EnrollmentDate = User.EnrollmentDate;

            DBContext.SaveChangesAsync();
        }

        public void DeleteUser(int Id)
        {
            var entity = new User()
            {
                Id = Id
            };

            DBContext.User.Attach(entity);
            DBContext.User.Remove(entity);
            DBContext.SaveChangesAsync();
        }
    }
}