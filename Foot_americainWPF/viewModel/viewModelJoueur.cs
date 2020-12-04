using ModelLayers.Business;
using ModelLayers.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Foot_americainWPF.viewModel
{
    class viewModelJoueur : viewModelBase
    {
        private DAOjoueur vmDaoJoueur;
        private DAOequipe vmDaoEquipe;
        private DAOpays vmDaoPays;
        private DAOposte vmDaoPoste;
        private ICommand updateCommand;
        private ICommand deleteCommand;
        private ICommand addCommand;
        private ObservableCollection<Joueur> listJoueurs;
        private ObservableCollection<Equipe> listEquipe;
        private ObservableCollection<Poste> listPostes;
        private ObservableCollection<Pays> listPays;
        private Equipe selectedEquipe = new Equipe();
        private Equipe activeEquipe = new Equipe();
        private Joueur selectedJoueur = new Joueur();

        public ObservableCollection<Joueur> ListJoueurs { get => listJoueurs; set => listJoueurs = value; }
        public ObservableCollection<Equipe> ListEquipe { get => listEquipe; set => listEquipe = value; }
        public ObservableCollection<Poste> ListPostes { get => listPostes; set => listPostes = value; }
        public ObservableCollection<Pays> ListPays { get => listPays; set => listPays = value; }

        public string Nom
        {
            get 
            {
                if (selectedJoueur != null)
                    return selectedJoueur.Nom;
                else
                    return null;
            }
            set
            {
                if (selectedJoueur.Nom != value)
                {
                    selectedJoueur.Nom = value;
                    OnPropertyChanged("Nom");
                }
            }
        }

        public Pays Pays
        {
            get
            {
                if (selectedJoueur != null)
                    return selectedJoueur.Pays;
                else
                    return null;
            }
            set
            {
                if (selectedJoueur.Pays != value)
                {
                    selectedJoueur.Pays = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("Pays");
                }
            }
        }

        public Poste Poste
        {
            get
            {
                if (selectedJoueur != null)
                    return selectedJoueur.Poste;
                else
                    return null;
            }
            set
            {
                if (selectedJoueur.Poste != value)
                {
                    selectedJoueur.Poste = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("Pays");
                }
            }
        }

        public DateTime Naissance
        {
            get
            {
                if (selectedJoueur != null)
                    return selectedJoueur.DateNaissance;
                else
                    return new DateTime();
            }
            set
            {
                if (selectedJoueur.DateNaissance != value)
                {
                    selectedJoueur.DateNaissance = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("DateNaissance");
                }
            }
        }

        public DateTime Entree
        {
            get
            {
                if (selectedJoueur != null)
                    return selectedJoueur.DateEntree;
                else
                    return new DateTime();
            }
            set
            {
                if (selectedJoueur.DateEntree != value)
                {
                    selectedJoueur.DateEntree = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("DateEntree");
                }
            }
        }

        public Equipe SelectedEquipe
        {
            get => selectedEquipe;
            set
            {
                if (selectedEquipe != value)
                {
                    selectedEquipe = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("SelectedEquipe");
                    OnPropertyChanged("Nom");
                    OnPropertyChanged("DateCreation");
                    OnPropertyChanged("ListEquipe");
                }
            }
        }


        public Joueur SelectedJoueur
        {
            get => selectedJoueur;
            set
            {
                if (selectedJoueur != value)
                {
                    selectedJoueur = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("SelectedJoueur");
                    OnPropertyChanged("Nom");
                    OnPropertyChanged("Pays");
                    OnPropertyChanged("Naissance");
                    OnPropertyChanged("Poste");
                    OnPropertyChanged("Entree");
                }
            }
        }



        //déclaration du contructeur de viewModelFromage
        public viewModelJoueur(DAOpays thedaopays, DAOposte thedaoposte, DAOjoueur thedaojoueur, DAOequipe thedaoequipe)
        {
            vmDaoPays = thedaopays;
            listPays = new ObservableCollection<Pays>(thedaopays.SelectAll());

            vmDaoPoste = thedaoposte;
            listPostes = new ObservableCollection<Poste>(thedaoposte.SelectAll());
            
            vmDaoJoueur = thedaojoueur;
            listJoueurs = new ObservableCollection<Joueur>(thedaojoueur.SelectAll());

            vmDaoEquipe = thedaoequipe;
            listEquipe = new ObservableCollection<Equipe>(thedaoequipe.SelectAll());

            foreach (Joueur lejoueur in ListJoueurs)
            {
                int i = 0;
                while (lejoueur.Pays.Id != listPays[i].Id)
                {
                    i++;
                }
                lejoueur.Pays = listPays[i];
            }

            foreach (Joueur lejoueur in ListJoueurs)
            {
                int i = 0;
                while (lejoueur.Poste.Id != listPostes[i].Id)
                {
                    i++;
                }
                lejoueur.Poste = listPostes[i];
            }
        }

        //Méthode appelée au click du bouton UpdateCommand
        public ICommand UpdateCommand
        {
            get
            {
                if (this.updateCommand == null)
                {
                    this.updateCommand = new RelayCommand(() => UpdateJoueur(), () => true);
                }
                return this.updateCommand;
            }

        }

        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(() => DeleteJoueur(), () => true);
                }
                return this.deleteCommand;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (this.addCommand == null)
                {
                    this.addCommand = new RelayCommand(() => AddJoueur(), () => true);
                }
                return this.addCommand;
            }
        }

        private void UpdateJoueur()
        {
            Joueur backup = new Joueur();
            backup = SelectedJoueur;
            this.vmDaoJoueur.Update(this.SelectedJoueur, this.SelectedEquipe);
            int a = listJoueurs.IndexOf(SelectedJoueur);
            listJoueurs.Insert(a, SelectedJoueur);
            listJoueurs.RemoveAt(a + 1);
            SelectedJoueur = backup;
            MessageBox.Show("Mis à jour réussis");
        }

        private void AddJoueur()
        {
            Joueur select = new Joueur();
            this.vmDaoJoueur.Insert(this.SelectedJoueur, this.SelectedEquipe);
            listJoueurs.Add(this.SelectedJoueur);
            select = this.SelectedJoueur;
            SelectedJoueur = select;
            MessageBox.Show("Fromage ajouté");
        }

        private void DeleteJoueur()
        {
            Joueur backup = new Joueur();
            backup = SelectedJoueur;
            this.vmDaoJoueur.Delete(this.SelectedJoueur);
            int a = listJoueurs.IndexOf(SelectedJoueur);
            listJoueurs.RemoveAt(a);
            MessageBox.Show("Fromage supprimé");
        }
    }
}
