using AuthenticatorApp.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthenticatorMvc.Controllers
{
 
    public class Repository
    {

        private readonly AuthContext context;
        private readonly DbSet<UserViewModel> dbSet;

        public Repository(AuthContext authContext)
        {

            context = authContext;
            this.dbSet = this.context.Set<UserViewModel>();


        }

        public List<UserViewModel> GetAllUsers() {

          return  context.Users.ToList();           
                }



        public UserViewModel EditUser(UserViewModel userViewModel)
        {
            var user = context.Users.FirstOrDefault(u=>u.Email == userViewModel.Email);
            if (user == null) { return new UserViewModel(); }

            user.QrCodeSetupManualEntryKey = userViewModel.QrCodeSetupManualEntryKey;
            user.QrCodeSetupImageUrl = userViewModel.QrCodeSetupImageUrl;
            user.QrCodeSetupCustomerSecretKey = userViewModel.QrCodeSetupCustomerSecretKey;
            user.IsEmailActive = userViewModel.IsEmailActive;
            user.IsQRActive= userViewModel.IsQRActive;


            dbSet.Entry(user).State = EntityState.Modified;
            dbSet.Update(user);

            context.SaveChanges();
            return user;

        }


        public UserViewModel GetUser(string Email )
        {
            var user = context.Users.FirstOrDefault(u => u.Email == Email);
            if (user == null) { return new UserViewModel(); }
            return user;

        }


        public UserViewModel Add(UserViewModel entity)
        {
              context.Add<UserViewModel>(entity);
            context.SaveChanges();

          return  GetUser(entity.Email);

        }


    }
}
