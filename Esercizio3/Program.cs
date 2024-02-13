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

//StatoOrdine stato = (StatoOrdine)4;

//Console.WriteLine(stato.ToString());
//Console.ReadLine();

var numeroGiorni = 0;
var giornoSelezionato = 0;
string stringaAcquisita;
int count = 0;

// richiesta numero giorni
do
{
    if(count > 0)
    {
        Console.WriteLine("Importo inserito non valido");
    }
    Console.WriteLine("Inserire un numero da 1 a 100");
    stringaAcquisita = Console.ReadLine();
    count++;
}
while(!Int32.TryParse(stringaAcquisita, out numeroGiorni));
// converte da stringa a int32 e restituisce true se la conversione è avvenuta

// richiesta giorno della settimana
count = 0;
DayOfWeek[] giorniSettimana = (DayOfWeek[])Enum.GetValues(typeof(DayOfWeek));

foreach(DayOfWeek giorno in giorniSettimana)
{
    Console.WriteLine(giorno.ToString("d") + " = " + giorno.GetItalianDayOfWeek());
}

do
{
    if (count > 0)
    {
        Console.WriteLine("Importo inserito non valido");
    }
    Console.WriteLine("Scegli un giorno della settimana:");
    stringaAcquisita = Console.ReadLine();
    count++;
}
while (!Int32.TryParse(stringaAcquisita, out giornoSelezionato) || giornoSelezionato < 0 || giornoSelezionato > 6);

// stampa del risultato
Console.WriteLine("numero giorni = " + numeroGiorni);
Console.WriteLine("Giorno selezionato = " + giornoSelezionato + " (" + giorniSettimana[giornoSelezionato] + ")");

var contaGiorni = 0;
DateTime data = DateTime.Now;

Console.WriteLine("Ecco i prossimi " + numeroGiorni + " " + giorniSettimana[giornoSelezionato].ToString("G"));
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



public static class Utilities
{
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
}

public class Ordine
{
    public int Id { get; set; }
    //..prodotto ordinato, quantità, etc..
    public StatoOrdine Stato { get; set; }

}

public enum StatoOrdine
{
    InAttesa = 0,
    Schedulato = 1,
    InLavorazione = 2,
    Concluso = 3
}