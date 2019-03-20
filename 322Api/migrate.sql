CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320000806_migration_1') THEN
    CREATE TABLE "Components" (
        "Id" serial NOT NULL,
        "DeviceID" integer NOT NULL,
        "ComponentName" text NULL,
        CONSTRAINT "PK_Components" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320000806_migration_1') THEN
    CREATE TABLE "Phones" (
        "Id" serial NOT NULL,
        "Name" text NULL,
        "LastCrawl" timestamp without time zone NOT NULL,
        "Score" integer NOT NULL,
        "Providers" text[] NULL,
        CONSTRAINT "PK_Phones" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320000806_migration_1') THEN
    CREATE TABLE "Reviews" (
        "Id" serial NOT NULL,
        "PhoneId" integer NOT NULL,
        "SourceId" integer NOT NULL,
        "ReviewText" text NULL,
        CONSTRAINT "PK_Reviews" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320000806_migration_1') THEN
    CREATE TABLE "ReviewSources" (
        "Id" serial NOT NULL,
        "SourceName" text NULL,
        CONSTRAINT "PK_ReviewSources" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320000806_migration_1') THEN
    CREATE TABLE "Users" (
        "Id" serial NOT NULL,
        "Username" text NULL,
        "Password" text NULL,
        "History" text[] NULL,
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320000806_migration_1') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190320000806_migration_1', '2.1.8-servicing-32085');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320000835_migration_4') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190320000835_migration_4', '2.1.8-servicing-32085');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320001709_migration_6') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190320001709_migration_6', '2.1.8-servicing-32085');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320001748_migration_8') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190320001748_migration_8', '2.1.8-servicing-32085');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320011412_migration_10') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190320011412_migration_10', '2.1.8-servicing-32085');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320011457_migration_12') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190320011457_migration_12', '2.1.8-servicing-32085');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320011539_migration_14') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190320011539_migration_14', '2.1.8-servicing-32085');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20190320011652_migration_16') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190320011652_migration_16', '2.1.8-servicing-32085');
    END IF;
END $$;
