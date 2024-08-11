namespace HavaDurumu.Modeller
{
    public class ApiKullanicilari
    {
        public static List<ApiKullanicisi> Kullanicilar = new()
       {
           new ApiKullanicisi { Id = 1 , KullaniciAdi = "Oğuzhan", Sifre = "1905", Rol = "Yönetici"},
           new ApiKullanicisi {Id = 2 ,KullaniciAdi = "Nurullah" , Sifre = "1907", Rol ="StandartKullanici"}
       };
    }
}
