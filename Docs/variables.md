# Variables

| Description | Type | Unit |
|-------------|------|------|
| masse | M | kg |
| position | vec x | m |
| vitesse | v | m/s |
| velocité | vec v | m/s |
| distance | d | m |
| temps | t | s |
| acceleration | a | m/s² |
| Force | N | (kg * m / s²) |

<!-- | force | F | N |
| energie | E | J |
| puissance | P | W |
| pression | P | Pa |
| densite | ρ | kg/m³ |
| volume | V | m³ |
| surface | S | m² |
| frequence | f | Hz |
| longueur | L | m | -->

# Constantes

- $$ G = 6.6738 \times 10^{-11} $$

# Formules

- $$ \vec{v} = \vec{x}/t $$
- $$ \vec{a} = \vec{v}/t^2 $$

## Le mouvement rectiligne uniformément accéléré

- $$ \vec{x} = \vec{x} + (\vec{v} \times t) + (\frac{1}{2} \times \vec{a} \times t^2) $$

- $$ \vec{v} = \vec{v} + (\vec{a} \times t) $$

- $$ \vec{N} = \sum f $$

- $$ \vec{a} = \frac{\vec{N}}{M} $$


## Mise en Mouvement

### Gravitation

- $$ N = \frac{G \times M_1 \times M_2}{d^2} $$

- $$ \vec{r} = \vec{r_2} - \vec{r_1} $$

- $$ \vec{\lvert r \rvert} = \sqrt{r_x^2 + r_y^2 + r_z^2} $$

- $$ \hat{r} = \frac{\vec{r}}{\vec{\lvert r \rvert}} $$

- $$ \vec{F} = -\frac{G \times M_1 \times M_2}{d^2} \times \hat{r} $$

### Mouvement Orbital

- $$ a = \frac{r_{min} + r_{max}}{2} $$

- $$ b = r_{min} $$

- $$ e = \sqrt{1 - \frac{b^2}{a^2}} $$

- $$ \theta = angle $$

- $$ r(\theta) = \frac{a \times (1 - e^2)}{1 + e \times \cos(\theta)}$$

- $$ v_0 = \sqrt[2]{G \times M \times (\frac{2}{r} - \frac{1}{a})} $$

### Frottement Statique

- $$ \vec{N} = \mu_s \times \vec{N} $$

### Frottement Cinétique

- $$ \vec{N} = \mu_c \times \vec{N} $$

### Force Normale

- $$ \vec{N} = M \times \vec{g} $$

M = masse;
g = gravité

### Frottement de l'air

- $$ \vec{N} = \frac{1}{2} \times \rho \times S \times C_x \times \vec{v}^2 $$
