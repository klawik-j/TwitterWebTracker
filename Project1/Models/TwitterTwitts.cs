using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    /// <summary>
    /// Klasa wygenerowana automatycznie podstawie formatu json zwracanego rzez API Twitter'a. Zawiera informacje o Twitt'ach.
    /// </summary>
    /// <item>
    /// <term>Data</term>
    /// <description>Lista szczegolowych danych o twittcie</description>
    /// </item>
    /// <item>
    /// <term>Meta</term>
    /// <description>Meta dane</description>
    /// </item>
    public class TwitterTwitts
    {
        public Datum[] Data { get; set; }
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Klasa wygenerowana automatycznie podstawie formatu json zwracanego rzez API Twitter'a. Zawiera Meta dane
    /// </summary>
    /// <item>
    /// <term>Oldest_id</term>
    /// <description>Numer identyfikacyjny za pomoca ktorego mozna wysylac zapytania o starszy zbior danych</description>
    /// </item>
    /// <iterm>
    /// <term>Newest_id</term>
    /// <description>Numer identyfikacyjny za pomoca ktorego mozna wysylac zapytania o nowszy zbior danych</description>
    /// </iterm>
    /// <item>
    /// <term>Result_count</term>
    /// <description>Licznik limitu wykorzystania zapytan API</description>
    /// </item>
    /// <item>
    /// <term>Next_token</term>
    /// <description>Nastepny token API</description>
    /// </item>
    public class Meta
    {
        public string Oldest_id { get; set; }
        public string Newest_id { get; set; }
        public int Result_count { get; set; }
        public string Next_token { get; set; }
    }

    /// <summary>
    /// Klasa wygenerowana automatycznie podstawie formatu json zwracanego rzez API Twitter'a. Zawiera szczegolowe dane o konkretnych twitt'cie
    /// </summary>
    /// <item>
    /// <term>Created_at</term>
    /// <description>Data utworzenia twitta</description>
    /// </item>
    /// <item>
    /// <term>Id</term>
    /// <description>Numer identyfikacyjny twitta</description>
    /// </item>
    /// <item>
    /// <term>Possibly_sensitive</term>
    /// <description>>Wartosc logiczna informujaca o potencjalnych tresciach wrazliwych o charakterze przemocy lub zbytniego nacechowania sexualnego</description>
    /// </item>
    /// <item>
    /// <term>Text</term>
    /// <description>Tresc twitta</description>
    /// </item>
    public class Datum
    {
        public DateTime Created_at { get; set; }
        public string Id { get; set; }
        public bool Possibly_sensitive { get; set; }
        public string Text { get; set; }
    }


}
