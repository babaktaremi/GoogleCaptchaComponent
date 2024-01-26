namespace GoogleCaptchaComponent.Models;

public class CaptchaLanguages
{
    public string Language { get; }

    private CaptchaLanguages(string language)
    {
        Language = language;
    }

    public static CaptchaLanguages Arabic => new("ar");
    public static CaptchaLanguages Afrikaans => new("af");
    public static CaptchaLanguages Amharic => new("am");
    public static CaptchaLanguages Armenian => new("hy");
    public static CaptchaLanguages Azerbaijani => new("az");
    public static CaptchaLanguages Basque => new("eu");
    public static CaptchaLanguages Bengali => new("bn");
    public static CaptchaLanguages Bulgarian => new("bg");
    public static CaptchaLanguages Catalan => new("ca");
    public static CaptchaLanguages ChineseHongKong => new("zh-HK");
    public static CaptchaLanguages ChineseSimplified => new("zh-CN");
    public static CaptchaLanguages ChineseTraditional => new("zh-TW");
    public static CaptchaLanguages Croatian => new("hr");
    public static CaptchaLanguages Czech => new("cs");
    public static CaptchaLanguages Danish => new("da");
    public static CaptchaLanguages Dutch => new("nl");
    public static CaptchaLanguages EnglishUK => new("en-GB");
    public static CaptchaLanguages English => new("en");
    public static CaptchaLanguages Estonian => new("et");
    public static CaptchaLanguages Filipino => new("fil");
    public static CaptchaLanguages Finnish => new("fi");
    public static CaptchaLanguages French => new("fr");
    public static CaptchaLanguages FrenchCanadian => new("fr-CA");
    public static CaptchaLanguages Galician => new("gl");
    public static CaptchaLanguages Georgian => new("ka");
    public static CaptchaLanguages German => new("de");
    public static CaptchaLanguages GermanAustria => new("de-AT");
    public static CaptchaLanguages GermanSwitzerland => new("de-CH");
    public static CaptchaLanguages Greek => new("el");
    public static CaptchaLanguages Gujarati => new("gu");
    public static CaptchaLanguages Hebrew => new("iw");
    public static CaptchaLanguages Hindi => new("hi");
    public static CaptchaLanguages Hungarian => new("hu");
    public static CaptchaLanguages Icelandic => new("is");
    public static CaptchaLanguages Indonesian => new("id");
    public static CaptchaLanguages Italian => new("it");
    public static CaptchaLanguages Japanese => new("ja");
    public static CaptchaLanguages Kannada => new("kn");
    public static CaptchaLanguages Korean => new("ko");
    public static CaptchaLanguages Laothian => new("lo");
    public static CaptchaLanguages Latvian => new("lv");
    public static CaptchaLanguages Lithuanian => new("lt");
    public static CaptchaLanguages Malay => new("ms");
    public static CaptchaLanguages Malayalam => new("ml");
    public static CaptchaLanguages Marathi => new("mr");
    public static CaptchaLanguages Mongolian => new("mn");
    public static CaptchaLanguages Norwegian => new("no");
    public static CaptchaLanguages Persian => new("fa");
    public static CaptchaLanguages Polish => new("pl");
    public static CaptchaLanguages Portuguese => new("pt");
    public static CaptchaLanguages PortugueseBrazil => new("pt-BR");
    public static CaptchaLanguages PortuguesePortugal => new("pt-PT");
    public static CaptchaLanguages Romanian => new("ro");
    public static CaptchaLanguages Russian => new("ru");
    public static CaptchaLanguages Serbian => new("sr");
    public static CaptchaLanguages Sinhalese => new("si");
    public static CaptchaLanguages Slovak => new("sk");
    public static CaptchaLanguages Slovenian => new("sl");
    public static CaptchaLanguages Spanish => new("es");
    public static CaptchaLanguages SpanishLatinAmerica => new("es-419");
    public static CaptchaLanguages Swahili => new("sw");
    public static CaptchaLanguages Swedish => new("sv");
    public static CaptchaLanguages Tamil => new("ta");
    public static CaptchaLanguages Telugu => new("te");
    public static CaptchaLanguages Thai => new("th");
    public static CaptchaLanguages Turkish => new("tr");
    public static CaptchaLanguages Ukrainian => new("uk");
    public static CaptchaLanguages Urdu => new("ur");
    public static CaptchaLanguages Vietnamese => new("vi");
    public static CaptchaLanguages Zulu => new("zu");
}