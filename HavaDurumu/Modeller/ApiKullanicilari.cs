namespace HavaDurumu.Modeller
{
    public class ApiKullanicilari
    {
        public static List<ApiKullanicisi> Kullanicilar = new()
       {
           new ApiKullanicisi { Id = 1 , KullaniciAdi = "Oguzhan", Sifre = "123456", Rol = "Yönetici"},
           new ApiKullanicisi {Id = 2 ,KullaniciAdi = "Nurullah" , Sifre = "1907", Rol ="StandartKullanici"}
       };
    }
}
