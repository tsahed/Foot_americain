using ModelLayers.Business;
using ModelLayers.Data;
using System;


namespace foot_americain
{
    class Program
    {
        private static dbal mydbal;
        private static Joueur myJoueur;
        private static DAOjoueur mydaojoueur;

        static void Main(string[] args)
        {
            mydbal = new dbal("foot_americainDb");
            //mydaojoueur.Delete();
        }
    }
}
