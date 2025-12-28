# Application de Gestion de Location de Voitures

## Description du Projet

Cette application est un système de gestion de location de voitures développé en .NET 8. Elle comprend deux interfaces distinctes :

1. **BackOffice Desktop (WinForms)** : Application Windows pour les administrateurs
2. **FrontOffice Web (ASP.NET Core MVC)** : Application web pour les clients

## Architecture

### Structure du Projet

```
DOTNETHYO4/
├── Data/                    # Bibliothèque de données (Entity Framework Core)
│   ├── Entities/           # Entités du domaine
│   └── CarRentalDbContext.cs
├── BackOffice.Desktop/      # Application WinForms pour Admin
│   └── Forms/              # Formulaires Windows
├── FrontOffice.Web/        # Application Web MVC pour Clients
│   ├── Controllers/        # Contrôleurs MVC
│   ├── Views/             # Vues Razor
│   └── Models/            # ViewModels
└── README.md
```

## Technologies Utilisées

- **.NET 8.0**
- **Entity Framework Core 8.0** (Code First)
- **SQL Server LocalDB**
- **WinForms** (pour l'application Admin)
- **ASP.NET Core MVC** (pour l'application Client)
- **QRCoder** (pour la génération de QR Codes)

## Base de Données

### Entités

1. **Admin** : Compte administrateur (ne peut pas être créé via l'interface)
2. **Client** : Compte client (peut être créé via le site web)
3. **Vehicle** : Véhicule disponible à la location
4. **Rental** : Location de véhicule

### Relations

- Un Client peut avoir plusieurs Rentals
- Un Vehicle peut avoir plusieurs Rentals

### Configuration

La base de données est créée automatiquement au premier lancement avec :
- **Serveur** : `(localdb)\mssqllocaldb`
- **Base de données** : `CarRentalDb`

## Authentification

### Logique d'Authentification

- **Compte Admin** : Ne peut PAS être créé via l'interface utilisateur. Il est pré-configuré dans la base de données.
- **Compte Client** : Peut être créé via le site web (page d'inscription).

### Compte Admin par Défaut

- **Username** : `admin`
- **Password** : `admin123`
- **Email** : `admin@carrental.com`

⚠️ **Note** : En production, les mots de passe doivent être hashés. Pour ce projet étudiant, ils sont stockés en clair.

## Fonctionnalités

### BackOffice Desktop (Admin)

L'application WinForms permet aux administrateurs de :

1. **Gérer les Véhicules** (CRUD)
   - Ajouter un véhicule
   - Modifier un véhicule
   - Supprimer un véhicule
   - Voir la liste des véhicules

2. **Gérer les Clients** (CRUD)
   - Ajouter un client
   - Modifier un client
   - Supprimer un client
   - Voir la liste des clients

3. **Consulter les Locations**
   - Voir toutes les locations
   - Voir les détails (client, véhicule, dates, montant, statut)

### FrontOffice Web (Client)

L'application web permet aux clients de :

1. **Créer un compte client**
   - Formulaire d'inscription avec validation
   - Vérification de l'unicité de l'email

2. **Voir les véhicules disponibles**
   - Liste des véhicules disponibles
   - Informations détaillées (marque, modèle, année, couleur, prix journalier)

3. **Demander une location**
   - Sélection d'un véhicule
   - Choix des dates de début et de fin
   - Calcul automatique du montant total
   - Génération d'un QR Code pour la location
   - Envoi d'un email de confirmation (simulé)

## Fonctionnalités Supplémentaires

### QR Code

Chaque location génère automatiquement un QR Code contenant :
- ID de la location
- Informations du client
- Informations du véhicule
- Dates de début et de fin

Le QR Code est accessible via : `/Rentals/QrCode/{id}`

### Email de Confirmation

Après chaque demande de location, un email de confirmation est envoyé (simulé dans les logs). En production, il faudrait configurer un service d'envoi d'emails réel.

## Comment Exécuter le Projet

### Prérequis

1. **Visual Studio 2022** ou **Visual Studio Code** avec les extensions .NET
2. **.NET 8 SDK** installé
3. **SQL Server LocalDB** (généralement inclus avec Visual Studio)

### Étapes d'Installation

1. **Cloner ou télécharger le projet**

2. **Restaurer les packages NuGet**
   ```bash
   dotnet restore
   ```

3. **Compiler la solution**
   ```bash
   dotnet build
   ```

4. **Créer la base de données**
   La base de données sera créée automatiquement au premier lancement de l'une des applications.

### Exécuter l'Application Admin (BackOffice Desktop)

1. Ouvrir le projet `BackOffice.Desktop`
2. Exécuter l'application (F5)
3. Se connecter avec :
   - Username : `admin`
   - Password : `admin123`

### Exécuter l'Application Client (FrontOffice Web)

1. Ouvrir le projet `FrontOffice.Web`
2. Exécuter l'application (F5)
3. L'application s'ouvrira dans le navigateur (généralement `https://localhost:5001` ou `http://localhost:5000`)
4. Pour créer un compte client, cliquer sur "Register"
5. Pour se connecter, utiliser l'email et le mot de passe créés

## Utilisation

### Pour un Administrateur

1. Lancer l'application `BackOffice.Desktop`
2. Se connecter avec les identifiants admin
3. Utiliser les onglets pour naviguer entre :
   - **Vehicles** : Gérer les véhicules
   - **Clients** : Gérer les clients
   - **Rentals** : Consulter les locations

### Pour un Client

1. Accéder à l'application web
2. Créer un compte (Register) ou se connecter (Login)
3. Choisir "Client" dans le type d'utilisateur lors de la connexion
4. Consulter les véhicules disponibles
5. Cliquer sur "Request Rental" pour un véhicule
6. Remplir le formulaire de location
7. Confirmer la demande

## Notes Importantes

- ⚠️ **Sécurité** : Ce projet est conçu pour un niveau étudiant. Les mots de passe ne sont pas hashés et l'authentification est basique. Pour un environnement de production, il faudrait implémenter une authentification sécurisée (ASP.NET Core Identity, JWT, etc.).

- ⚠️ **Email** : L'envoi d'email est simulé (log dans la console). Pour un environnement réel, configurer un service SMTP.

- ⚠️ **QR Code** : Le QR Code est généré et stocké dans la base de données. Il peut être visualisé via l'URL `/Rentals/QrCode/{id}`.

## Structure de la Base de Données

### Table Admin
- Id (int, PK)
- Username (string)
- Password (string)
- Email (string)

### Table Client
- Id (int, PK)
- FirstName (string)
- LastName (string)
- Email (string)
- Phone (string)
- Password (string)
- CreatedAt (DateTime)

### Table Vehicle
- Id (int, PK)
- Brand (string)
- Model (string)
- Year (int)
- Color (string)
- LicensePlate (string)
- DailyRate (decimal)
- IsAvailable (bool)
- Description (string, nullable)

### Table Rental
- Id (int, PK)
- ClientId (int, FK)
- VehicleId (int, FK)
- StartDate (DateTime)
- EndDate (DateTime)
- TotalAmount (decimal)
- Status (string) : "Pending", "Confirmed", "Completed", "Cancelled"
- CreatedAt (DateTime)
- QrCodeData (string, nullable)

## Dépannage

### Problème : La base de données ne se crée pas

**Solution** : Vérifier que SQL Server LocalDB est installé et démarré. Vous pouvez le vérifier avec :
```bash
sqllocaldb info mssqllocaldb
```

### Problème : Erreur de connexion à la base de données

**Solution** : Vérifier la chaîne de connexion dans `CarRentalDbContext.cs` et s'assurer que LocalDB est accessible.

### Problème : L'application ne compile pas

**Solution** : 
1. Restaurer les packages NuGet : `dotnet restore`
2. Nettoyer et reconstruire : `dotnet clean && dotnet build`

## Auteur

Projet développé dans le cadre d'un Mini Projet .NET universitaire.

## Licence

Ce projet est un projet éducatif.

