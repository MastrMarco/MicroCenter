using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Gestionale_WEB.Models
{
    public class Lib_JSON_CRUD<T>
    {

        private readonly string _filePath;
        private T _data;

        // Costruttore: Specifica il percorso del file JSON
        public Lib_JSON_CRUD(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        // Carica i dati dal file JSON
        private void LoadData()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string jsonData = File.ReadAllText(_filePath);
                    _data = JsonConvert.DeserializeObject<T>(jsonData) ?? Activator.CreateInstance<T>();
                }
                else
                {
                    _data = Activator.CreateInstance<T>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il caricamento dei dati: {ex.Message}");
                _data = Activator.CreateInstance<T>();
            }
        }

        // Salva i dati nel file JSON
        public void SaveData()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(_data, Formatting.Indented);
                File.WriteAllText(_filePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il salvataggio dei dati: {ex.Message}");
            }
        }

        // Ritorna tutti i dati
        public T GetData()
        {
            return _data;
        }

        // Ricerca un elemento in una lista basata su una chiave stringa
        public U Trova<U>(Func<U, bool> predicate) where U : class
        {
            try
            {
                // Trova tutte le liste nel modello
                var lists = _data.GetType().GetProperties()
                    .Where(p => typeof(IEnumerable<U>).IsAssignableFrom(p.PropertyType))
                    .Select(p => (IEnumerable<U>)p.GetValue(_data));

                foreach (var list in lists)
                {
                    var element = list.FirstOrDefault(predicate);
                    if (element != null)
                        return element;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante la ricerca: {ex.Message}");
            }
            return null;
        }

        // Aggiungi un elemento a una lista
        public bool AddElement<U>(U newElement, string listName) where U : class
        {
            try
            {
                var listProperty = _data.GetType().GetProperty(listName);
                if (listProperty != null && typeof(IEnumerable<U>).IsAssignableFrom(listProperty.PropertyType))
                {
                    var list = (IList<U>)listProperty.GetValue(_data);
                    if (list != null)
                    {
                        list.Add(newElement);
                        SaveData();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante l'aggiunta dell'elemento: {ex.Message}");
            }
            return false;
        }

        // Aggiorna un elemento esistente
        public bool Aggiorna<U>(Func<U, bool> predicate, Action<U> updateAction) where U : class
        {
            try
            {
                var element = Trova(predicate);
                if (element != null)
                {
                    updateAction(element);
                    SaveData();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante l'aggiornamento dell'elemento: {ex.Message}");
            }
            return false;
        }

        // Elimina un elemento dalla lista
        public bool DeleteElement<U>(Func<U, bool> predicate) where U : class
        {
            try
            {
                var lists = _data.GetType().GetProperties()
                    .Where(p => typeof(IEnumerable<U>).IsAssignableFrom(p.PropertyType))
                    .Select(p => (IList<U>)p.GetValue(_data));

                foreach (var list in lists)
                {
                    var element = list.FirstOrDefault(predicate);
                    if (element != null)
                    {
                        list.Remove(element);
                        SaveData();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante l'eliminazione dell'elemento: {ex.Message}");
            }
            return false;
        }



        //private string _filePath;
        //private T _data;

        //// Costruttore: Specifica il percorso del file JSON
        //public Lib_JSON_CRUD(string filePath)
        //{
        //    _filePath = filePath;
        //    LoadData();
        //}

        //// Carica i dati dal file JSON
        //private void LoadData()
        //{
        //    if (File.Exists(_filePath))
        //    {
        //        string jsonData = File.ReadAllText(_filePath);
        //        _data = JsonConvert.DeserializeObject<T>(jsonData) ?? Activator.CreateInstance<T>();
        //    }
        //    else
        //    {
        //        _data = Activator.CreateInstance<T>();
        //    }
        //}

        //// Salva i dati nel file JSON
        //public void SaveData()
        //{
        //    string jsonData = JsonConvert.SerializeObject(_data, Formatting.Indented);
        //    File.WriteAllText(_filePath, jsonData);
        //}

        //// Ritorna i dati caricati
        //public T GetData()
        //{
        //    return _data;
        //}

        //// Aggiorna i dati completamente (opzione avanzata)
        //public void UpdateData(T newData)
        //{
        //    _data = newData;
        //    SaveData();
        //}













    }
}
