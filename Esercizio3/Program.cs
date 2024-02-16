//lavorare con oggetto DateTime: chiedere all'utente di inserire un numero da 1 a 100, acquisendolo nella variabile "numeroGiorni" 
//Dopo l'inserimento di un numero valido, presentare all'utente la lista numerata dei giorni della settimana: 1 = Lunedì, 2 = Martedì, etc..
//Chiedere quindi all'utente di selezionare un giorno della settimana inserendo il numero corrispondente. Dopo che è stato inserito un numero valido
//(in questo caso da 1 a 7), tenerne traccia nella variabile "giornoSelezionato"
//Stampare quindi a video un numero di date (in formato gg/MM/yyyy, ovvero 21/02/2024, 13/04/2024, etc..), partendo dalla data corrente (inclusa)
//pari al "numeroGiorni" inserito in cui il giorno (di ciascuna data stampata) corrisponda al valore di "giornoSelezionato".
//Anche in questo caso, se in una fase di acquisizione viene inserito un dato non valido, segnalare all'utente l'errore e predisporsi per un nuovo inserimento.

//esempio di input/ output (data corrente = 07/02/2024)

//numeroGiorni = 3
//giorno = giovedì(4)

//->output: 

//08 / 02 / 2024
//15 / 02 / 2024
//22 / 02 / 2024 

using System.Globalization;

// richiesta numero giorni
var numeroGiorni = Utilities.AcquisisciInteroDaConsole("Inserire un numero da 1 a 100", 1, 100);


// richiesta giorno della settimana
DayOfWeek[] giorniSettimana = (DayOfWeek[])Enum.GetValues(typeof(DayOfWeek));

foreach(DayOfWeek giorno in giorniSettimana)
{
    Console.WriteLine(giorno.ToString("d") + " = " + giorno.GetItalianDayOfWeek());
}
var giornoSelezionato = Utilities.AcquisisciInteroDaConsole("Scegli un giorno della settimana:", 0, 6);

// stampa del risultato
Console.WriteLine("numero giorni = " + numeroGiorni);
Console.WriteLine("Giorno selezionato = " + giornoSelezionato + " (" + giorniSettimana[giornoSelezionato].GetItalianDayOfWeek() + ")");

Console.WriteLine("Ecco i prossimi " + numeroGiorni + " " + giorniSettimana[giornoSelezionato].GetItalianDayOfWeek());
Utilities.StampaDateSelezionate(numeroGiorni, giorniSettimana, giornoSelezionato);

public static class Utilities
{
    public static int AcquisisciInteroDaConsole(string messaggioUtente, int min, int max)
    {
        Console.WriteLine(messaggioUtente);
        var stringaAcquisita = Console.ReadLine();

        if (!Int32.TryParse(stringaAcquisita, out int toReturn))
        {
            Console.WriteLine("Attenzione, il valore inserito non è un valore valido");
            return AcquisisciInteroDaConsole(messaggioUtente, min, max);
        }

        if (!(_checkValoreSogliaMassima(toReturn, max) && _checkValoreSogliaMinima(toReturn, min)))
        {
            return AcquisisciInteroDaConsole(messaggioUtente, min, max);
        }

        return toReturn;
    }

    private static bool _checkValoreSogliaMinima(int numeroAcquisito, int min)
    {
        if (numeroAcquisito < min)
        {
            Console.WriteLine("Attenzione, il valore non può essere minore di " + min);
            return false;
        }

        return true;
    }

    private static bool _checkValoreSogliaMassima(int numeroAcquisito, int max)
    {
        if (numeroAcquisito > max)
        {
            Console.WriteLine("Attenzione, il valore non può essere maggiore di " + max);
            return false;
        }

        return true;
    }
    public static string GetItalianDayOfWeek(this DayOfWeek day)
    {
        // Create a CultureInfo for Italian in Italy
        CultureInfo italianCulture = new CultureInfo("it-IT");

        // Convert the DayOfWeek enum to a DateTime, using a base date plus the number of days for the DayOfWeek (0 for Sunday, 1 for Monday, etc.)
        DateTime baseDate = new DateTime(2023, 1, 1); // Use any date that is a Sunday
        DateTime targetDate = baseDate.AddDays((int)day);

        // Get the day of the week in Italian
        string italianDayOfWeek = targetDate.ToString("dddd", italianCulture);

        return italianDayOfWeek;
    }

    public static void StampaDateSelezionate(int numeroGiorni, DayOfWeek[] giorniSettimana, int giornoSelezionato)
    {
        var contaGiorni = 0;
        DateTime data = DateTime.Today;
        while (contaGiorni != numeroGiorni)
        {
            if (data.DayOfWeek.Equals(giorniSettimana[giornoSelezionato]))
            {
                Console.WriteLine(data.ToString("dd/MM/yyyy"));
                data = data.AddDays(6);
                contaGiorni++;
            }

            data = data.AddDays(1);
        }
    }
}

