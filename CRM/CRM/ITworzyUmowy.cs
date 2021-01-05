using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    /// <summary>
    /// Interfejs grupujący metody potrzebne do stowrzenia umowy
    /// </summary>
    interface ITworzyUmowy
    {
        /// <summary>
        /// Metoda licząca łączny koszt umowy
        /// </summary>
        /// <returns>Wartość double łącznego kosztu umowy</returns>
        double Koszt();
        /// <summary>
        /// Metoda wyszukująca kod w umowie na podstawie kodu produktu
        /// </summary>
        /// <param name="kod">Kod szukanego produktu</param>
        /// <returns>Zwraca szukany produkt lub wyjątek ProductNotFoundException</returns>
        Produkt ZnajdzProdukt(string kod);
        /// <summary>
        /// Metoda, która sprawdza czy jednostka miary produktu to szuka i jeśli tak - ilość zostaje zaokrąglona w dół do wartości całkowitych.
        /// </summary>
        /// <param name="produkt">Wybrany produkt dla którego sprawdzana jest jednostka</param>
        /// <param name="ilosc">Ilość wybranego produktu wprowadzona przez użytkownika</param>
        /// <returns>Wartość ilości produktu. Jeśli produkt liczony jest w sztukach, zwracana jest wartość ilości zaokrąlona w dół do najbliższej liczby całkowitej</returns>
        double JesliSztuki(Produkt produkt, double ilosc);
        /// <summary>
        /// Metoda dodająca 1 produkt do umowy. Jeśli produkt już jest w umowie, dodaje się do niego jedną jednostkę.
        /// </summary>
        /// <param name="produkt">Dodawany produkt</param>
        void DodajProdukt(Produkt produkt);
        /// <summary>
        /// Metoda dodająca produkt do umowy wraz z ilością. Jeśli produkt już jest w umowie, dodaje się do istniejącej ilości dodatkowe jednostki nowej ilości.
        /// </summary>
        /// <param name="produkt">Dodawany produkt</param>
        /// <param name="ilosc">Ilość dodawanego produktu</param>
        void DodajProdukt(Produkt produkt, double ilosc);
        /// <summary>
        /// Metoda usuwająca produkt z umowy.
        /// </summary>
        /// <param name="produkt">Usuwany produkt.</param>
        /// <returns>True jeśli produkt był w umowie i został usunięty. W przeciwnym wypadku - false</returns>
        bool UsunProdukt(Produkt produkt);
        /// <summary>
        /// Metoda usuwająca produkt z umowy wg kodu produktu.
        /// </summary>
        /// <param name="kod">Kod usuwanego produktu</param>
        /// <returns>True jeśli produkt był w umowie i został usunięty. W przeciwnym wypadku - false</returns>
        bool UsunProduktKod(string kod);
        /// <summary>
        /// Metoda zmieniająca ilość produktu będącego w umowie.
        /// </summary>
        /// <param name="produkt">Wybrany produkt</param>
        /// <param name="ilosc">Nowa ilość produktu</param>
        /// <returns>True jeśli produkt był w umowie i zmieniono jego ilość na nową, w przeciwnym wypadku - false</returns>
        bool ZmienIlosc(Produkt produkt, double ilosc);
        /// <summary>
        /// Metoda zmieniająca ilość produktu będącego w umowie wg kodu produktu.
        /// </summary>
        /// <param name="kod">Kod wybranego produktu</param>
        /// <param name="ilosc">Nowa ilość produktu</param>
        /// <returns>True jeśli produkt był w umowie i zmieniono jego ilość na nową, w przeciwnym wypadku - false</returns>
        bool ZmienIloscKod(string kod, double ilosc);
    }
}
