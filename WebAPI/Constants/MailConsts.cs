namespace WebAPI.Constants;

public static class MailConsts
{
    public static string InviteSubject => "Grup Daveti";
    public static string RegisterSubject => "Kayıt Linki";

    public static string InviteRegisteredBody =>
        "{0} adlı kullanıcıdan bir gruba davet aldınız. Kabul etmek için butona tıklayınız!";

    public static string InviteNotRegisteredBody =>
        "{0} adlı kullanıcıdan bir gruba davet aldınız. Kayıt Olmak için butona tıklayınız!";

    public static string RegisterBody => "Uygulamaya Hoşgeldiniz Kayıt olmak için Aşağıdaki Butona Tıklayınız!";


    public static string registerActionText => "Kayıt Ol";

    public static string ActionText => "Onayla";
}