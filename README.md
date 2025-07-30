Settings Configuration
Update the following settings to configure the application correctly:

{IntegrationKey}
The integration key (client ID) of your application created under the "Apps and Keys" section in your DocuSign developer account.

Private RSA Key
Save the generated private RSA key (refer to the "Prerequisites" section) in the following path:

\sample-app-MyWebForms-csharp\Docusign.MyWebForms\Docusign.MyWebForms\private.key
{UserId}
The GUID of your DocuSign test user (also found in the "Apps and Keys" section).

{AccountId}
The account ID linked to your test user.

{RedirectUri}
The redirect URI used for embedded signing.
For local development, set this to:

http://localhost:5000/sign/completed
