using HavaDurumu.Modeller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HavaDurumu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Yonetici,StandartKullanici")]
    public class KimlikDenetimiController : ControllerBase
    {
        private readonly JwtAyarlari _jwtAyarlari;
        public KimlikDenetimiController(IOptions<JwtAyarlari> jwtAyarlari)
        {
                _jwtAyarlari = jwtAyarlari.Value;
        }

        [AllowAnonymous]
        [HttpPost("Giris")]
        public IActionResult Giris([FromBody] ApiKullanicisi apiKullanicisiBilgileri)
        {
            var apiKullanicisi = KimlikDenetimiYap(apiKullanicisiBilgileri);
            if (apiKullanicisi == null) return NotFound("Kimlik Bulunmadı");

            var token = TokenOlustur(apiKullanicisi);
            return Ok(token);
        }

        private string TokenOlustur(ApiKullanicisi apiKullanicisi)
        {
            if (_jwtAyarlari.Key == null) throw new Exception("Jwt ayrlarındaki Key değeri null olamaz.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAyarlari.Key));
            var credentionals = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256);

            var claimDizisi = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, apiKullanicisi.KullaniciAdi!),
                new Claim(ClaimTypes.Role, apiKullanicisi.Rol!)
            };

            var token = new JwtSecurityToken(_jwtAyarlari.Issuer , _jwtAyarlari.Audience,claimDizisi,expires:DateTime.Now.AddHours(1),
              signingCredentials: credentionals) ;
            
            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }

        private ApiKullanicisi? KimlikDenetimiYap(ApiKullanicisi apiKullanicisiBilgileri)
        {
            return ApiKullanicilari.
                Kullanicilar
                .FirstOrDefault(x =>
                x.KullaniciAdi?.ToLower() == apiKullanicisiBilgileri.KullaniciAdi && x.Sifre == apiKullanicisiBilgileri.Sifre);
        }
    }
}
