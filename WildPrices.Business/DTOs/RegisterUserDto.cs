namespace WildPrices.Business.DTOs
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;
    }
}
