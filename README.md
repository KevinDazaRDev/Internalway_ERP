# Internalway ERP - Amway Catalog Backend

**Visión General**
Este proyecto es el backend REST para un catálogo tipo e‑commerce de Amway. Resuelve la necesidad de gestionar marcas, categorías jerárquicas y productos de forma consistente, escalable y orientada a negocio, preparando el terreno para futuras ventas, órdenes e integraciones externas. Es un API construida con ASP.NET Core y PostgreSQL, siguiendo principios de Clean Architecture.

**Modelo De Dominio (Negocio)**
- **Marca (Brand):** representa una marca comercial de Amway (ej. Nutrilite, Artistry). Es un concepto de catálogo y branding.
- **Categoría (Category):** organiza el catálogo en niveles. Una categoría puede ser raíz o subcategoría mediante `parent_id`. Esto permite jerarquías reales.
- **Producto (Product):** ítem comercializable. Pertenece a una marca y a una subcategoría (categoría hoja).
- **Cliente (Client):** entidad independiente orientada a futuras compras, órdenes y contacto.

Relaciones de negocio:
- Un **producto** pertenece a **una marca** y **una subcategoría**.
- Las **marcas no se asignan directamente** a categorías. La relación marca‑categoría se infiere a través de productos.
- Las **categorías** pueden tener subcategorías (jerarquía).

Diagrama conceptual:
```
Brand (1) ──────< (N) Product >────── (1) Category
                             Category (self-reference)
                                 parent_id -> Category.id
```

**Decisiones De Diseño Importantes**
- **Categorías jerárquicas con `parent_id`:** habilita navegación tipo tienda y categorías anidadas sin crear tablas extra.
- **Sin tabla `brand_categories`:** la relación marca‑categoría no es directa; se deriva de productos reales y evita inconsistencias.
- **Uso de slugs:** los slugs son estables para URLs y referencias externas, evitan exponer IDs internos.
- **Separación entre dominio y persistencia:** el dominio expresa reglas de negocio; la persistencia se mantiene desacoplada mediante repositorios y services.

**Estructura Conceptual De Base De Datos**
Tablas principales:
- **brands:** catálogo de marcas.
- **categories:** categorías jerárquicas con `parent_id`.
- **products:** productos con referencia a marca y categoría.
- **clients:** clientes potenciales o reales.

Relaciones clave:
- `products.brand_id -> brands.id`
- `products.category_id -> categories.id`
- `categories.parent_id -> categories.id` (self-reference)

**Flujo Típico De Uso**
Ejemplo de navegación:
1. Usuario entra a una categoría raíz (ej. Maquillaje).
2. Selecciona una subcategoría (ej. Rostro).
3. El sistema lista productos asociados a esa subcategoría.

Ejemplo de filtrado por marca:
1. Usuario selecciona la marca (ej. Nutrilite).
2. El sistema devuelve productos de esa marca, sin necesidad de tabla intermedia.

**Endpoints Principales (Alto Nivel)**
- **Brands:** gestión de marcas del catálogo.
- **Categories:** creación y mantenimiento de categorías y subcategorías.
- **Products:** CRUD de productos, filtrados y carga masiva.
- **Clients:** gestión de clientes para futuras órdenes.

**Consideraciones Futuras**
- **Orders / OrderItems:** creación de pedidos y líneas de pedido.
- **Imágenes de productos:** almacenamiento y CDN para assets.
- **Inventario:** stock por SKU y bodegas.
- **Autenticación y roles:** administración segura del catálogo.

