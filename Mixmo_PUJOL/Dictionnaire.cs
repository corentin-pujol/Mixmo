using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mixmo_PUJOL
{
    public class Dictionnaire
    {
        //Champs
        List<List<string>> dictionnaire;

        //Constructeur
        public Dictionnaire(string nomfichier)
        {
            
            List<List<string>> dico = new List<List<string>>(); //Liste de liste de mots initialisée
            StreamReader fichLect = new StreamReader(nomfichier);
            string ligne = "";
            int nombre_de_lettre = 0; //On initialise le nombre de lettre composant les mots (Pour savoir a quel niveau du dico nous nous situons puisque chaque paragraphe de mots est precede du nombre correspondant au nombre de lettre les composants)
            while (fichLect.Peek() > 0)
            {
                // on traite la ligne indiquant le nombre de lettres
                ligne = fichLect.ReadLine(); //Lecture d'une ligne
                nombre_de_lettre = Convert.ToInt32(ligne); //On convertit le string en entier, pour pouvoir manipuler l'entier après
                // on traite la ligne avec les mots
                ligne = fichLect.ReadLine(); //Lecture d'une ligne
                List<string> listemots = new List<string>(); // Liste de mots initialisée
                for (int i = 0 ; i < ligne.Length ; i = i + nombre_de_lettre + 1) //On boucle sur la ligne et a chaque itération on pointe sur le debut du mot suivant (le +1 sert a supprimer les espaces entre chaque mots)
                {
                    string mot = "";
                    for(int j = 0; j<nombre_de_lettre;  j++) //En fonction du nombre de lettre composant un mot nous rajoutons les lettres au string mots
                    {
                        mot = mot + ligne[i+j]; //En fonction du nombre de lettre composant un mot nous rajoutons les lettres au string mots
                    }
                    listemots.Add(mot); //Que l'on ajoute a la liste des mots
                }
                dico.Add(listemots); // Et nous ajoutons la liste complète a la liste de liste de mots "dico"
            }
            fichLect.Close();
            this.dictionnaire = dico; //Affectation de l'instance dictionnaire
        }

        //Propriétés
        public List<List<string>> Dico
        {
            get { return this.dictionnaire; }
        }

        //Méthodes

        //public string afficherdico()
        //{
        //    string motsdico = "";
        //    foreach(List<string> n in this.dictionnaire)
        //    {
        //        foreach(string mot in n)
        //        {
        //            motsdico = motsdico + mot;
        //        }
        //    }
        //    return motsdico;
        //}


        /// <summary>
        /// Cette methode permet la recherche recursive d'un mot dans le fichier dictionnaire
        /// </summary>
        /// <param name="debut">index debut</param>
        /// <param name="fin">index fin</param>
        /// <param name="mot">mot recherché</param>
        /// <returns>mot trouvé ou non</returns>
        public bool RechDichoRecursif(int debut, int fin, string mot) //IL EST INTERDIT D'ESSAYER DES MOTS DE MOINS D'UNE LETTRE 
        {
            List<string> tab = this.dictionnaire[mot.Length - 2];
            int milieu = (fin + debut) / 2;
            if(debut >= fin)
            {
                return false;
            }
            else
            {
                if(mot.CompareTo(tab[milieu]) == 0)
                {
                    return true;
                }
                else
                {
                    if(mot.CompareTo(tab[milieu]) == 1)
                    {
                        return RechDichoRecursif(milieu + 1, fin, mot);
                    }
                    else
                    {
                        return RechDichoRecursif(debut, milieu - 1, mot);
                    }
                }
            }
        }
    }
}
