namespace MoviesApi.Dtos
{
    public class dtoNewUser
    {
        [Required]
        public string userName { get; set; } = string.Empty;

        [Required]
        public string password { get; set; }

        [Required]
        public string email { get; set; } 

        public string? phoneNumber { get; set; }
    }
}
