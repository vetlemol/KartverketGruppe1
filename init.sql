-- Sjekk om brukeren allerede eksisterer
SELECT COUNT(*) INTO @user_exists FROM mysql.user WHERE user = 'root' AND host = '%';

-- Hvis brukeren ikke eksisterer, opprett brukeren
IF @user_exists = 0 THEN
    CREATE USER 'root'@'%' IDENTIFIED BY 'test123';
ELSE
    -- Hvis brukeren eksisterer, oppdater passordet
    ALTER USER 'root'@'%' IDENTIFIED BY 'test123';
END IF;

-- Gi brukeren tilgang til databasen
GRANT ALL PRIVILEGES ON kartverketgruppe1* TO 'root'@'localhost';
FLUSH PRIVILEGES;