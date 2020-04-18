using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab_1_app.Model
{

    class Pilkarz
    {
        #region Properties
        public string Imie { get; private set; }
        public string Nazwisko { get; private set; }
        public uint Wiek { get; private set; }
        public uint Waga { get; private set; }
        #endregion

        #region Constructors
        [JsonConstructor]
        public Pilkarz(string imie, string nazwisko, uint wiek, uint waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
        }

        public Pilkarz(string imie, string nazwisko) : this(imie, nazwisko, 15, 50) { }
        public Pilkarz() { }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Imie} {Nazwisko}, lat: {Wiek}, waga: {Waga} kg";
        }

        public bool IsRepeated(Pilkarz p)
        {
            if (p.Imie != this.Imie) return false;
            if (p.Nazwisko != this.Nazwisko) return false;
            if (p.Wiek != this.Wiek) return false;
            if (p.Waga != this.Waga) return false;
            return true;
        }
        #endregion

    }
}
