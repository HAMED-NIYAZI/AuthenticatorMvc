namespace AuthenticatorMvc.Models
{
    public class ValidationKeyViewmodel
    {
       
        public int ValidationKey { get; set; }
        public Guid UserGuid { get; set; }
        public string QrCodeSetupManualEntryKey { get; set; }
        public string QrCodeSetupImageUrl { get; set; }
        public string QrCodeSetupCustomerSecretKey { get; set; }
    }
}
