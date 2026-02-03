-- Seed products from JSON payload passed as psql variable :json
-- Usage (via PowerShell): see seed_products_from_slug.ps1

-- Report missing brand slugs
with data as (
  select jsonb_array_elements(:'json'::jsonb) as item
)
select distinct item->>'brandSlug' as missing_brand_slug
from data d
left join brands b on b.slug = d.item->>'brandSlug'
where b.id is null;

-- Report missing category slugs
with data as (
  select jsonb_array_elements(:'json'::jsonb) as item
)
select distinct item->>'categorySlug' as missing_category_slug
from data d
left join categories c on c.slug = d.item->>'categorySlug'
where c.id is null;

-- Insert/update products
with data as (
  select jsonb_array_elements(:'json'::jsonb) as item
)
insert into products (
  brand_id,
  category_id,
  name,
  slug,
  sku,
  price,
  currency,
  is_active,
  created_at,
  updated_at
)
select
  b.id as brand_id,
  c.id as category_id,
  d.item->>'name' as name,
  d.item->>'slug' as slug,
  nullif(d.item->>'sku', '') as sku,
  nullif(d.item->>'price', '')::numeric as price,
  nullif(d.item->>'currency', '') as currency,
  true as is_active,
  now() as created_at,
  now() as updated_at
from data d
join brands b on b.slug = d.item->>'brandSlug'
join categories c on c.slug = d.item->>'categorySlug'
on conflict (slug) do update
set
  brand_id = excluded.brand_id,
  category_id = excluded.category_id,
  name = excluded.name,
  sku = excluded.sku,
  price = excluded.price,
  currency = excluded.currency,
  updated_at = now();
