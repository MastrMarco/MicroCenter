using System.IO;
using System.Text.Json;

namespace Gestionale_WEB.Models
{


    public class LetturaDatiSalvatiJson<C> where C : class
    {
        private readonly string _filePath;


        public LetturaDatiSalvatiJson(string filePath)
        {
            _filePath = filePath;
        }


        private List<C> LoadData()
        {
            if (!File.Exists(_filePath))
            {
                return new List<C>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<C>>(json) ?? new List<C>();
        }

        //private List<ElementiGestionale> LoadData()
        //{
        //    if (!File.Exists(_filePath))
        //    {
        //        return new List<ElementiGestionale>();
        //    }

        //    var json = File.ReadAllText(_filePath);
        //    return JsonSerializer.Deserialize<List<ElementiGestionale>>(json) ?? new List<ElementiGestionale>();
        //}

        private void SaveData(List<C> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<C> GetAll()
        {
            return LoadData();
        }

        //public ElementiGestionale GetByTitolo(string titolo)
        //{
        //    return LoadData().FirstOrDefault(p => p.Titolo.Equals(titolo, StringComparison.OrdinalIgnoreCase));
        //}

        public C GetByTitolo(string classAttributo, string titolo)
        {
            var data = LoadData();
            var propInfo = typeof(C).GetProperty(classAttributo);
            if (propInfo == null) return null;

            return data.FirstOrDefault(item => propInfo.GetValue(item)?.ToString() == titolo);
        }


        //public void Crea(ElementiGestionale newPrenotazione)
        //{
        //    var data = LoadData();
        //    if (data.Any(p => p.Titolo.Equals(newPrenotazione.Titolo, StringComparison.OrdinalIgnoreCase)))
        //    {
        //        throw new InvalidOperationException("Una prenotazione con questo nome esiste già.");
        //    }

        //    data.Add(newPrenotazione);
        //    SaveData(data);
        //}

        public void Crea(string classAttributo, C newItem)
        {
            var data = LoadData();
            var propInfo = typeof(C).GetProperty(classAttributo);

            if (propInfo != null)
            {
                var titolo = propInfo.GetValue(newItem)?.ToString();
                if (data.Any(item => propInfo.GetValue(item)?.ToString() == titolo))
                {
                    throw new InvalidOperationException("Un elemento con lo stesso Nome esiste già.");
                }
            }

            data.Add(newItem);
            SaveData(data);
        }

        //public void Update(Prenotazione updatedPrenotazione)
        //{
        //    var data = LoadData();
        //    var prenotazione = data.FirstOrDefault(p => p.Id == updatedPrenotazione.Id);

        //    if (prenotazione != null)
        //    {
        //        prenotazione.Nome = updatedPrenotazione.Nome;
        //        prenotazione.DataPrenotazione = updatedPrenotazione.DataPrenotazione;
        //        prenotazione.NumeroPosti = updatedPrenotazione.NumeroPosti;
        //        SaveData(data);
        //    }
        //}

        //public void Elimina(string titolo)
        //{
        //    var data = LoadData();
        //    var prenotazione = data.FirstOrDefault(p => p.Titolo == titolo);

        //    if (prenotazione != null)
        //    {
        //        data.Remove(prenotazione);
        //        SaveData(data);
        //    }
        //}

        public void Aggiorna(string classAttributo, C updatedItem)
        {
            var data = LoadData();
            var propInfo = typeof(C).GetProperty(classAttributo);
            if (propInfo == null) return;

            var name = propInfo.GetValue(updatedItem)?.ToString();
            var existingItem = data.FirstOrDefault(item => propInfo.GetValue(item)?.ToString() == name);

            if (existingItem != null)
            {
                data.Remove(existingItem);
                data.Add(updatedItem);
                SaveData(data);
            }
        }

        public void Elimina(string classAttributo, string titolo)
        {
            var data = LoadData();
            var propInfo = typeof(C).GetProperty(classAttributo);
            if (propInfo == null) return;

            var itemToDelete = data.FirstOrDefault(item => propInfo.GetValue(item)?.ToString() == titolo);
            if (itemToDelete != null)
            {
                data.Remove(itemToDelete);
                SaveData(data);
            }
        }

    }
}
