using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserCenter.WebApi.Common
{
    /// <summary>
    /// JWT
    /// </summary>
    public static class JWTHelper
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public static string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";//不要泄露
        
        /// <summary>
        /// 进行jwt加密（Head base64位，Signature进行HMACSHA256加密的 ）
        /// </summary>
        /// <param name="payload">加密的主体对象</param>
        /// <returns></returns>
        public static string JWTEncry(Dictionary<string, object> payload)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return token;
        }

      /// <summary>
      /// JWT解密
      /// </summary>
      /// <param name="token"></param>
      /// <param name="secret"></param>
      /// <returns></returns>
        public static string JWTDecry(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                var json = decoder.Decode(token, secret, verify: true);
                return json;
            }
            catch (TokenExpiredException ex)
            {
                return "Token has expired,"+ex.Message;
            }
            catch (SignatureVerificationException ex)
            {
                return "Token has invalid signature," + ex.Message;
            }
        }
    }
}