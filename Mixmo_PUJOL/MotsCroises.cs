using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixmo_PUJOL
{
    public class MotsCroises
    {
        //Champs
        Joueur joueur;
        Dictionnaire dico;

        //Constructeur
        public MotsCroises(Joueur J, Dictionnaire D)
        {
            this.joueur = J;
            this.dico = D;
        }

        //Propriété
        public Joueur JoueurMotsCroisés
        {
            get { return this.joueur; }
        }
        public Dictionnaire DicoMotsCroisés
        {
            get { return this.dico; }
        }
        //Methode
        /// <summary>
        /// 
        /// </summary>
        /// <returns>mot saisi par l'utilisateur</returns>
        public string SaisirMot()
        {
            Console.WriteLine("Saisissez un mot en MAJUSCULES avec les lettres que vous possédez :"); // J'affiche dans la console l'instruction demandé à l'utilisateur
            string mot = Convert.ToString(Console.ReadLine().ToUpper());
            return mot;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="M">Mot a traiter</param>
        /// <returns>mot traité vrai ou faux</returns>
        public bool Traitementdelaproposition(string M)
        {
            bool flag = false;
            string mot = M;
            if(mot.Length>=2)
            {
                Console.WriteLine("Le mot est bien composé d'au moins 2 lettres");
                if (joueur.VerificationLettrePossesion(mot) == true) //Verification que le mot demandé est réalisable compte tenu des lettres a disposition du joueur 
                {
                    Console.WriteLine("Les lettres du mot sont bien disponibles dans la main.");
                    if (dico.RechDichoRecursif(0, dico.Dico[mot.Length - 2].Count, mot) == true) //Verification que le mot demandé est bien dans le dictionnaire
                    {
                        Console.WriteLine("Le mot est bien Dans le dictionnaire.");
                        if (joueur.Mots_trouves.Contains(mot) == false) //Vérification que le mot n'a pas été trouvé précédemment
                        {
                            Console.WriteLine("Le mot n'a pas encore été trouvé.");
                            joueur.Add(mot); //On ajoute donc le mot trouvé dans la liste des mots trouvés par le joueur
                            joueur.OteLettre(mot); //On enleve les lettres utilisés dans le mot
                            flag = true; //Validation des opérations
                        }
                    }
                }
            }
            
            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Ligne saisi par l'utilisateur </returns>
        public int SaisirLigne()
        {
            Console.WriteLine("Un numéro de ligne ?");
            
            int lignedemandée = Convert.ToInt32(Console.ReadLine())-1; //Convertion des valeurs données par l'utilisateur
            return lignedemandée;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Colonne saisi par l'utilisateur</returns>
        public int SaisirColonne()
        {
            Console.WriteLine("Un numéro de colonne ?");
            int colonnedemandée = Convert.ToInt32(Console.ReadLine())-1;
            return colonnedemandée;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Orientation du mot demandée par l'utilisateur</returns>
        public int SaisirOrientation()
        {
            Console.WriteLine("Horizontal (1) ou verticale(0) ?");
            int placement = Convert.ToInt32(Console.ReadLine());
            return placement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Grille vide vrai ou faux </returns>
        public bool Grillevide()
        {
            bool flag = false;
            int cpt = 0;
            for(int i = 0 ; i<joueur.GrilleDeMotsCroises.GetLength(0); i++)
            {
                for( int j =0; j<joueur.GrilleDeMotsCroises.GetLength(1);j++)
                {
                    if(joueur.GrilleDeMotsCroises[i,j].Equals(' '))
                    {
                        cpt += 1;
                    }
                }
            }
            if(cpt== joueur.GrilleDeMotsCroises.Length)
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="M">premier mot a placer</param>
        public void PlacerMot(string M)
        {
            if(Traitementdelaproposition(M)==true) //Verification si le mot est contenu dans le dictionnaire, si les lettres du mot sont disponibles dans la main du joueur et si le mot n'a pas deja ete trouvé
            {
                Console.WriteLine("Saisissez une position selon le schéma suivant :"); // J'affiche dans la console l'instruction demandé à l'utilisateur
                int lignedemandée = SaisirLigne();
                int colonnedemandée = SaisirColonne();
                int placement = SaisirOrientation();
                for (int i = 0; i < M.Length; i++)
                {
                    if (placement == 0) //mot vertical
                    {
                        if (M.Length + lignedemandée <= joueur.GrilleDeMotsCroises.GetLength(0)) //Condition afin de ne pas sortir de la grille
                        {
                            joueur.GrilleDeMotsCroises[lignedemandée + i, colonnedemandée] = M[i];
                        }
                        else
                        {
                            Console.WriteLine("1) Vous ne pouvez pas effectuer cette opération. Perdu...");
                        }
                    }
                    if (placement == 1) //mot horizontal
                    {
                        if (M.Length + colonnedemandée <= joueur.GrilleDeMotsCroises.GetLength(1)) //Condition pour ne pas sortir de la ligne
                        {
                            joueur.GrilleDeMotsCroises[lignedemandée, colonnedemandée + i] = M[i];
                        }
                        else
                        {
                            Console.WriteLine("2) Vous ne pouvez pas effectuer cette opération. Perdu...");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("3) Le mot n'est pas valide. Perdu...");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lettre_du_mot"></param>
        /// <param name="L">ligne demandée par l'utilisateur</param>
        /// <param name="C">colonnne demandée par l'utilisateur</param>
        /// <returns>Le mot est possible</returns>
        public bool traiter_mots_verticaux(char lettre_du_mot,int L, int C) //Cette fonction permet de verifier les possibles mots verticaux créés pour chaque lettre d'un mot horizontal en fonction des lettres deja présentes dans la grille
        {
            bool flag = false;
            int i = L-1;
            string mon_mot = Convert.ToString(lettre_du_mot);
            while(i >= 0) //Condition pour ne pas sortir de la grille
            {
                if (joueur.GrilleDeMotsCroises[i, C].Equals(' ') == false) //On verifie si la case d'avant n'est pas un espace
                {
                    mon_mot = joueur.GrilleDeMotsCroises[i, C-1] + mon_mot; // si false ajout dune lettre de la grille au debut du mot
                    i = i - 1; //Case precedente
                }
                else
                {
                    i = -1; //Condition de sorti du while
                }
                Console.WriteLine("4)" + mon_mot);
            }
            int j = L + 1;
            while(j != joueur.GrilleDeMotsCroises.GetLength(0)+1) //Condition pour ne pas sortir de la grille
            {
                if (joueur.GrilleDeMotsCroises[j, C].Equals(' ') == false) //On verifie si la case d'avant n'est pas un espace
                {
                    mon_mot = mon_mot + joueur.GrilleDeMotsCroises[j, C];//si oui ajout dune lettre de la grille au debut du mot
                    j = j + 1; //Case suivante
                }
                else
                {
                    j = joueur.GrilleDeMotsCroises.GetLength(0) + 1; //Condition de sorti du while
                }
                Console.WriteLine("5)" + mon_mot);
            }
            if (mon_mot.Length > 1)
            {
                if (dico.RechDichoRecursif(0, dico.Dico[mon_mot.Length - 2].Count, mon_mot) == true) //Verification que le mot est bien dans le dictionnaire
                {
                    flag = true;
                }
            }
            if (mon_mot.Length == 1) //Si la lettre est entourée d'espace alors il n'y a pas de pb
            {
                flag = true;
            }
            return flag;
        }

        public bool traiter_mots_horizontaux(char lettre_du_mot, int L, int C) //Cette fonction permet de verifier les possibles mots horizontaux créés pour chaque lettre d'un mot vertical en fonction des lettres deja présentes dans la grille
        {
            bool flag = false;
            int i = C - 1;
            string mon_mot = Convert.ToString(lettre_du_mot);
            while (i >= 0) //Condition pour ne pas sortir de la grille
            {
                if (joueur.GrilleDeMotsCroises[L, i].Equals(' ') == false) //On verifie si la case d'avant n'est pas un espace
                {
                    mon_mot = joueur.GrilleDeMotsCroises[L, i] + mon_mot; //si oui ajout dune lettre de la grille au debut du mot
                    i = i - 1; //Case précedente
                }
                else
                {
                    i = -1; //Condition de sorti du while
                }
                Console.WriteLine("6)" + mon_mot);
            }
            int j = C + 1;
            while (j != joueur.GrilleDeMotsCroises.GetLength(1)+1)  //Condition pour ne pas sortir de la grille
            {
                if (joueur.GrilleDeMotsCroises[L, j].Equals(' ') == false)//On verifie si la case d'avant n'est pas un espace
                {
                    mon_mot = mon_mot + joueur.GrilleDeMotsCroises[L, j]; //si oui ajout dune lettre de la grille au debut du mot
                    j = j + 1; // Case suivante
                }
                else
                {
                    j = joueur.GrilleDeMotsCroises.GetLength(1)+1; //Condition de sorti du while
                }
                Console.WriteLine("7)" + mon_mot);
            }
            if(mon_mot.Length>1)
            {
                if (dico.RechDichoRecursif(0, dico.Dico[mon_mot.Length - 2].Count, mon_mot) == true) //Verification que le mot est bien dans le dictionnaire)
                {
                    flag = true;
                }
            }
            if(mon_mot.Length == 1) //Si le mot est entouré que d'espaces alors il n' y a aucun pb
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="M">Mot a placer</param>
        public void Placerlesmots(string M)
        {
            Console.WriteLine("Saisissez une position selon le schéma suivant :"); // J'affiche dans la console l'instruction demandé à l'utilisateur
            int lignedemandée = SaisirLigne();
            int colonnedemandée = SaisirColonne();
            int placement = SaisirOrientation();
            bool flag = false;
            if((M.Length>1)&&(dico.RechDichoRecursif(0, dico.Dico[M.Length - 2].Count, M) == true)) //Verification que le mot est bien dans le dictionnaire
            {
                if (joueur.Mots_trouves.Contains(M) == false) //Vérification que le mot n'a pas été trouvé précédemment
                {



                    if (placement == 0) //Pour un mot vertical
                    {
                        //On étudie ce qu'il y a avant le mot

                        string mon_mot = M; //Mot saisi par l'utilisateur
                        int i = lignedemandée - 1;
                        while (i >= 0)//Tant qu'on n'atteint pas la limite de la grille et que nous ne tombons pas sur un espace, on complete le mot saisi par les lettres déjà existantes
                        {
                            if (joueur.GrilleDeMotsCroises[i, colonnedemandée].Equals(' ') == false) //On verifie si la case d'avant n'est pas un espace
                            {
                                mon_mot = joueur.GrilleDeMotsCroises[i, colonnedemandée] + mon_mot;  //si oui ajout dune lettre de la grille au debut du mot
                                i = i - 1; //Case precedente
                               
                            }
                            else
                            {
                                i = -1; //Condition de sortie du while
                            }
                            Console.WriteLine("8)" + mon_mot);
                        }
                        int j = lignedemandée + M.Length;
                        while (j != joueur.GrilleDeMotsCroises.GetLength(0)) //Même méthode mais apres le mot
                        {
                            if (joueur.GrilleDeMotsCroises[j, colonnedemandée].Equals(' ') == false)
                            {
                                mon_mot = mon_mot + joueur.GrilleDeMotsCroises[j, colonnedemandée];
                                j = j + 1;
                            }
                            else
                            {
                                j = joueur.GrilleDeMotsCroises.GetLength(0);
                            }
                            Console.WriteLine("9)" + mon_mot);
                        }
                        if (mon_mot.Length > 1)
                        {
                            if (dico.RechDichoRecursif(0, dico.Dico[mon_mot.Length - 2].Count, mon_mot) == true) //Verification que le mot est bien dans le dictionnaire)
                            {
                                flag = true;
                            }
                        }
                        for (int t = 0; t < M.Length; t++) //on traite maintenant l'environnement latéral du mot grace en étudiant les possibles mots horizontaux créés
                        {
                            if (traiter_mots_horizontaux(M[t], lignedemandée + t, colonnedemandée) == false)
                            {
                                flag = false;
                                Console.WriteLine("La position demandée n'est pas valide.");
                            }
                        }
                    }
                    


                    if (placement == 1) //Pour un mot horizontal
                    {
                        //On étudie ce qu'il y a avant le mot

                        string mon_mot = M; //Mot saisi par l'utilisateur
                        int i = colonnedemandée-1;
                        while (i >= 0) //Tant qu'on n'atteint pas la limite de la grille et que nous ne tombons pas sur un espace, on complete le mot saisi par les lettres déjà existantes
                        {
                            if (joueur.GrilleDeMotsCroises[lignedemandée, i].Equals(' ') == false)
                            {
                                mon_mot = joueur.GrilleDeMotsCroises[lignedemandée, i] + mon_mot;
                                i = i - 1;
                            }
                            else
                            {
                                i=-1;
                            }
                            Console.WriteLine("10" + mon_mot);
                        }
                        int j = colonnedemandée + M.Length;
                        while(j != joueur.GrilleDeMotsCroises.GetLength(1)) //Même méthode mais apres le mot
                        {
                            if (joueur.GrilleDeMotsCroises[lignedemandée, j].Equals(' ') == false)
                            {
                                mon_mot = mon_mot + joueur.GrilleDeMotsCroises[lignedemandée, j];
                                j = j + 1;
                            }
                            else
                            {
                                j = joueur.GrilleDeMotsCroises.GetLength(1);
                            }
                            Console.WriteLine("11)" + mon_mot);
                        }
                        if (mon_mot.Length > 1)
                        {
                            if (dico.RechDichoRecursif(0, dico.Dico[mon_mot.Length - 2].Count, mon_mot) == true) //Verification que le mot est bien dans le dictionnaire)
                            {
                                flag = true;
                            }
                        }
                        for (int t=0; t< M.Length;t++) //on traite maintenant l'environnement latéral du mot grace en étudiant les possibles mots verticaux créés
                        {
                            if(traiter_mots_verticaux(M[t], lignedemandée, colonnedemandée+t)==false)
                            {
                                flag = false;
                                Console.WriteLine("12) La position demandée n'est pas valide.");
                            }
                        }
                    }
                }
            }
            if(flag==true) //Si le mot est possible, on le place dans la grille
            {
                for (int i = 0; i < M.Length; i++)
                {
                    if (placement == 0)
                    {
                        if (M.Length + lignedemandée <= joueur.GrilleDeMotsCroises.GetLength(0)) //&&(joueur.GrilleDeMotsCroises[lignedemandée - 1 + i, colonnedemandée - 1] == ' '))
                        {

                            joueur.GrilleDeMotsCroises[lignedemandée + i, colonnedemandée] = M[i];
                        }
                        else
                        {
                            Console.WriteLine("13) Vous ne pouvez pas effectuer cette opération. Perdu...");
                        }
                    }
                    if (placement == 1)
                    {
                        if (M.Length + colonnedemandée <= joueur.GrilleDeMotsCroises.GetLength(1)) //&& (joueur.GrilleDeMotsCroises[lignedemandée - 1, colonnedemandée - 1 + i] == ' '))
                        {
                            joueur.GrilleDeMotsCroises[lignedemandée, colonnedemandée + i] = M[i];
                        }
                        else
                        {
                            Console.WriteLine("14) Vous ne pouvez pas effectuer cette opération. Perdu...");
                        }
                    }
                }
                joueur.Add(M); //On ajoute donc le mot trouvé dans la liste des mots trouvés par le joueur
                joueur.OteLettre(M); //On enleve les lettres utilisés dans le mot
            }
            else
            {
                Console.WriteLine("15) Le mot ne marche pas, réessayez...");
            }
        }
    }
}


