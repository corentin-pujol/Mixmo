using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mixmo_PUJOL
{
    public class Lettres
    {
        //champs
        List<Lettre> pioche;
        List<Lettre> main_du_joueur;

        //constructeur
        public Lettres(string nomFichier) //Constructeur pioche
        {
            List<Lettre> p = new List<Lettre>(); //Readfile
            StreamReader fichLect = new StreamReader(nomFichier);
            char[] sep = new char[1] { ',' };
            string ligne = "";
            string[] datas = new string[3];
            while (fichLect.Peek() > 0)
            {
                ligne = fichLect.ReadLine(); //Lecture d'une ligne
                datas = ligne.Split(sep);
                char symb = System.Convert.ToChar(datas[0]); //conversion des elements du fichier
                int fréquence = System.Convert.ToInt32(datas[1]);
                int pts = System.Convert.ToInt32(datas[2]);
                Lettre lettre = new Lettre(symb, fréquence, pts); //création d'une lettre
                for (int i = 0; i < lettre.Nombre_de_lettres; i++)
                {
                    p.Add(lettre);
                }
            }
            fichLect.Close();
            this.pioche = p;
        }
        //Constructeur main du joueur
        public Lettres(List<Lettre> pioche_P, Random lettre_aleatoire) 
        {
            List<Lettre> main_initiale = new List<Lettre>();
            for (int i = 0; i < 6; i++)
            {
                int index_d_une_lettre_aleatoire = lettre_aleatoire.Next(0, pioche_P.Count); //création d'un index aléatoire
                Lettre Lettre_aleatoire = pioche_P[index_d_une_lettre_aleatoire];
                main_initiale.Add(Lettre_aleatoire); //Ajout dans la main d'un joueur d'une lettre choisi au hasard dans la pioche
                pioche_P.Remove(Lettre_aleatoire); //Suppression de l'élément dans la pioche
                if (Lettre_aleatoire.Nombre_de_lettres > 0)
                {
                    Lettre_aleatoire.Nombre_de_lettres -= 1; //On met à jour la fréquence de la lettre 
                }
            }
            this.main_du_joueur = main_initiale; //Affectation de l'instance main du joueur
            this.pioche = pioche_P; //On change l'état de la pioche
        }
        //Propriété
        public List<Lettre> Pioche //Création des accès en lecture et en accès écriture
        {
            get { return this.pioche; }
            set { this.pioche = value; }
        }
        public List<Lettre> Main_du_joueur
        {
            get { return this.main_du_joueur; }
            set { this.main_du_joueur = value; }
        }
        //Méthodes
        /// <summary>
        /// Cette méthode retourne l'ensemble des éléments de la pioche
        /// </summary>
        /// <returns>éléments de la pioche</returns>
        public string ToString_Pioche()
        {
            string tostring_pioche = "";
            foreach (Lettre lettre_pioche in this.pioche)
            {
                tostring_pioche += lettre_pioche.ToString() + "\n";
            }
            return tostring_pioche;
        }
        /// <summary>
        /// Cette méthode decrit la main d'un joueur
        /// </summary>
        /// <returns>Les lettres que le joueur a dans sa main</returns>
        public string ToString_Joueur()
        {
            string tostring_joueur = "";
            foreach (Lettre lettre_joueur in this.main_du_joueur)
            {
                tostring_joueur += lettre_joueur.Symbole +" ";
            }
            return "Le joueur a en main les cartes suivantes :" + "\n" + tostring_joueur;
        }
    }
}
