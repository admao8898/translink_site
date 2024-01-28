using OpenQA.Selenium;

namespace TranslinkSite.Locators
{
    public class TransitAlertPageLocators
    {
        public static readonly By TransitAlertsButton = By.LinkText("Sign in to Transit Alerts");
        public static readonly By NameField = By.Id("displayName");
        public static readonly By EmailField = By.Id("email");
        public static readonly By SubmitButton = By.Id("pagecolumnsrows_0_btnSubmit");
        public static readonly By SignUpLink = By.LinkText("Sign up now");

        public readonly string expectedSignInTransitAlertsAccountHeader = "Sign In To Your Transit Alerts Account";
        public readonly string signTranAlertHeaderFailMsg = "Transit Alerts Missing";
        public readonly string expectedSignUpTransAlertHeader = "Sign Up for Transit Alerts";
        public readonly string signUpTransAlertFailMsg = "Transit Alerts Sign up heaer missing";

        // Field Validation Error Messages 
        public readonly string expectedNameFailMsg = "Please enter a first name";
        public readonly string expectedEmailFailMsg = "Please enter a valid email address";
        public readonly string expectedPasswordFailMsg = "Your password must be at least 8 characters";
        public readonly string expectedTermsFailMsg = "You must agree to the terms in order to use this service";

        public readonly string nameFailMsgMissing = "Empty First Name Message Missing";
        public readonly string emailFailMsgMissing = "Empty Email Field Message Missing";
        public readonly string passwordFailMsgMissing = "Password Field Message Missing";
        public readonly string termsFailMsgMissing = "Terms & Conditions Empty Message Missing";

    }
}
