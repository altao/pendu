using System;

namespace Pendu_19._12
{
    public class Mot
    {
        private string _MotATrouver;

        // Ici ont creer un acceseur equivalant a la fonction get set 
        public string motATrouver => _MotATrouver;

        private int _TailleDuMot; 
        public int tailleDuMot => _TailleDuMot;

        // Constructeur qui nous permet de recuperer la taille du mot piocher
        public Mot(string val) {
            _MotATrouver = val;
            _TailleDuMot = val.Length;
        }
        
            
    }
}
