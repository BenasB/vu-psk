# A system for cooking recipes

Built as part of the "Programų sistemų kūrimas" module at Vilnius University

## System design

```mermaid
flowchart LR
    A(User) --> B[FrontEnd]
    B --> C[Recipes]
    B --> D[Identity]
    C --> E[Recipes DB]
    D --> F[Identity DB]
```

## Screenshots

#### Discover page

![Discover page](./docs/discover.png)

#### See a recipe

![See a recipe](./docs/single-recipe.png)

#### Editing a recipe

![Edit a recipe](./docs/edit.png)

#### User page

![User page](./docs/user.png)
