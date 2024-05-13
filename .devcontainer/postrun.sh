sudo apt-get update
sudo apt-get install -y postgresql-client
sudo apt-get install -y postgresql postgresql-contrib
dotnet --info
dotnet dev-certs https --trust
dotnet tool install --global dotnet-ef

PGPASSWORD=$POSTGRES_PASSWORD psql -h $POSTGRES_HOST -U $POSTGRES_USER -d $POSTGRES_DB -p $POSTGRES_PORT -c "
CREATE TABLE IF NOT EXISTS random_data (
    id SERIAL PRIMARY KEY,
    random_value INTEGER NOT NULL
);

INSERT INTO random_data (random_value) VALUES (floor(random() * 10000)::int);
"
