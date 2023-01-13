namespace user_service_core
{
    public record UserDTO
    {
        public UserDTO(string id, string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException($"'{nameof(firstName)}' cannot be null or whitespace.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException($"'{nameof(lastName)}' cannot be null or whitespace.", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}