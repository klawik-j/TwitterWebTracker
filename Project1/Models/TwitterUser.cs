using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Project1.Models
{
    /// <summary>
    /// Klasa wygenerowana automatycznie podstawie formatu json zwracanego rzez API Twitter'a. Zaweira informacje o uzytkowniku i o ewentualnych bledach
    /// </summary>
    /// <item>
    /// <term>Data</term>
    /// <description>Szczegolowe informacje o uzytkowniku</description>
    /// </item>
    /// <item>
    /// <term>Errors</term>
    /// <description>Tablica informacji o ewentualnych bledach</description>
    /// </item>
    public class TwitterUser
    {   
        public Data Data { get; set; }
        public Error[] Errors { get; set; }
        public TwitterUser()
        {
            Data = new Data();
            Errors = Array.Empty<Error>();
        }
    }

    /// <summary>
    /// Klasa wygenerowana automatycznie podstawie formatu json zwracanego rzez API Twitter'a. Zawiera szczegolowe informacje o uzytkowniku
    /// </summary>
    /// <item>
    /// <term>Id</term>
    /// <description>Numer identyfikacyjny uzytkownika</description>
    /// </item>
    /// <item>
    /// <term>Name</term>
    /// <description>Imie uzytkownika</description>
    /// </item>
    /// <item>
    /// <term>Username</term>
    /// <description>Nazwa uzytkowika</description>
    /// </item>
    /// <item>
    /// <term>Profile_image_url</term>
    /// <description>url do obrazu profilowego uzytkownika</description>
    /// </item>
    public class Data
    {
        public Data()
        {
            Id = null;
            Name = null;
            Username = null;
            Profile_image_url = null;
        }
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Profile_image_url { get; set; }
    }

    /// <summary>
    /// Klasa wygenerowana automatycznie na podstawie formatu json zwracanego przez API Twitter'a. Zawiera szczegolowe informacje o ewentualnych bledach
    /// </summary>
    /// <item>
    /// <term>Detail</term>
    /// <description>Szczegoly bledu</description>
    /// </item>
    /// <item>
    /// <term>Title</term>
    /// <description>Tytul bledu</description>
    /// </item>
    /// <item>
    /// <term>Resource_type</term>
    /// <description>Kategoria bledu</description>
    /// </item>
    /// <item>
    /// <term>Value</term>
    /// <description>Tresc bledu</description>
    /// </item>
    /// <item>
    /// <term>Type</term>
    /// <description>Typ bledu</description>
    /// </item>
    public class Error
    {
        public Error()
        {
            Detail = null;
            Title = null;
            Resource_type = null;
            Parameter = null;
            Value = null;
            Type = null;
        }
        public string Detail { get; set; }
        public string Title { get; set; }
        public string Resource_type { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

}
