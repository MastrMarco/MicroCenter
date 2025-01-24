namespace MicroCenter.Classi
{
    public class SerialParser
    {
        // Metodo per suddividere la stringa
        //public List<List<string>> ParseString(string input)
        //{
        //    // Lista principale che conterrà le liste separate da ';'
        //    var result = new List<List<string>>();

        //    // Dividi la stringa principale utilizzando ';'
        //    var sections = input.Split(';', StringSplitOptions.RemoveEmptyEntries);

        //    foreach (var section in sections)
        //    {
        //        // Per ogni sezione, dividi ulteriormente utilizzando ','
        //        var values = section.Split(',', StringSplitOptions.RemoveEmptyEntries);
        //        result.Add(new List<string>(values));
        //    }

        //    return result;
        //}

        private List<List<string>> _parsedData = new List<List<string>>();
        // Metodo per suddividere la stringa e sovrascrivere i dati esistenti
        public List<List<string>> ParseString(string input)
        {
            // Resetta i dati precedenti
            _parsedData.Clear();

            // Dividi la stringa principale utilizzando ';'
            // var sections = input.Split(';', StringSplitOptions.RemoveEmptyEntries);
            var sections = input.Split(';');

            foreach (var section in sections)
            {
                // Per ogni sezione, dividi ulteriormente utilizzando ','
                // var values = section.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var values = section.Split(',');
                _parsedData.Add(new List<string>(values));
            }

            return _parsedData;
        }











        // Metodo per stampare il risultato in modo leggibile
        //public void PrintParsedResult(List<List<string>> parsedData)
        //{
        //    for (int i = 0; i < parsedData.Count; i++)
        //    {
        //        Console.WriteLine($"Sezione {i + 1}:");
        //        foreach (var value in parsedData[i])
        //        {
        //            Console.WriteLine($"  Valore: {value}");
        //        }
        //    }
        //}
    }

}
