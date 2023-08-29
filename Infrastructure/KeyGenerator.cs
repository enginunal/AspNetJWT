using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace AspNetJWT.Infrastructure;

public class KeyGenerator
{
    /// <summary>
    /// Generate Private and Public key pairs
    /// </summary>
    /// <returns></returns>
    private string CreatePrivateAndPublicKey()
    {
        string KeyId = Guid.NewGuid().ToString("N");
        var key = RSA.Create(2048);
        SecurityKey privateKey = new RsaSecurityKey(key) { KeyId = KeyId };
        SecurityKey publicKey = new RsaSecurityKey(key.ExportParameters(includePrivateParameters: false)) { KeyId = KeyId };
        var keyBytesPrv = key.ExportRSAPrivateKey();
        var keyBytesPub = key.ExportRSAPublicKey();
        var keyStringPrv = Convert.ToBase64String(keyBytesPrv);
        var keyStringPub = Convert.ToBase64String(keyBytesPub);
        return keyStringPrv + " " + keyStringPub;
    }
}