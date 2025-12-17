# CHESDB

## Un projet fait dans le cadre du cours "Object Oriented Programming".

Il s'agit d'une application de gestion des matchs d’une fédération d’échecs qui sera gérée par le personnel administratif de la fédération. Cette application peut être déclinée pour tous les sports qui ont des matchs un contre un.

## Fonctionnalitées

- Gérer les joueurs (Ajouter, modifier);
- Gérer les compétitions (Ajouter, modifier);
- Incription des joueurs aux compétitions;
- Gérer les matchs de chaque compétitons;
- _Fonctionnalité supplémentaire:_ Classement des joueurs en focntion de leur ELO;
- Toutes les données sont sauvegardées dans une base données.

### Gérer les joueurs

Dans une page spécifique aux joueurs, il y a la possibilité d'ajouter un joueur en spécifiant: **Prénom, Nom, ELO**. Pour chaque joueur, un **Id** leur sera attribué automatiquement.\

Sur la page, la liste de chaque joueur ajouté est affichée avec leur Nom, Prénom, ELO, Id. Chaque joueur est un bouton cliquable qui permet d'afficher plus de détail sur le joueur sélectionné.\

Dans la page spécifique à un seul joueur, il est possible de le modifier (ex: Nom, Prénom,...).

### Gérer les compétitions

Dans une page spécifique aux compétitions, il y a la possibilité d'ajouter une compétition en spécifiant: **Nom, Pays, Ville, Date**. Pour chaque compétition, un **Id** leur sera attribué automatiquement.\

Sur la page, la liste de chaque compétition ajoutée est affichée avec leur Nom, Pays, Ville, Date, Id. Chaque compétition est un bouton cliquable qui permet d'afficher plus de détail sur la compétition sélectionnée.\

Dans la page spécifique à une seule compétition, il est possible de la modifier (ex: Nom, Pays,...).

#### Inscription des joueurs aux compétitions

Une liste des joueurs inscrits est affichée. Le bouton + à coté de la liste, permet d'ajouter des joueurs à la compétition.

#### Gérer les matchs de chaque compétitons

Une liste des matchs est affichée. Le bouton + à coté de la liste, permet d'ajouter des matchs à la compétition. Les matchs contiennent: **Joueur1, Joueur2, les Coups de chaque joueur, Gagnant**.

### _Fonctionnalité supplémentaire:_ Classement des joueurs en fonction de leur ELO

Dans une page spécifique au classement, est affichée une liste de tous les joueurs triée par ELO. Le joueur qui a le plus d'ELO est en haut de la liste. Il est possible de rechercher un joueur en fonction de son Nom, Prénom, ELO et Id.

## Diagramme de classes

## Diagramme de séquence

## Diagramme d'activité

## Adaptabilité à d'autres fédérations/sports

Cette application peut être déclinée pour tous les fédérations/sports qui ont des matchs un contre un.\

Les seuls paramètres propres aux échecs sont;

- Les coups
- L'ELO

Les coups peuvent être modifiés pour inscrire un score (ex: 2-1) et l'ELO peut être décliné/supprimé pour s'adapter à un autre sport.\

Il n'y a pas besoin de modifier toute l'application si il faut l'utiliser avec une autre fédération. Il y a juste quelques paramètres à modifier qui ne vont pas interférer avec le fonctionnement global de l'application.

## Principes SOLID

## Conclusion
