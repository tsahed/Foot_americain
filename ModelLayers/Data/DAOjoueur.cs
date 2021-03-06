﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ModelLayers.Business;
using ModelLayers.Data;


namespace ModelLayers.Data
{
    public class DAOjoueur
    {
        #region Attributs
        private dbal thedbal;
        private DAOpays leDAOPays;
        private DAOposte leDAOPoste;
        private DAOequipe leDAOEquipe;
        #endregion

        #region Constructeurs
        public DAOjoueur(dbal mydbal, DAOpays leDAOPays, DAOposte leDAOPoste, DAOequipe leDAOEquipe)
        {
            this.thedbal = mydbal;
            this.leDAOPays = leDAOPays;
            this.leDAOPoste = leDAOPoste;
            this.leDAOEquipe = leDAOEquipe;
        }
        #endregion

        public void Insert(Joueur leJoueur, Equipe laEquipe)
        {
            string requete = "Joueur (id, nom, dateEntree, dateNaissance, pays, poste, equipe) VALUES ("
                + leJoueur.Id + ", '"
                + leJoueur.Nom + "', '"
                + leJoueur.DateEntree.ToString("yyyy-MM-dd") + "', '"
                + leJoueur.DateNaissance.ToString("yyyy-MM-dd") + "', "
                + leJoueur.Pays.Id + ", "
                + leJoueur.Poste.Id + ", "
                + laEquipe.Id + ")";
            this.thedbal.Insert(requete);
        }

        public void Update(Joueur leJoueur, Equipe laEquipe)
        {
            string requete = "Joueur SET id = " + leJoueur.Id
                + ", nom = '" + leJoueur.Nom.Replace("'", "''")
                + "', dateEntree = '" + leJoueur.DateEntree.ToString("yyyy-MM-dd")
                + "', dateNaissance = '" + leJoueur.DateNaissance.ToString("yyyy-MM-dd")
                + "', pays = '" + leJoueur.Pays.Id
                + "', poste = '" + leJoueur.Poste.Id
                + "', equipe = '" + laEquipe.Id
                + "' WHERE id = " + leJoueur.Id + ";" ;
            this.thedbal.Update(requete);
        }

        public void Delete(Joueur leJoueur)
        {
            string requete = "Joueur where id = '" + leJoueur.Id + "'";
            this.thedbal.Delete(requete);
        }

        public List<Joueur> SelectAll()
        {
            List<Joueur> listJoueur = new List<Joueur>();
            DataTable myTable = this.thedbal.SelectAll("Joueur");

            foreach (DataRow r in myTable.Rows)
            {
                Pays myPays = this.leDAOPays.SelectById((int)r["id"]);
                Poste myPoste = this.leDAOPoste.SelectById((int)r["id"]);
                listJoueur.Add(new Joueur((int)r["id"], (string)r["nom"], (DateTime)r["dateEntree"], (DateTime)r["dateNaissance"], myPays, myPoste));
            }

            return listJoueur;
        }

        public Joueur SelectById(int id)
        {
            DataRow rowJoueur = this.thedbal.SelectById("Joueur", id);
            Pays myPays = this.leDAOPays.SelectById((int)rowJoueur["id"]);
            Poste myPoste = this.leDAOPoste.SelectById((int)rowJoueur["id"]);
            return new Joueur((int)rowJoueur["id"], (string)rowJoueur["nom"], (DateTime)rowJoueur["dateEntree"], (DateTime)rowJoueur["dateNaissance"], myPays, myPoste);
        }

        public Joueur SelectByName(string name)
        {
            string search = "nom = '" + name + "'";
            DataTable tableJoueur = this.thedbal.SelectByField("Joueur", search);
            Pays myPays = this.leDAOPays.SelectById((int)tableJoueur.Rows[0]["id"]);
            Poste myPoste = this.leDAOPoste.SelectById((int)tableJoueur.Rows[0]["id"]);
            return new Joueur((int)tableJoueur.Rows[0]["id"], (string)tableJoueur.Rows[0]["nom"], (DateTime)tableJoueur.Rows[0]["dateEntree"], (DateTime)tableJoueur.Rows[0]["dateNaissance"], myPays, myPoste);
        }
    }
}
