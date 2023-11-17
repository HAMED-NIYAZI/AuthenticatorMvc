using AuthenticatorApp.Model;
using Google.Authenticator;
using System.Reflection;

namespace AuthenticatorApp.Controllers
{
    public  static class AuthenticatorService
    {

        public static QRViewmodel GenerateBarCode(string customerEmail) {

            try
            {
                var twoFactor = new TwoFactorAuthenticator();

                // We need a unique PER USER key to identify this Setup
                // must be saved: you need this value later to verify a validation code
                var customerSecretKey = Guid.NewGuid().ToString();

                var setupInfo = twoFactor.GenerateSetupCode(
                    // name of the application - the name shown in the Authenticator
                    "VdexTest",
                    // an account identifier - shouldn't have spaces
                    customerEmail,
                    // the secret key that also is used to validate later
                    customerSecretKey,
                    // Base32 Encoding (odd this was left in)
                    false,
                    // resolution for the QR Code - larger number means bigger image
                    10);



                var NewQr = new QRViewmodel();

                // a string key
                //   model.TwoFactorSetupKey = setupInfo.ManualEntryKey;//user can see on ui
                NewQr.QrCodeSetupManualEntryKey = setupInfo.ManualEntryKey;
                // a base64 formatted string that can be directly assigned to an img src
                // model.QrCodeImageData = setupInfo.QrCodeSetupImageUrl;//user can see on ui
                NewQr.QrCodeSetupImageUrl = setupInfo.QrCodeSetupImageUrl;

                // Store the key with the user/customer (app sepecific)
                // customer.TwoFactorKey = customerSecretKey;  //should save in db
                NewQr.CustomerSecretKey = customerSecretKey;

                return NewQr;
            }
            catch (Exception)
            {

                throw;
            }

        
        }
    }
}
