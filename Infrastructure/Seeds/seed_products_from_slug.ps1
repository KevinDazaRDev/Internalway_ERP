$ErrorActionPreference = "Stop"

$jsonPath = "C:\Users\ananp\Desktop\Amway Proyect\Amway\Infrastructure\Seeds\amway_products_seed_by_slug.json"
$sqlPath = "C:\Users\ananp\Desktop\Amway Proyect\Amway\Infrastructure\Seeds\seed_products_from_slug.sql"

# Update this connection string before running
$conn = "postgresql://USER:PASSWORD@HOST:5432/DBNAME?sslmode=require"

$json = Get-Content -Raw -Path $jsonPath

psql $conn -v json="$json" -f $sqlPath
