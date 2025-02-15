using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace MicroCenter.Classi
{

    public class Lib_JSON_CRUD_3<T> where T : class, new()
    {
        private readonly string _filePath;

        // Due strutture diverse per supportare entrambi i formati
        private Dictionary<string, List<Dictionary<string, List<T>>>> _dataAnnidato;
        private Dictionary<string, List<T>> _dataSemplice;

        private bool _usaFormatoSemplice; // Flag per identificare il formato

        public Lib_JSON_CRUD_3(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    _dataAnnidato = new Dictionary<string, List<Dictionary<string, List<T>>>>();
                    _dataSemplice = new Dictionary<string, List<T>>();
                    return;
                }

                string jsonData = File.ReadAllText(_filePath);

                // Tentiamo di deserializzare come formato annidato
                try
                {
                    _dataAnnidato = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, List<T>>>>>(jsonData);
                    if (_dataAnnidato != null && _dataAnnidato.Count > 0)
                    {
                        _usaFormatoSemplice = false;
                        return;
                    }
                }
                catch { }

                // Se fallisce, tentiamo di deserializzare come formato semplice
                try
                {
                    _dataSemplice = JsonConvert.DeserializeObject<Dictionary<string, List<T>>>(jsonData);
                    if (_dataSemplice != null && _dataSemplice.Count > 0)
                    {
                        _usaFormatoSemplice = true;
                        return;
                    }
                }
                catch { }

                // Se nessun formato è valido, inizializziamo con strutture vuote
                _dataAnnidato = new Dictionary<string, List<Dictionary<string, List<T>>>>();
                _dataSemplice = new Dictionary<string, List<T>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il caricamento dei dati: {ex.Message}");
                _dataAnnidato = new Dictionary<string, List<Dictionary<string, List<T>>>>();
                _dataSemplice = new Dictionary<string, List<T>>();
            }
        }

        public void SaveData()
        {
            try
            {
                string jsonData = _usaFormatoSemplice
                    ? JsonConvert.SerializeObject(_dataSemplice, Formatting.Indented)
                    : JsonConvert.SerializeObject(_dataAnnidato, Formatting.Indented);

                File.WriteAllText(_filePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il salvataggio dei dati: {ex.Message}");
            }
        }

        // 📌 Metodo per ottenere gli elementi principali (solo per JSON annidato)
        public List<string> GetElementiPrincipali(string versione)
        {
            if (_usaFormatoSemplice) return new List<string>(); // Non applicabile

            return _dataAnnidato.ContainsKey(versione)
                ? _dataAnnidato[versione].SelectMany(d => d.Keys).ToList()
                : new List<string>();
        }

        // 📌 Metodo per ottenere il contenuto di una categoria (funziona per entrambi i formati)
        public List<T> GetContenutoElemento(string versione, string categoria)
        {
            if (_usaFormatoSemplice)
            {
                return _dataSemplice.ContainsKey(categoria) ? _dataSemplice[categoria] : new List<T>();
            }

            if (!_dataAnnidato.ContainsKey(versione)) return new List<T>();

            var elementoTrovato = _dataAnnidato[versione].FirstOrDefault(d => d.ContainsKey(categoria));
            return elementoTrovato != null ? elementoTrovato[categoria] : new List<T>();
        }

        // 📌 Metodo per ottenere tutti gli elementi (solo per JSON annidato)
        public List<T> GetDatiSenzaCategoria(string versione)
        {
            if (_usaFormatoSemplice) return new List<T>(); // Non applicabile

            if (!_dataAnnidato.ContainsKey(versione)) return new List<T>();

            return _dataAnnidato[versione]
                .SelectMany(entry => entry.Values.SelectMany(value => value))
                .ToList();
        }

        // 📌 Metodo per aggiungere un elemento (funziona per entrambi i formati)
        public bool AddElement(string versione, string categoria, T newItem)
        {
            if (_usaFormatoSemplice)
            {
                if (!_dataSemplice.ContainsKey(categoria))
                {
                    _dataSemplice[categoria] = new List<T>();
                }
                _dataSemplice[categoria].Add(newItem);
            }
            else
            {
                if (!_dataAnnidato.ContainsKey(versione))
                    _dataAnnidato[versione] = new List<Dictionary<string, List<T>>>();

                var categoriaTrovata = _dataAnnidato[versione].FirstOrDefault(d => d.ContainsKey(categoria));
                if (categoriaTrovata == null)
                {
                    categoriaTrovata = new Dictionary<string, List<T>> { { categoria, new List<T>() } };
                    _dataAnnidato[versione].Add(categoriaTrovata);
                }
                categoriaTrovata[categoria].Add(newItem);
            }

            SaveData();
            return true;
        }

        // 📌 Metodo per eliminare un elemento (funziona per entrambi i formati)
        public bool DeleteElement(string versione, string categoria, Func<T, bool> predicate)
        {
            if (_usaFormatoSemplice)
            {
                if (!_dataSemplice.ContainsKey(categoria)) return false;

                var lista = _dataSemplice[categoria];
                var elementoDaRimuovere = lista.FirstOrDefault(predicate);
                if (elementoDaRimuovere != null)
                {
                    lista.Remove(elementoDaRimuovere);
                    SaveData();
                    return true;
                }
            }
            else
            {
                if (!_dataAnnidato.ContainsKey(versione)) return false;

                var categoriaTrovata = _dataAnnidato[versione].FirstOrDefault(d => d.ContainsKey(categoria));
                if (categoriaTrovata == null) return false;

                var lista = categoriaTrovata[categoria];
                var elementoDaRimuovere = lista.FirstOrDefault(predicate);
                if (elementoDaRimuovere != null)
                {
                    lista.Remove(elementoDaRimuovere);
                    SaveData();
                    return true;
                }
            }
            return false;
        }


        // 📌 Metodo per trovare un elemento (funziona per entrambi i formati) ?
        public T FindElement(string versione, string categoria, Func<T, bool> predicate)
        {
            if (_usaFormatoSemplice)
            {
                if (_dataSemplice.ContainsKey(categoria))
                {
                    return _dataSemplice[categoria].FirstOrDefault(predicate);
                }
            }
            else
            {
                if (_dataAnnidato.ContainsKey(versione))
                {
                    var categoriaTrovata = _dataAnnidato[versione].FirstOrDefault(d => d.ContainsKey(categoria));
                    if (categoriaTrovata != null)
                    {
                        return categoriaTrovata[categoria].FirstOrDefault(predicate);
                    }
                }
            }
            return null;
        }



    }

}
