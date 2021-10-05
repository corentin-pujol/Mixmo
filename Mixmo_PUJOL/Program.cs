using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixmo_PUJOL
{
    class Program
    {
        static void Affichermatrice(char[,] matrice)
        {
            if (matrice == null)
            {
                Console.WriteLine("La matrice est null");
            }
            else if ((matrice.GetLength(0) == 0) && (matrice.GetLength(1) == 0))
            {
                Console.WriteLine("La matrice est vide");
            }
            else
            {
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < matrice.GetLength(1) - 1; j++)
                    {
                        Console.Write(matrice[i, j]);
                        Console.Write('|');
                    }
                    Console.Write(matrice[i, matrice.GetLength(1) - 1]);
                    Console.WriteLine();
                }
            }
        }
        static void AfficherList(List<string> motstrouvés)
        {
            if(motstrouvés == null)
            {
                Console.WriteLine("La collection est nulle");
            }
            else if (motstrouvés.Count == 0)
            {
                Console.WriteLine("La collection de mots trouvés est vide");
            }
            else
            {
                Console.WriteLine("Les mots déjà trouvés sont : ");
                for (int i = 0; i < motstrouvés.Count; i++)
                {
                    Console.Write(motstrouvés[i] +";") ;
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Lettres Pioche = new Lettres("Lettre.txt");
            Random r = new Random();
            Dictionnaire LePetitRobert = new Dictionnaire("MotsPossibles1.txt");
            Console.WriteLine("Bienvenue dans le MIXMO, a combien souhaitez vous jouer ?");
            int nbre_de_joueurs = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Définir un nombre de manche ? (une manche signifie un mot trouvé)"); //La partie s'arretera apres ce nombre de mots trouvés
            int nombredetours = Convert.ToInt32(Console.ReadLine());
            List<Joueur> MylistJoueurs = new List<Joueur>();
            List<MotsCroises> MylistMotscroises = new List<MotsCroises>();
            for (int z = 0; z < nbre_de_joueurs; z++)
            {
                Console.WriteLine("Saisissez votre nom : ");
                string J = Console.ReadLine();
                Joueur joueur = new Joueur(J, new Lettres(Pioche.Pioche, r));
                MylistJoueurs.Add(joueur);
                MylistMotscroises.Add(new MotsCroises(joueur, LePetitRobert));
            }
            while (Pioche.Pioche.Count > 0)
            {
                foreach (MotsCroises n in MylistMotscroises)
                {
                    if(Pioche.Pioche.Count==0)
                    {
                        Console.WriteLine("La partie est terminée.");
                    }
                    else
                    {
                        Console.WriteLine("C'est au tour de " + n.JoueurMotsCroisés.Nom_joueur + " de jouer.");
                        Console.WriteLine(n.JoueurMotsCroisés.Afficher_grille_de_mots_croises());
                        Console.WriteLine(n.JoueurMotsCroisés.Mainjoueur.ToString_Joueur());
                        AfficherList(n.JoueurMotsCroisés.Mots_trouves);
                        string mot = n.SaisirMot();
                        if (n.Grillevide() == true)
                        {

                            n.PlacerMot(mot);
                            Console.WriteLine(n.JoueurMotsCroisés.Afficher_grille_de_mots_croises());
                            n.JoueurMotsCroisés.Add_Lettres(2, Pioche, r);
                        }
                        else
                        {
                            n.Placerlesmots(mot);
                            Console.WriteLine(n.JoueurMotsCroisés.Afficher_grille_de_mots_croises());
                            n.JoueurMotsCroisés.Add_Lettres(2, Pioche, r);
                        }

                        if (n.JoueurMotsCroisés.Mots_trouves.Count == nombredetours)
                        {
                            Console.WriteLine("La partie est finie, voici les résultats.");
                            for (int j = 0; j < MylistJoueurs.Count; j++)
                            {
                                Console.WriteLine(MylistJoueurs[j].Nom_joueur + ": " + MylistJoueurs[j].calcul_du_score());
                                Pioche.Pioche.Clear();
                            }
                        }
                    }
                }
            }






































            //////Console.WriteLine(Pioche.ToString_Pioche());
            //Lettres lettresdujoueur1 = new Lettres(Pioche.Pioche, r);
            //Lettres lettresdujoueur2 = new Lettres(Pioche.Pioche, r);
            ////Console.WriteLine(Pioche.ToString_Pioche());
            //Joueur Joueur1 = new Joueur("Joueur1", lettresdujoueur1);
            //Joueur Joueur2 = new Joueur("Joueur2", lettresdujoueur2);
            //Console.WriteLine("Saisissez le nombre de joueur");
            //int nbredejoueurs = Convert.ToInt32(Console.ReadLine());
            //Affichermatrice(Joueur1.GrilleDeMotsCroisés);
            ////Console.WriteLine(Joueur1.Afficher_grille_de_mots_croisés());
            //Console.WriteLine(Joueur1.ToString());
            //Joueur1.Add_Lettres(2, Pioche, r);
            //Console.WriteLine(Joueur1.ToString());
            //foreach(string n in LePetitRobert.Dico[1])
            //{
            //    Console.WriteLine(n);
            //}
            //Console.WriteLine(LePetitRobert.Dico[1].Count.ToString());
            ////Console.WriteLine(LePetitRobert.afficherdico()); la methode to string est inutile car nous ne pouvons pas afficher une chaine de caractères de 500000 mots...
            //string mot = "COCO";
            //Console.WriteLine(LePetitRobert.RechDichoRecursif(0, LePetitRobert.Dico[mot.Length - 2].Count, mot));
            //MotsCroises J2 = new MotsCroises(Joueur2, LePetitRobert);
            //string mot1 = J1.SaisirMot();
            //Console.WriteLine(J1.Traitementdelaproposition(mot1));
            //J1.PlacerMot(mot1);
            //Console.WriteLine(Joueur1.Afficher_grille_de_mots_croisés());
            //Console.WriteLine(lettresdujoueur1.ToString_Joueur());
            //Console.WriteLine(lettresdujoueur2.ToString_Joueur());
            //string mot2 = J2.SaisirMot();
            //Console.WriteLine(J2.Traitementdelaproposition(mot2));
            //Console.WriteLine(lettresdujoueur2.ToString_Joueur());
            //PlacerMot(string M)
            Console.ReadKey();
        }
    }
}
