using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.Models;


namespace WcfService
{
[ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        User GetUserById(Guid id);

        [OperationContract]
        bool CreateUser(User user);

        [OperationContract]
        bool UpdateUser(User user);

        [OperationContract]
        bool DeleteUser(Guid id);
    }

}
