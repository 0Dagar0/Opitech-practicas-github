namespace LibraryManagement.WebApi.Models
{
    public class CreateLoanRequest
    {
        public Guid BookCopyId { get; set; }
        public Guid UserId { get; set; }
    }
}