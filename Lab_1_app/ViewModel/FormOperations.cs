using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Media;


namespace Lab_1_app.ViewModel
{
    using Model;
    using BaseClasses;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    class FormOperations: ViewModelBase
    {
        private const string plikArchiwizacji = "archiwum.json";
        public FormOperations() { Window_Loaded(); }
        ~FormOperations() { Window_Closing(); }

        #region Interfejs publiczny
        private bool _isNameValid = false;
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; onPropertyChanged(nameof(IsNameValid)); }
        }

        private bool _isSurnameValid = false;
        public bool IsSurnameValid
        {
            get { return _isSurnameValid; }
            set { _isSurnameValid = value; onPropertyChanged(nameof(IsSurnameValid)); }
        }

        private string name;
        public string Name 
        {
            get { return name; } set { name = value; onPropertyChanged(nameof(Name)); } 
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; onPropertyChanged(nameof(Surname)); }
        }

        private uint age = 15;
        public uint Age
        {
            get { return age; }
            set { age = value; onPropertyChanged(nameof(Age)); }
        }

        private uint weight = 50;
        public uint Weight
        {
            get { return weight; }
            set { weight = value; onPropertyChanged(nameof(Weight)); }
        }

        private ObservableCollection<Pilkarz> lista = new ObservableCollection<Pilkarz>();
        public ObservableCollection<Pilkarz> Lista 
        {
            get { return lista; }
            set { lista = value; } 
        }

        private Pilkarz _currentPilkarz = null;
        public Pilkarz currentPilkarz
        {
            get { return _currentPilkarz; }
            set { _currentPilkarz = value; onPropertyChanged(nameof(currentPilkarz)); }
        }
        #endregion

        #region Metody
        private void LoadPlayer(Pilkarz p)
        {
            Name = p.Imie;
            Surname= p.Nazwisko;
            Age = p.Wiek;
            Weight = p.Waga;
        }
        private void listBoxPilkarze_SelectionChanged(object sender)
        {  
            if (currentPilkarz != null)
            {
                LoadPlayer(currentPilkarz);
            }
        }

        private void Clear()
        {
            Name = "";
            Surname = "";
            Age = 15;
            Weight = 50;
            currentPilkarz = null;
        }
        
        private void EdytujF(object sender)
        {
            Pilkarz edytowanyPilkarz = new Pilkarz(Name.Trim(), Surname.Trim(), Age, Weight);
            bool powtorzono = false;

            foreach (var pilkarz in Lista)
            {
                var pilkarzNaLiscie = pilkarz as Pilkarz;
                if (pilkarzNaLiscie.IsRepeated(edytowanyPilkarz))
                {
                    powtorzono = true;
                    break;
                }
            }
            if (!powtorzono)
            {
                MessageBoxResult msgBoxResult = MessageBox.Show($"Czy edytować ?\n{currentPilkarz.ToString()}", "Edycja", MessageBoxButton.YesNo);

                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    Lista.Insert(Lista.IndexOf(currentPilkarz), edytowanyPilkarz);
                    Lista.Remove(currentPilkarz);
                }
                Clear();
            }
            else
            {
                MessageBox.Show($"Piłkarz: {edytowanyPilkarz.ToString()}", "Powtórzono");
            }
        }

        private void DodajF(object sender)
        {
            IsNameValid = string.IsNullOrWhiteSpace(Name);
            IsSurnameValid = string.IsNullOrWhiteSpace(Surname);

            if (!IsNameValid & !IsSurnameValid)
            {
                Pilkarz nowyPilkarz = new Pilkarz(Name.Trim(), Surname.Trim(), Age, Weight);
                bool powtorzono = false;
                if (!Lista.Any())
                {
                    Lista.Add(nowyPilkarz);
                    Clear();
                }
                else
                {
                    foreach (var pilkarz in Lista)
                    {
                        var pilkarzNaLiscie = pilkarz as Pilkarz;
                        if (pilkarzNaLiscie.IsRepeated(nowyPilkarz))
                        {
                            powtorzono = true;
                            break;
                        }
                    }

                    if (!powtorzono)
                    {
                        Lista.Add(nowyPilkarz);
                        Clear();
                    }
                    else
                    {
                        MessageBoxResult msgBoxResult = MessageBox.Show($"Piłkarz: {nowyPilkarz.ToString()}\nCzy wyczyścić formularz", "Powtórzono", MessageBoxButton.OKCancel);
                        if (msgBoxResult == MessageBoxResult.OK)
                        {
                            Clear();
                        }
                    }
                }
            }

        }

        private void UsunF(object sender)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show($"Czy napewno chcesz usunąć \n{currentPilkarz.ToString()}", "Usuwanie", MessageBoxButton.YesNo);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                Lista.RemoveAt(Lista.IndexOf(currentPilkarz));
            }
            Clear();   
        }
        #endregion


        #region Komendy

        private ICommand dodaj = null;
        public ICommand Dodaj 
        {
            get
            {
                if(dodaj == null)
                {
                    dodaj = new RelayCommand(
                        DodajF,
                        arg => true
                    );
                }

                return dodaj;
            }
        }

        private ICommand edytuj = null;
        public ICommand Edytuj
        {
            get
            {
                if (edytuj == null)
                {
                    edytuj = new RelayCommand(
                        EdytujF,
                        arg => (!string.IsNullOrEmpty(Name) & !string.IsNullOrEmpty(Surname) & currentPilkarz != null)
                    );
                }

                return edytuj;
            }
        }

        private ICommand usun = null;
        public ICommand Usun
        {
            get
            {
                if (usun == null)
                {
                    usun = new RelayCommand(
                        UsunF,
                        arg => (currentPilkarz != null) 
                    );
                }

                return usun;
            }
        }

        private ICommand load = null;
        public ICommand Load
        {
            get
            {
                if (load == null)
                {
                    load = new RelayCommand(
                        listBoxPilkarze_SelectionChanged,
                        arg => true
                    );
                }

                return load;
            }
        }


        #endregion

        #region ArchivingToJson

        private void Window_Closing()
        {
            var jsonStr = JsonConvert.SerializeObject(Lista);
            File.WriteAllText(plikArchiwizacji, jsonStr);
        }

        private void Window_Loaded()
        {
            if (File.Exists(plikArchiwizacji))
            {
                var jsonStr = File.ReadAllText(plikArchiwizacji);
                var FromJsonToObj = JsonConvert.DeserializeObject<ObservableCollection<Pilkarz>>(jsonStr);
                foreach (var item in FromJsonToObj)
                {
                    Lista.Add(item);
                }
            }
        }
        #endregion
    }
}
