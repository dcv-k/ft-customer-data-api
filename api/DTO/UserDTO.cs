using System.ComponentModel.DataAnnotations;


    public class UserDTO : LoginUserDTO
    {
        public ICollection<string> Roles {get; set;}
    }

