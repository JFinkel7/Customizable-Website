using System.ComponentModel.DataAnnotations;

namespace MainActivity.Models {
    public class User {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }// CLASS ENDS 
}
