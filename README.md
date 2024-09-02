Group: s17-04-ft-csharp

# JuniorHub

### 1. Descripción General del Proyecto

**Brand:** JuniorHub

**Idea:** JuniorHub es una plataforma web diseñada para conectar programadores y diseñadores junior con empresas y clientes que buscan talento fresco y asequible. Facilita el proceso de trabajo freelance, permitiendo que freelancers junior encuentren oportunidades adecuadas a su nivel de experiencia y que los empleadores accedan a un grupo de talentos emergentes. JuniorHub no solo conecta a ambas partes, sino que también ofrece herramientas para gestionar proyectos, desde la publicación y postulación hasta la evaluación y retroalimentación final, asegurando una experiencia fluida y enriquecedora para todos los usuarios.

### 2. Roles y Funcionalidades

### Roles Disponibles

- **Admin:** Gestión completa de usuarios (freelancers y employers), incluyendo su creación, edición, visualización y eliminación.
- **Employer:** Registro, inicio de sesión, publicación y gestión de ofertas de trabajo, búsqueda de freelancers, evaluación y calificación de freelancers tras la finalización de un proyecto.
- **Freelancer:** Registro, inicio de sesión, creación y gestión de un perfil profesional, búsqueda de ofertas de trabajo, postulación a proyectos, evaluación y calificación de empleadores tras la finalización de un proyecto.

### 3. Flujo de Trabajo

### Admin

- Gestión integral de usuarios, permitiendo la creación, edición, visualización y eliminación de cuentas de freelancers y employers.

### Employer

- Registro e inicio de sesión en la plataforma.
- Publicación de ofertas de trabajo con descripciones detalladas, requisitos, honorarios y plazos de entrega.
- Recepción de postulaciones de freelancers, evaluación y selección de candidatos.
- Evaluación y calificación de freelancers tras la finalización del proyecto, con la posibilidad de dejar comentarios y reseñas.

### Freelancer

- Registro e inicio de sesión en la plataforma.
- Creación y actualización de un perfil profesional, incluyendo datos personales, habilidades, tecnologías dominadas, experiencia previa y enlaces a portafolios y redes profesionales.
- Búsqueda de ofertas de trabajo disponibles según título y tecnologías.
- Aplicación a proyectos.
- Evaluación y calificación de employers tras la finalización del proyecto, con la posibilidad de dejar comentarios y reseñas.

### 4. Requerimientos Funcionales

### Employer

- **Publicación de Proyectos:**
  - Publicar proyectos con una descripción clara, especificaciones técnicas, requisitos de habilidades, tarifas por hora o fijas, y fechas de entrega.
- **Gestión de Postulaciones:**
  - Revisar y gestionar las postulaciones recibidas.
- **Búsqueda de Freelancers:**
  - Buscar freelancers por habilidades específicas, tecnologías dominadas y rol principal.
- **Evaluación de Freelancers:**
  - Calificar a los freelancers al finalizar un proyecto con una puntuación de 1 a 5 y dejar comentarios detallados sobre su desempeño.

### Freelancer

- **Creación de Perfil Profesional:**
  - Completar un perfil detallado que incluya datos personales, habilidades, tecnologías dominadas, roles principales, experiencia previa y enlaces a LinkedIn, GitHub, Figma y portafolios.
- **Búsqueda de Proyectos:**
  - Buscar proyectos por título y tecnologías requeridas.
- **Aplicación a Proyectos:**
  - Enviar postulaciones a los proyectos publicados.
- **Gestión de Aplicaciones:**
  - Visualizar y gestionar las aplicaciones realizadas, permitiendo el seguimiento de las postulaciones y su estado actual.
- **Evaluación de Employers:**
  - Calificar a los employers al finalizar un proyecto con una puntuación de 1 a 5 y dejar comentarios sobre la experiencia de trabajo.

### 5. Páginas del Proyecto

### Página inicial

- **Landing Page** (P2)

### Páginas de Acceso y Registro

- **Employer Register** (P1)
- **Employer Login** (P1)
- **Freelancer Register** (P1)
- **Freelancer Login** (P1)

### Páginas de Employer

- **Employer Dashboard** (P1)
  - Gestión completa de ofertas de trabajo (CRUD Offers).
  - Búsqueda y filtrado de freelancers.

### Páginas de Freelancer

- **Perfil del Freelancer** (P1)
  - **Mis Aplicaciones Anteriores**: Ver ofertas a las que ya se ha postulado.
  - **Ver Aplicaciones Actuales**: Ver las ofertas a las que está postulando actualmente.
  - **Editar Perfil**: Modificar la información del perfil.
- **Ver Ofertas Publicadas** (P1)
  - **Detalle de la Oferta** (P1)
    - **Aplicar a la Oferta** (P1)

### 6. Filtros de Búsqueda

- **Employer:** Capacidad de buscar freelancers dentro de cada oferta publicada, filtrando por habilidades, tecnologías y rol principal.
- **Freelancer:** Posibilidad de filtrar ofertas de trabajo por título, tecnologías requeridas, presupuesto y plazo de entrega.
