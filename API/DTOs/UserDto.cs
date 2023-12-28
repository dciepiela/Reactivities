namespace API.DTOs
{
    /// <summary>
    /// Właściwości, które chcemy odesłać, gdy klient pomyślnie się zaloguje lub zarejestruje.
    /// </summary>
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
    }
}
