# TP 1 Automne 2024 - IFT 2103 - Équipe 21

## Table des matières
- [Définition de l’environnement](#définition-de-lenvironnement)
- [Diagramme de la boucle de jeu](#diagramme-de-la-boucle-de-jeu)
- [Définition de l’action réalisée](#définition-de-laction-réalisée)
- [Formes de collisionneurs supportées](#formes-de-collisionneurs-supportées)
- [Optimisation de la détection de collision](#optimisation-de-la-détection-de-collision)
- [Les réactions aux collisions](#les-réactions-aux-collisions)

## Définition de l’environnement
#### Environement

- **Espace euclidien**: Un espace euclidien fait référence à un espace qui suit la géométrie d'Euclide. Il contient des droites parallèles, angles fixes, et des distances constantes.

#### Dimensions

- **Nombre**: _3_

- **Nature**: _Continue_. Une nature continue signifie que les valeurs peuvent être n'importe quel nombre réel. Comme le monde réel. On utilise des ``vector3`` pour représenter les coordonnées des objets.``float x``, ``float y``, ``float z``

- **Forme**: _Plane_. Une forme plane signifie que l'espace est plat et que les objets peuvent se déplacer dans toutes les directions.

- **Grandeur**: _Infini_. L'espace est infini, ce qui signifie que les objets peuvent se déplacer indéfiniment.

## Diagramme de la boucle de jeu

![alt text](Docs/diagramme1.svg)

## Définition de l’action réalisée

- **Gravité**: La gravité est une force qui attire les objets vers le bas. Elle est causée par la masse des objets.

- **Frottement**: Le frottement est une force qui s'oppose au mouvement des objets. Il est causé par le contact entre les objets et l'air ou une surface.

## Formes de collisionneurs supportées

- **Shères**: Les sphères sont des objets géométriques en forme de boule. Elles sont souvent utilisées pour représenter des objets ronds comme des balles ou des planètes.

- **Capsule**: Les capsules sont des objets géométriques en forme de cylindre avec des extrémités arrondies.

## Optimisation de la détection de collision

- **Gestionaire de collision**: Un gestionnaire de collision est un système qui détecte les collisions entre les objets et gère les réactions aux collisions.

## Les réactions aux collisions

Il y a 2 types de réactions aux collisions:

- **Rebond**: Lorsque qu'un objet entre en collision avec un autre, il rebondit. Cela signifie que l'objet change de direction et de vitesse.

- **Immobile**: Lorsque qu'un objet entre en collision avec un autre, il s'arrête. Cela signifie que l'objet ne bouge plus.