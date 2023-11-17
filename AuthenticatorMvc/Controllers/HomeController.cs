using AuthenticatorApp.Controllers;
using AuthenticatorApp.Model;
using AuthenticatorMvc.Models;
using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AuthenticatorMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthContext _authContext;
        public HomeController(AuthContext authContext)
        {
             _authContext = authContext;
        }




        [HttpGet()]
        public IActionResult Login()
        {
            return View();

        }


        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel model)
        {

            try
            {
                if (model.Email == "Hamed.niyazi5@gmail.com" && model.Password == "1")
                {
                    var userَAllActive =new Repository(_authContext).GetAllUsers().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password && u.IsEmailActive == true && u.IsQRActive == true);
                    if (userَAllActive is not null)
                    {
                        ViewBag.Message = "لاگین موفق امیز بود";
                        return View();
                    }


                    UserViewModel? userEmailIsnotActive = new Repository(_authContext).GetAllUsers().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password && u.IsEmailActive == false);
                    if (userEmailIsnotActive is not null)
                    {
                        ViewBag.Message = "لینک فعال سازی به ایمیلتان تان ارسال شد";
                        return View();
                    }



                    var userEmailIsActive = new Repository(_authContext).GetAllUsers().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password && u.IsEmailActive == true);
                    if (userEmailIsActive is null)
                    {
                        ViewBag.Message = "لینک فعال سازی به ایمیلتان تان ارسال شد";
                        return View();

                    }

                    var QR = AuthenticatorService.GenerateBarCode(userEmailIsActive.Email);
                     userEmailIsActive.QrCodeSetupCustomerSecretKey = QR.CustomerSecretKey;
 
                    var u=new Repository(_authContext).EditUser(userEmailIsActive);
                    userEmailIsActive = u;




                    //generate QR code and show it to user
                    var message = $"کد QR را اسکن کن        {QR.QrCodeSetupImageUrl}  \n\n\n \n   کد:   \n {QR.QrCodeSetupManualEntryKey}  ";

                    ViewBag.QrCodeSetupImageUrl = QR.QrCodeSetupImageUrl;
                    ViewBag.QrCodeSetupManualEntryKey = QR.QrCodeSetupManualEntryKey;



                    var m = new ValidationKeyViewmodel();
                    m.UserGuid = userEmailIsActive.UserGuid;
                    m.QrCodeSetupCustomerSecretKey = userEmailIsActive.QrCodeSetupCustomerSecretKey;
                    m.QrCodeSetupImageUrl = QR.QrCodeSetupImageUrl;
                    m.QrCodeSetupManualEntryKey = QR.QrCodeSetupManualEntryKey;
                    return RedirectToAction("QRPage", "Home", m);

                }
                else { throw new Exception(); }

            }
            catch (Exception)
            {
                return View();
                //  return BadRequest();
            }


        }

        [HttpGet("QRPage")]
        public IActionResult QRPage(ValidationKeyViewmodel validationKeyViewmodel)
        {
            return View(validationKeyViewmodel);
        }

        [HttpPost("QRPagePost")]
        public string QRPagePost(ValidationKeyViewmodel validationKeyViewmodel)
        {
            var userَ = new Repository(_authContext).GetAllUsers().FirstOrDefault(u => u.UserGuid == validationKeyViewmodel.UserGuid);

            if (userَ.IsEmailActive && userَ.IsQRActive == false/* && userَ.QrCodeSetupCustomerSecretKey == validationKeyViewmodel.QrCodeSetupCustomerSecretKey*/)
            {

                var twoFactor = new TwoFactorAuthenticator();

                var setupInfo = twoFactor.GenerateSetupCode(
    // name of the application - the name shown in the Authenticator
    "VdexTest",
    // an account identifier - shouldn't have spaces
    userَ.Email,
    // the secret key that also is used to validate later
    userَ.QrCodeSetupCustomerSecretKey,
    // Base32 Encoding (odd this was left in)
    false,
    // resolution for the QR Code - larger number means bigger image
    10);


                userَ.QrCodeSetupManualEntryKey = setupInfo.ManualEntryKey;
                userَ.QrCodeSetupImageUrl = setupInfo.QrCodeSetupImageUrl;
                //save user
                var u = new Repository(_authContext).EditUser(userَ);
                userَ = u;


                // Explicitly validate before saving

                if (twoFactor.ValidateTwoFactorPIN(userَ.QrCodeSetupCustomerSecretKey, validationKeyViewmodel.ValidationKey.ToString()))
                {


                    ViewBag.IsQRActiveMessage = "یوزر فعال شد";


                    //save user => IsQRActive==true


                }
                else
                {

                    ViewBag.IsQRActiveMessage = "کد ارسال شده اشتباه است";

                }



            }
            return ViewBag.IsQRActiveMessage as string;
        }
    }
}