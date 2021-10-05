using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mixmo_PUJOL
{
    public class Lettre //version0 (120 lettres, pas de joker, on ne remélange pas les lettres)
    {
        //Champs
        char symbole;
        int nombre_de_lettres;
        int poids;
        //Constructeur
        public Lettre(char S, int N, int P)
        {
            this.symbole = S;
            this.nombre_de_lettres = N;
            this.poids = P;

        }

        //propriétés
        public char Symbole
        {
            get { return this.symbole; }
            set { this.symbole = value; }
        }
        public int Nombre_de_lettres
        {
            get { return this.nombre_de_lettres; }
            set { this.nombre_de_lettres = value; }
        }
        public int Poids
        {
            get { return this.poids; }
        }

        //Méthodes
        /// <summary>
        /// Cette méthode retourne les caractéristiques d'une lettre
        /// </summary>
        /// <returns>caracteristique d'une lettre</returns>
        public override string ToString()
        {
            return "La lettre sélectionnée est la lettre : " + this.symbole + ". Il y a " + this.nombre_de_lettres + " fois la lettre et elle vaut : " + this.poids + " points.";
        }
    }
}
