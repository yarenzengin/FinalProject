using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);//join atıcaz(userın sisteme girmesi vb + vt den ope.claimlerini çekmek istiyorum)
    }
}
