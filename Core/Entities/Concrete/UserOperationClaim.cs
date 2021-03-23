namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }//hangi tablonun id'si
        public int OperationClaimId { get; set; }
    }
}
