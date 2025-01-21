DROP TABLE IF EXISTS user_claims CASCADE;
DROP TABLE IF EXISTS users CASCADE;
DROP TABLE IF EXISTS trainings CASCADE;
DROP TABLE IF EXISTS disciples CASCADE;
DROP TABLE IF EXISTS fathers CASCADE;
DROP TABLE IF EXISTS mothers CASCADE;
DROP TABLE IF EXISTS squads CASCADE;
DROP TABLE IF EXISTS disciple_levels CASCADE;
DROP TABLE IF EXISTS sd_projects CASCADE;

CREATE TABLE IF NOT EXISTS sd_projects (
    id uuid NOT NULL,
    name character(20) NOT NULL,
    CONSTRAINT pk_sd_projects PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS users (
    id uuid NOT NULL,
    first_name character(20) NULL,
    last_name character(20) NULL,
    email varchar(50) NOT NULL,
    city character(20) NULL,
    hash_password varchar(1000) NOT NULL,
    CONSTRAINT pk_users PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS user_claims (
    id uuid NOT NULL,
    name varchar(20) NOT NULL,
    value varchar(1000) NOT NULL,
    user_id uuid NOT NULL,
    CONSTRAINT pk_user_claims PRIMARY KEY (id),
    CONSTRAINT fk_user_claims_user FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
);


CREATE TABLE IF NOT EXISTS disciple_levels (
    id uuid NOT NULL,
    name character(20) NOT NULL,
    CONSTRAINT pk_disciple_levels PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS squads (
    id uuid NOT NULL,
    name character(20) NULL,
    city character(20) NOT NULL,
    location character(50) NULL,
    user_id uuid NOT NULL,
    CONSTRAINT pk_squads PRIMARY KEY (id),
    CONSTRAINT fk_squad_user FOREIGN KEY (user_id) REFERENCES users (id)
);

CREATE TABLE IF NOT EXISTS mothers (
    id uuid NOT NULL,
    name character(50) NOT NULL,
    phone character(20) NOT NULL,
    comment character(1000) NULL,
    CONSTRAINT pk_mothers PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS fathers (
    id uuid NOT NULL,
    name character(50) NOT NULL,
    phone character(20) NOT NULL,
    comment character(1000) NULL,
    CONSTRAINT pk_fathers PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS disciples (
    id uuid NOT NULL,
    first_name character(20) NULL,
    last_name character(20) NULL,
    date_of_birth date NULL,
    sex character(10) NULL,
    project_id uuid NOT NULL,
    level_id uuid NULL,
    first_training_date date NULL,
    status character(20) NULL,
    mother_id uuid NULL,
    father_id uuid NULL,
    squad_id uuid NOT NULL,
    CONSTRAINT pk_sisciples PRIMARY KEY (Id),
    CONSTRAINT fk_disciples_sd_projects FOREIGN KEY (project_id) REFERENCES sd_projects (id) ON DELETE RESTRICT,
    CONSTRAINT fk_disciples_mothers FOREIGN KEY (mother_id) REFERENCES mothers (id) ON DELETE RESTRICT,
    CONSTRAINT fk_disciples_fathers FOREIGN KEY (father_id) REFERENCES fathers (id) ON DELETE RESTRICT,
    CONSTRAINT fk_disciples_squads FOREIGN KEY (squad_id) REFERENCES squads (id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS trainings (
    id uuid NOT NULL,
    date date NOT NULL,
    month integer NOT NULL,
    number integer NOT NULL,
    presence varchar(20) NULL,
    comment character(1000) NULL,
    disciple_id uuid NOT NULL,
    CONSTRAINT pk_trainings PRIMARY KEY (id),
    CONSTRAINT fk_trainings_disciples FOREIGN KEY (disciple_id) REFERENCES disciples (id) ON DELETE CASCADE
);