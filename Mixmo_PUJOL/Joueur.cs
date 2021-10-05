using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixmo_PUJOL
{
    public class Joueur
    {
        //Champ
        private string nom_joueur;
        private int score_joueur;
        private Lettres mainjoueur;
        private List<string> mots_trouves;
        private char[,] grille_de_mots_croises;

        //Constructeur
        public Joueur(string Nom_du_joueur, Lettres MdJ)
        {
            this.nom_joueur = Nom_du_joueur;
            List<string> MT = new List<string>(); //Liste initialement vide des mots trouvés
            this.mots_trouves = MT;
            this.mainjoueur = MdJ;
            int score = 0; //On initialise le score à zéro
            this.score_joueur = score;
            char[,] grille = new char[20, 20];
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j ++)
                {
                    grille[i, j] = ' ';
                }
            }
            this.grille_de_mots_croises = grille;
        }
        //Propriétés
        public string Nom_joueur
        {
            get { return this.nom_joueur; }
            set { this.nom_joueur = value; }
        }
        public int Score_joueur
        {
            get { return this.score_joueur; }
            set { this.score_joueur = value; }
        }
        public Lettres Mainjoueur
        {
            get { return this.mainjoueur; }
            set { this.mainjoueur = value; }
        }
        public List<string> Mots_trouves
        {
            get { return this.mots_trouves; }
            set { this.mots_trouves = value; }
        }
        public char[,] GrilleDeMotsCroises
        {
            get { return this.grille_de_mots_croises; }
            set { this.grille_de_mots_croises = value; }
        }

        //Méthodes
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Grille de mots croisés</returns>
        public string Afficher_grille_de_mots_croises()
        {
            string grille = "";
            int colonne = 1;
            int ligne = 1;
            for(int i = 0; i<this.grille_de_mots_croises.GetLength(0)+2; i++)
            {
                for(int j = 0; j<this.grille_de_mots_croises.GetLength(1)+2; j++)
                {
                    if(i==0)
                    {
                        if(j<3)
                        {
                            grille += "  ";
                        }
                        else
                        {
                            if(colonne<=9)
                            {
                                grille += ""+Convert.ToString(colonne) + "   ";
                                colonne += 1;
                            }
                            if(colonne>9)
                            {
                                grille += Convert.ToString(colonne) + "  ";
                                colonne += 1;
                            }
                        }
                    }
                    if(i==1)
                    {
                        grille += "____";
                    }
                    if(i>1)
                    {
                        if(j==0)
                        {
                            if(ligne<10)
                            {
                                grille += Convert.ToString(ligne)+" ";
                            }
                            if(ligne>=10)
                            {
                                grille += Convert.ToString(ligne);
                            }
                            ligne += 1;
                        }
                        if((j>0)&&(j<3))
                        {
                            grille += " ";
                        }
                        if(j>=3)
                        {
                            grille += "| " + this.grille_de_mots_croises[i-2, j-3]+" ";
                        }
            
                    }
                }
                if (i > 1) grille += "| " + this.grille_de_mots_croises[i - 2, grille_de_mots_croises.GetLength(1)-1];
                grille += "\n";
            }
            return grille;
        }
        //public override string ToString()
        //{
        //    return Afficher_grille_de_mots_croisés() + "\n" + "Joueur : " + this.nom_joueur + "\n" + this.mainjoueur.ToString_Joueur() + "\n" + "Score : " + this.score_joueur;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nb">nombre de lettres a ajouter </param>
        /// <param name="P">pioche</param>
        /// <param name="lettre_aleatoire">random</param>
        /// <returns></returns>
        public bool Add_Lettres(int nb, Lettres P, Random lettre_aleatoire)
        {
            bool flag = false;
            for (int i = 0; i < nb; i++)
            {
                int index_d_une_lettre_aleatoire = lettre_aleatoire.Next(0, P.Pioche.Count); //création d'un index aléatoire
                Lettre Lettre_aleatoire = P.Pioche[index_d_une_lettre_aleatoire];
                this.mainjoueur.Main_du_joueur.Add(Lettre_aleatoire); //Ajout dans la main d'un joueur d'une lettre choisi au hasard dans la pioche
                P.Pioche.Remove(Lettre_aleatoire); //Suppression de l'élément dans la pioche
                if (Lettre_aleatoire.Nombre_de_lettres > 0)
                {
                    Lettre_aleatoire.Nombre_de_lettres -= 1; //On met à jour la fréquence de la lettre 
                }
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mot">mot</param>
        public void Add(string mot)
        {
            this.mots_trouves.Add(mot); //On ajoute le mot a la liste des mots trouvés par le joueur
        }
        public void OteLettre(string mot)
        {
           foreach (char n in mot)
                {
                for (int i = 0; i < mainjoueur.Main_du_joueur.Count; i++)
                    {
                    Lettre L = this.mainjoueur.Main_du_joueur[i];
                    if (n == L.Symbole) //On verifie si la lettre a supprimer est la bonne 
                        {
                            this.mainjoueur.Main_du_joueur.Remove(L); //On supprime de la liste des lettres en main du joueur
                            i = mainjoueur.Main_du_joueur.Count;
                        }
                    }
                }     
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mot">mot</param>
        /// <returns>lettre en possession dans la main vrai ou faux</returns>
        public bool VerificationLettrePossesion(string mot)
        {
            bool flag = false;
            int cpt = 0;
            List<Lettre> L = this.mainjoueur.Main_du_joueur;
            foreach (char n in mot)
            {
                for(int i = 0; i< L.Count;i++) //On boucle sur la chaine de caractère du mot et sur les lettres en possession du joueur 
                {
                    if(n==L[i].Symbole) //Si la lettre dans le mot est contenue dans les lettres du joueur, alors on change l'etat du flag
                    {
                        cpt = cpt + 1;
                        i = L.Count - 1; //On considère que la condition nécessaire et suffisante pour avoir le droit de faire un mot est d'avoir au moins une fois les lettres composant le mot souhaité
                    }
                }
            }
            if(cpt == mot.Length)
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>score d'un joueur</returns>
        public int calcul_du_score()
        {
            this.score_joueur = 0; 
            for(int i = 0; i<this.Mots_trouves.Count;i++)
            {
                for(int j = 0; j<mots_trouves[i].Length;j++)
                {
                    if(mots_trouves[i][j].Equals('K'))
                    {
                        this.score_joueur += 5;
                    }
                    if (mots_trouves[i][j].Equals('W'))
                    {
                        this.score_joueur += 5;
                    }
                    if (mots_trouves[i][j].Equals('X'))
                    {
                        this.score_joueur += 5;
                    }
                    if (mots_trouves[i][j].Equals('Y'))
                    {
                        this.score_joueur += 5;
                    }
                    if (mots_trouves[i][j].Equals('Z'))
                    {
                        this.score_joueur += 5;
                    }

                }
                if(mots_trouves[i].Length>=2) //Un mot de n lettres apportent n points.
                {
                    this.score_joueur += mots_trouves[i].Length;
                }
            }
            return this.score_joueur;
        }
    }
}
