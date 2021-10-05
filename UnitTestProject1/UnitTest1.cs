using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mixmo_PUJOL
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestToStringLettre() 
        {
            Lettre A = new Lettre('A', 10, 0); //Test affichage des caracteristiques d'une lettre
            string n = A.ToString();
            Assert.AreEqual(n, "La lettre sélectionnée est la lettre : " + 'A' + ". Il y a " + 10 + " fois la lettre et elle vaut : " + 0 + " points.");
        }

        [TestMethod]
        public void TestAdd()
        {
            Lettres Pioche = new Lettres("Lettre.txt");
            Random r = new Random();
            Lettres B = new Lettres(Pioche.Pioche, r);
            Joueur Joueur1 = new Joueur("Joueur1", B);
            string mot = "et c'est bien vrai!";
            List<string> liste1 =new List<string> { "marie", "est", "la", "plus", "gentille" };
            Joueur1.Mots_trouves = liste1;
            Joueur1.Add(mot);
            List<string> liste2 = new List<string> { "marie", "est", "la", "plus", "gentille", "et c'est bien vrai!" };
            for(int i=0; i<liste2.Count; i++)
            {
                Assert.AreEqual(Joueur1.Mots_trouves[i], liste2[i]);
            }

        }
        [TestMethod]
        public void TestGrillevide()
        {
            Lettres Pioche = new Lettres("Lettre.txt");
            Random r = new Random();
            Lettres lettresdujoueur2 = new Lettres(Pioche.Pioche, r);
            Joueur Joueur2 = new Joueur("Joueur2", lettresdujoueur2);
            Dictionnaire LePetitRobert = new Dictionnaire("MotsPossibles1.txt");
            MotsCroises J2 = new MotsCroises(Joueur2, LePetitRobert);
            bool flag = J2.Grillevide();
            Assert.AreEqual(flag, true);
        }
    }
}
