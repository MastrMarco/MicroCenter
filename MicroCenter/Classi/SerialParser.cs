using System.IO.Ports;

namespace MicroCenter.Classi
{
    public class SerialParser
    {


        // Crea una Lista Delle Porte Seriali Attive e un Filtraggio di essi
        //public List<string> ListaPorteConFiltro(string? Filtro)
        //{
        //    Filtro = "USB-SERIAL CH340";
        //    string[] portNames = SerialPort.GetPortNames();
        //    List<string> ch340Ports = new List<string>();

        //    foreach (string portName in portNames)
        //    {
        //        string? description = ContollerSerialPort.GetPortDescription(portName);

        //        // Verifica se la descrizione inizia con "USB-SERIAL CH340"
        //        if (description.StartsWith(Filtro))
        //        {
        //            ch340Ports.Add(portName); // Aggiungi solo il nome della porta alla lista temporanea
        //        }
        //    }
        //    return ch340Ports;
        //}

        public async Task<List<string>> ListaPorteConFiltroAsync(string? filtro = "USB-SERIAL CH340")
        {
            return await Task.Run(() =>
            {
                string[] portNames = SerialPort.GetPortNames();
                List<string> ch340Ports = new List<string>();

                foreach (string portName in portNames)
                {
                    string? description = ContollerSerialPort.GetPortDescription(portName);

                    if (!string.IsNullOrEmpty(description) && description.StartsWith(filtro))
                    {
                        ch340Ports.Add(portName);
                    }
                }
                return ch340Ports;
            });
        }



    }

}
