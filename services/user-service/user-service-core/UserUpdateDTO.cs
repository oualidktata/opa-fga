namespace user_service_core
{
    public record UserUpdateDTO
    {
        public UserUpdateDTO( string firstName, string lastName, string email)
        {
           
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}