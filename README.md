# Lucca : test technique développeur backend

Le test consiste à réaliser une application web en .NET avec une API REST pour :
- Créer des dépenses
- Lister les dépenses

# Spécifications
## Ressources
### Dépenses
Une dépense est caractérisée par :

- Un utilisateur (personne qui a effectué l'achat)
- Une date
- Une nature (valeurs possibles : Restaurant, Hotel et Misc)
- Un montant et une devise
- Un commentaire

### Utilisateurs
Un utilisateur est caractérisé par :

- Un nom de famille
- Un prénom
- Une devise dans laquelle il effectue ses dépenses

## Fonctionnalités principales
### Création d'une dépense
Avant de persister la dépense, il faut vérifier qu'elle soit valide.

Règles de validation d'une dépense :

- Une dépense ne peut pas avoir une date dans le futur,
- Une dépense ne peut pas être datée de plus de 3 mois,
- Le commentaire est obligatoire,
- Un utilisateur ne peut pas déclarer deux fois la même dépense (même date et même montant),
- La devise de la dépense doit être identique à celle de l'utilisateur.

### Liste des dépenses
L'API (REST) doit permettre de :

- Lister les dépenses pour un utilisateur donné,
- Trier les dépenses par montant ou par date,
- Afficher toutes les propriétés de la dépense ; l'utilisateur de la dépense doit apparaitre sous la forme {FirstName} {LastName} (eg: "Anthony Stark").

# Contraintes techniques
## Langage
L'application doit être développée en C#/.NET.

## Stockage
Les données doivent être persistées dans une base de données SQL.

La table des utilisateurs doit être initialisée avec les utilisateurs suivants :

- Stark Anthony (avec pour devise le dollar américain),
- Romanova Natasha (avec pour devise le rouble russe).

## Utilisation de librairies
Comme tout développeur, nous n'aimons pas réinventer la roue, et apprécions de ce fait utiliser diverses bibliothèques selon les besoins.

Tu es donc libre d'utiliser les librairies qui te semblent pertinentes.

## Délais et ressources
A la reception de ce test technique, envoie-nous une estimation de livraison du rendu de ton test technique.

On estime qu'il faut environ 5 heures pour réaliser ce test technique.

En cas de doute, n'hésite pas à poser des questions en envoyant un mail à test-technique@luccasoftware.com

# Critères d'évaluation
L'évaluation se fera en fonction de plusieurs critères et également du degré d'exigence nécessaire au poste auquel tu postules.

## Critères de qualité
- Le code doit être propre, lisible, extensible, bien structuré et facilement maintenable.
- Le code doit respecter les bonnes pratiques de développement.
- La solution proposée doit comporter des tests unitaires.

## Critères d'acceptance
Toutes les fonctionnalités décrites dans les consignes doivent être implémentées et fonctionnelles.

Les règles de validation de la dépense doivent être testées unitairement.

## Critères de performance
- L'application doit être rapide et réactive.
- Les temps de chargement doivent être minimaux.

# Suite du process
- Une fois que tu auras poussé ton repo, merci de prévenir Marion ou Florence par mail que tu as finalisé ton test.
- Ton test sera alors évalué par l'équipe de recrutement et tu recevras une réponse dans un délai d'une semaine.
- Si tu es retenu, tu pourras échanger avec ta future équipe pour une rencontre avec celle-ci et un entretien technique (Petit Oral).
- Si tu n'es pas retenu, tu recevras un e-mail détaillé avec des explications.
