using System.ComponentModel.DataAnnotations;

namespace AuthenticatorApp.Model
{
    public class UserViewModel
    {
        [Key]
        public Guid UserGuid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailActive { get; set; }
        public bool IsQRActive { get; set; }
 

        public string? QrCodeSetupManualEntryKey { get; set; }
        public string? QrCodeSetupImageUrl { get; set; }
        public string? QrCodeSetupCustomerSecretKey { get; set; }


        //public List<UserViewModel> GetAllUsers()
        //{

        //    var users = new List<UserViewModel>() {
        //new UserViewModel() { UserGuid=Guid.Parse("5BB243A6-420A-440F-B642-A7A277852FB3"),Email = "Hamed.niyazi5@gmail.com", Password = "1" ,IsEmailActive=true,IsQRActive=false, QrCodeSetupImageUrl="",QrCodeSetupManualEntryKey="",QrCodeSetupCustomerSecretKey=""},
        //new UserViewModel() {  UserGuid=Guid.Parse("6086698C-3881-424E-AD5C-273A27819B16"),Email = "Hamed.niyazi6@gmail.com", Password = "1" ,IsEmailActive=false,IsQRActive=false, QrCodeSetupImageUrl="",QrCodeSetupManualEntryKey="",QrCodeSetupCustomerSecretKey=""},
        //new UserViewModel() {UserGuid = Guid.Parse("7E3F55C3-A445-40D3-AE77-5E6449B0FFE0"),  Email = "Hamed.niyazi7@gmail.com", Password = "1" ,IsEmailActive=false,IsQRActive=false, QrCodeSetupImageUrl="",QrCodeSetupManualEntryKey="",QrCodeSetupCustomerSecretKey=""},
        //};

        //    return users;
        //}


        //public UserViewModel GetUser(Guid userGuid)
        //{

        //    var users = GetAllUsers();
        //    var user = users.FirstOrDefault(u => u.UserGuid == userGuid);
        //    return user;

        //}


    }
}
