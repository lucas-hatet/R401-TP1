using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    public class Devise
    {

        private int id;
        private string? nomDevise;
        private double taux;

        public int Id {
            get { return id; }
            set { id = value; }
        }
        [Required]
        public string? NomDevise
        {
            get { return nomDevise; }
            set { nomDevise = value; }
        }
        public double Taux
        {
            get { return taux; }
            set { taux = value; }
        }

        public Devise()
        {
            Id = 0;
            NomDevise = null;
            Taux = 0;
        }
        public Devise(int id, string nomDevise, double taux)
        {
            Id = id;
            NomDevise = nomDevise;
            Taux = taux;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Devise))
                return false;
            else
                return this.Id == ((Devise)obj).Id
                    && this.NomDevise == ((Devise)obj).NomDevise
                    && this.Taux == ((Devise)obj).Taux;
        }
    }
}
