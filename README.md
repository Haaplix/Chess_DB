# CHESDB

## Un projet fait dans le cadre du cours "Object Oriented Programming".

Il s'agit d'une application de gestion des matchs d’une fédération d’échecs qui sera gérée par le personnel administratif de la fédération. Cette application peut être déclinée pour tous les sports qui ont des matchs un contre un.

## 1. Fonctionnalitées

- Gérer les joueurs (Ajouter, modifier);
- Gérer les compétitions (Ajouter, modifier);
- Incription des joueurs aux compétitions;
- Gérer les matchs de chaque compétitons;
- _Fonctionnalité supplémentaire:_ Classement des joueurs en focntion de leur ELO;
- Toutes les données sont sauvegardées dans une base données.

### 1.1 Gérer les joueurs

Joueur: **Prénom, Nom, ELO, Id**

- Affichage d'une liste de joueurs;
- Recherche dans la liste de joueurs (grâce aux Nom, Prénom, ELO, Id);
- Ajout d'un joueur;
- Affichage du joueur, de ses matchs joués et des compétitions participées dans une page spécifique à celui-ci;
- Modification d'un joueur dans une page spécifique à celui-ci.

### 1.2 Gérer les compétitions

Compétiton: **Nom, Pays, Ville, Date, Id**

- Affichage d'une liste de compétitions;
- Recherche dans la liste de compétitions (grâce aux Nom, Pays, Ville, Date, Id);
- Ajout d'une compétitions;
- Affichage de la compétiton, de ses matchs et des joueurs qui ont participés dans une page spécifique à celle-ci;
- Modification d'une compétition dans une page spécifique à celle-ci.

### 1.3 Inscription des joueurs aux compétitions:

- Affichage d'une liste des joueurs inscrits à la compétition;
- Ajout de joueur à la compétition.

### 1.4 Gérer les matchs de chaque compétitons:

Match: **Joueur1, Joueur2, les Coups de chaque joueur, Gagnant, Competition**

- Affichage d'une liste des matchs de la compétitions;
- Ajout de matchs à la compétition;
- Affichage du match dans une fenêtre spécifique à celui-ci;
- Modification du match dans un page sécifique à celui-ci.

### 1.5 _Fonctionnalité supplémentaire:_ Classement des joueurs en fonction de leur ELO

Dans une page spécifique au classement, est affichée une liste de tous les joueurs triée par ELO. Le joueur qui a le plus d'ELO est en haut de la liste. Il est possible de rechercher un joueur en fonction de son Nom, Prénom, ELO et Id.

## 2. Diagramme de classes

![Diagramme de classes](/Pictures/Diagramme%20de%20classes.png)

## Diagramme de séquence

Diagramme de séquence qui montre l'affichage d'un match ainsi que la création d'un match.

![Diagramme de séquence](/Pictures/Diagramme%20de%20séquence.svg)

## 3. Diagramme d'activité

Diagramme d'activé de création d'une compétition.

![Diagramme activé](/Pictures/Diagramme%20activite.svg)

## 4. Adaptabilité à d'autres fédérations/sports

Les seuls paramètres propres aux échecs sont;

- Les coups
- L'ELO

Les coups peuvent être décliné en score (ex: 2-1) mais si la fédération n'utilise pas de score, les coups ne sont pas obligatoires pour enregistrer un match.
L'ELO peut être décliné/supprimé pour s'adapter à un autre sport.

Il n'y a pas besoin de modifier toute l'application si il faut l'utiliser avec une autre fédération. Il y a juste quelques paramètres à modifier qui ne vont pas interférer avec le fonctionnement global de l'application.

Cette application peut donc être déclinée pour tous les fédérations/sports qui ont des matchs un contre un (1v1).

## 5. Principes SOLID

### 5.1 Single Responsibility Principle

Chaque ViewModel est reponsable de la gestion d'une seule chose.

Prenons la classe **PlayerViewModel** en exemple, elle ne peut pas modifier les compétitions ni les matchs, elle a juste accès à la base de données pour afficher ce qui est relié au joueur. Donc **PlayerViewModel** ne gère que **Player**. De même pour les autres ViewModel.

### 5.2 Open/Closed Principle

Vu que notre application est adaptable à d'autres fédérations, elle adhère au Open/Closed Principle. En effet, comme toutes les fédérations n'ont pas les mêmes règles, l'application doit être pensée pour pouvoir enlever et rajouter des fonctionnalités facilement.

Par exemple, lors de la conception de l'application, la fonctionnalité **Ranking** a été rajoutée en dernière et nous n'avons pas dû faire de modification au reste du code pour que cela fonctionne.

## 6. Base de données

En ce qui concerne la base de données, nous avons utilisé SQLite et l'ORM Entity Framework Core.

## 7. Conclusion

En conclusion, cette application correspond au cahier des charges;

- Gestion de Joueurs, Compétitions, Matchs;
- Flexibilité pour d'autres fédérations/sports;
- Sauvegarde persistante des données.

Cependant, une piste d'amélioration serait de faire une banque de ViewModel au lieu d'en créer à chaque fois des nouveaux.
