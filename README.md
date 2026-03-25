# sae-402

## Notes:

Ce commit va

## Répartition travail

### Abdel:

- Compléter le niveau du projet en permettant au joueur de passer au niveau suivant
    - Il y a déjà des trophées (prefab) qui servent de fin de niveau, il faudra juste les compléter
- Afficher un indicateur du nombre de points de vie
    - Optionnel : possibilité de proposer le regain de vie
    - Note : Vous pouvez également décider qu'au moindre dégât le joueur meurt immédiatement
> La gestion des points de vie du joueur est gérée via un ScriptableObject. Qui gère à la fois le nombre de points de vie actuels et maximum. A noter que la valeur des points de vie actuels est "clampée", autrement dit, elle ne peut pas être inférieure à 0 ni supérieure au nombre de points de vie maximum définis. Si vous le souhaitez, vous pouvez supprimer ce comportement.

### Malo:

- Terminer la gestion de la mort du personnage. A l'heure actuelle, il y a :
    - un évènement (OnPlayerDeath) sur le GameObject "Player" (utilisable depuis l'onglet `Assets` en Play Mode)
    - une animation de mort du personnage (testable avec la touche N du clavier)
    - "Suppression" du Rigidbody2D associé (Passage de "Simulated" à "Non simulated"). Le personnage traverse les murs à la mort
    - La mort instanée si le joueur est écrasé par un RockHead ou que ses points de vie atteignent 0
    - **A vous de faire le reste** (liste non exhaustive)
      - Bloquer les mouvements du joueur
      - Afficher un écran de "Fin de partie"
      - ...

### Timéo:

- Ajouter un nouveau niveau (décors, ennemis et mécaniques compris)
    - Vous pouvez utiliser le thème que vous souhaitez pour la décoration. Vous pouvez donc importer de nouvelles tilemaps ou utiliser celles déjà présentes dans le projet
    - Vous pouvez récupérer des mécanismes/ennemis déjà présents dans le premier niveau
     - Il est **impératif** d'avoir dans vos niveaux supplémentaires la prefab "CurrentSceneManager"
    - **Il doit être possible de terminer ce nouveau niveau**. Quand le niveau termine (au choix) :
        - Afficher les crédits
        - Retourner au menu principal
    - Vous pourrez trouver des inspirations ici :
        - [https://pixelfrog-assets.itch.io/pixel-adventure-1](https://pixelfrog-assets.itch.io/pixel-adventure-1)
        - [https://pixelfrog-assets.itch.io/pixel-adventure-2](https://pixelfrog-assets.itch.io/pixel-adventure-2)

### Yves:

- Rajouter un ennemi parmi ceux proposés
    - Dans l'archive que vous avez récupéré au début des cours, il y a un dossier `ressources/unity/sprites/platformer/Enemies`
        - N'oubliez pas qu'il y déjà des scripts décrivant des comportants appliquables à un ennemi : Patrouille (EnemyPatrol), Tir (EnemyShooting + ObjectPooling), Santé (Enemy)... à vous de les réutiliser, au besoin, pour votre nouvel ennemi
        - Voir [gestion des ennemis](#gestion-des-ennemis) pour en savoir plus
- Remplacer les carrés bleus qui font office de checkpoints par un sprite plus approprié
    - Il y a un sprite dédié dans le projet (`Assets/Imports/Scripts/Misc/Checkpoint*`)
- Ajouter un état "désactivé" quand un bloc n'est plus interagissable
  - **Vous devez faire vous-même le sprite**

### Bassem:

- Compléter l'écran d'accueil (écran qui permet de commencer le jeu)
    - L'écran doit contenir :
        - Le logo de l'université
            - Plusieurs versions sont déjà présentes dans le projet dans le dossier `Assets/Imports/Logos` à vous de choisir
        - Le nom du jeu
          - Vous devez en trouver un
          - Le logo peut être fait sous Photoshop ou autre puis intégré sous forme d'image dans Unity ou tout simplement du texte
        - De quoi commencer le jeu au premier niveau
            - Il y a déjà un bouton, il faut ajouter la fonction pour charger le premier niveau grâce au `GameObject` CurrentSceneManager
        - Un bouton pour permettre de quitter le jeu
        - Les développeurs / développeuses du jeu, le nom de la formation et l'année scolaire courante
    - Pour rappel, ici il faudra utiliser un Canvas
- Ajouter un écran des crédits [(voir contenu attendu)](#contenu-de-lécran-des-crédits)
  - Ces crédits doivent être accessible depuis l'écran d'accueil, au minimum
- Afficher dans la splash page de jeu le logo de l'université (voir menu `Project Settings > Player > Logo`)
    - [Didacticiel en anglais sur la personnalisation de la splash page](https://www.youtube.com/watch?v=BY40xbA5qYQ)
- Compléter le menu Pause
    - Il est déjà possible de mettre le jeu en pause en appuyant sur la touche "Echap". Grâce au composant "PauseManager" dans le projet, complétez-le
    - La gestion du menu pause est faite grâce à un ScriptableObject (`Assets/ScriptableObjects/Events/Events/OnTogglePauseEvent`)
    - Libre à vous d'ajouter d'autres options dans le menu de pause comme relancer le niveau ou encore retourner au menu principal via un bouton
    - Le menu Pause doit **au minimum** contenir un bouton pour relancer le niveau et revenir au menu principal
    > Le menu Pause contient du texte. Toutefois, il est possible qu'il ne s'affiche pas. C'est lié à des packages Unity manquants. Pour ce faire, allez dans le menu d'Unity : `Window > TextMeshPro > Import TMP Essential Ressources.` Ceci va afficher une fenêtre, cliquez sur le bouton "Import" en bas à droite.

## Votre liste à faire
- [x] Lire les consignes
- [x] Former votre groupe (3 à 5 max), plus tôt vous le ferez, plus tôt vous pourrez commencer à travailler sereinement
  - **N'oubliez pas que si vous êtes plus de trois, vous avez plus de choses à faire**
- [x] Initialiser le projet sur github.
- [ ] Respecter les attentes
- [ ] Tester le jeu avant de le rendre
- [ ] Générer une archive contenant :
    - **Votre build pour Windows ou MacOS du jeu (pas de build WebGL)**
        - Pensez à tester le build final de votre jeu. Il faut faire un build de production, **l'option "Développement Build" ne doit pas être cochée**
            - [Voir didacticiel sur la génération d'un build](https://github.com/DanYellow/cours/blob/main/creation-et-design-interactif-s4/travaux-pratiques/numero-1/ressources/unity/BUILD.md)
        - Un fichier texte avec le lien du projet git + les fonctionnalités supplémentaires que vous avez ajouté ([partie "Liste des choses à faire au choix"](#liste-des-choses-à-faire-au-choix-vous-devez-au-moins-en-faire-une-deux-si-vous-êtes-plus-de-trois-dans-votre-groupe))
    > Note : Pour vous aider, un menu "Debug" a été ajouté dans le menu d'Unity avec une option "Create build". Pour rappel, un build ne se commite pas. Le .gitignore du projet crée ignore tout le contenu du dossier "Builds"
