using Pendu_19._12;
using System;

namespace ConsoleApp4
{
    class Program
    {
        // Declaration des differents variable a utiliser pour le pendu
        static string[] mots = { "furet", "barbe", "moustache" }; // String qui contient a la base une suite de carachteres. Ici de type tableau qui contient donc plusieurs mots. Dans ce tableau seront piocher le mot deviner. 
        static Mot motAtrouver; // Recuperation des classe qui appartienne au script mot
        static ascii ascii = new ascii(); // Recuperation des classe qui appartienne au script ascii
        static char lettre; // Stock la lettre taper par le joueur. 
        static char[] lettreADevinees;//array de caractères. Qui va contenir le nombres d'etoiles en fonction du mot piocher.
        static bool victoire; // Bool qui permet de definir l'etat de victoire. Victoire ou loose. False/ true. 
        static int vie; // Contient un nombre entier ( vie du joueur qui evolue en fonction du jeux du joueur ) 
        static void Main(string[] args) // fonction principale lu par le programme en premier, point d'entrée
        {
            Pendu(); // fonction de la mechanic du jeux. Qui se lance au debut du jeux.
        }

        private static void Pendu()
        {
            AfficherAccueil(); // Message de presantation lors du lancement du jeux.

            ChoisirUnMot(); // Premiere etape pour que le jeux fonction. Fonction capable de choisir un mot aleatoirement depuis notre tableau de mot.
            CrypterMot(); // Permet d'encoder le mot. 

            while (GameOver() == false) // S'effectue tant que la condition est vrai ( Tant que gameover n'es pas vrai alors la boucle se continue ).
            {
                Console.WriteLine(AfficherMot()); // Affiche le mot sous forme d'etoiles
                ChoisirUneLettre(); // On demande au joueur de choisir une lettre. Et lire la lettre taper. 
                TesterLettre(); // Permet de lire la lettre taper par le joueur. Et remplacer le charachtere par la lettre si elle a etait trouver. Sinon ont diminue la vie avec un message.
            }
            FinirPartie(); // Fonction qui permet d'afficher le message de victoire ou gameover en fonction du bool victoire,
            Rejouer();
        }

        // texte de bienvenue lors du commencement de la partie 
        private static void AfficherAccueil()

        {
            vie = 7; // Declare le nombre de vie 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" ---------- Bienvenue sur le jeu du pendu ! ---------- ");
            Console.WriteLine("\n");
            Console.WriteLine("Vous avez " + vie + " pv pour trouver le bon mot.");
            Console.WriteLine("\n");
            Console.WriteLine("Allez-y");
            Console.ResetColor();

        }

        // pioche un mot parmis les mots du static string ligne 9
        private static void ChoisirUnMot()
        {
            Random random = new Random();
            int dblRandom = random.Next(0, mots.Length);
            motAtrouver = new Mot(mots[dblRandom]); // Stokage du mot piocher dans la variable motATrouver. 
        }
        
        private static void FinirPartie()
        {
            if (victoire) // Si victoire est parametrer a vrai alors on affiche le message de felicitation + le mot deviner
            {
                Console.ForegroundColor = ConsoleColor.Green; // couleur si c'est win en VERT
                Console.WriteLine("Vous avez gagné, le mot était " + motAtrouver.motATrouver);
                Console.ResetColor();
            }
            else // Si victoire est rester a false alors on affiche le message de gameover plus le mot qui etait a deviner.
            {
                Console.ForegroundColor = ConsoleColor.Red; // couleur si c'est loose en rouge
                Console.WriteLine("Vous avez perdu, le mot était " + motAtrouver.motATrouver);
                Console.ResetColor();
            }
        }
        
        // Remplacement les lettres du mot par des etoiles un fonction du nombre de lettres dans le mot, pour pouvoir l'afficher au joueur.
        private static void CrypterMot()
        {
            lettreADevinees = new char[motAtrouver.tailleDuMot]; // Creation autant de charachtere par rapport a la taille du mot. 
            for (int i = 0; i < lettreADevinees.Length; i++)
            {
                lettreADevinees[i] = '*';
            }
        }
        private static string AfficherMot() // affiche dans un nouveau string MotAfficher, les lettres a deviner 
        {
            string motAfficher = new string(lettreADevinees);
            return motAfficher;
        }
        
        private static void ChoisirUneLettre()
        {
            Console.ForegroundColor = ConsoleColor.Blue; // demande de choisir une lettre en couleur bleu
            Console.WriteLine("Veuillez saisir une lettre");
            lettre = Console.ReadLine()[0]; // Lecture de la lettre taper par le joueur. 
        }
        private static void TesterLettre()
        {
            bool contient = false; // Condition qu'on passe vrai ou faux si la lettre est contenu ou non dans le mot.

            for (int i = 0; i < motAtrouver.tailleDuMot; i++) // Baleye une a une toute les lettres du mot.
            {
                if (motAtrouver.motATrouver[i] == lettre) // Si la lettre taper est contenu dans le mot.
                {
                    contient = true; // Alors contient passe en true.
                    lettreADevinees[i] = lettre; // Puis ont remplace le carachtere en question par la lettre sesi.
                    Console.WriteLine(lettreADevinees); // puis ont reaffiche le tout. 
                }
            }
            if (!contient) // Si la lettre n'es pas contenu dans le mot
            {
                Console.WriteLine("Le mot ne contient pas la lettre " + lettre); // alors affichage de la lettre qui ne correspond pas
                vie--; // Decrementation de la vie 
                Console.WriteLine("Il vous reste " + vie + "vie"); // Message du nombre de vie restant 
                Console.WriteLine(ascii.dessinAscii[vie]); // On vien charger le dessin en ascii en fonction de la vie restante. 
            }
            Console.WriteLine("\n ");
        }

        // mise en place du game over dans 2 cas de figure.
        private static bool GameOver()
        {
            if (vie <= 0) // Si la vie est inferieur ou egal a 0 alors on retourne true. GameOver est true = perdu
            {
                victoire = false; // Bool pour activer le message de game over 
                return true;
            }
            foreach (char chara in lettreADevinees) 
            {
                if (chara == '*') // Si tout les charachtere etoiles sont remplacer par les bonnes lettres alors ont retourne false. GameOver est false = Victoire c'est pour cela que ligne 56 victoire est egale a true.
                {
                    return false;
                }
            }
            victoire = true; // bool pour activer le message de victoire. 
            return true;
        }


        // fin du jeux en avec une fonction restart pour recommencer le jeux ou bien de leave la partie avec une commande Y/N pour yes ou no 
        private static void Rejouer()
        {
            ConsoleKey reponse;


            do
            {
                Console.WriteLine("Voulez vous rejouer (Y/N)");
                reponse = Console.ReadKey(false).Key;
                if (reponse != ConsoleKey.Enter)
                    Console.WriteLine();

            } while (reponse != ConsoleKey.Y && reponse != ConsoleKey.N);

            if (reponse == ConsoleKey.Y) // La lettre Y permet de rejouer.
            {
                Console.WriteLine("La partie recommence");
                Pendu(); // En relancant la fonction pendu Ligne 21.  
            }
            else
            {
                Console.WriteLine("Au revoir !");
            }
        }
    }





}